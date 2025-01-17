using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;
using GrpcClient.Models;
using Google.Protobuf.WellKnownTypes;
using System.Text.RegularExpressions;

namespace GrpcClient
{
    class ExecContainer
    {
        List<AsyncDuplexStreamingCall<ExecutionResult, SubmissionInformation>> AutoExecList = new List<AsyncDuplexStreamingCall<ExecutionResult, SubmissionInformation>>();
        List<AsyncServerStreamingCall<SubmissionInformation>> ManualExecList = new List<AsyncServerStreamingCall<SubmissionInformation>>();
        HttpClientHandler httpHandler;
        GrpcChannel channel;
        public ExecContainer()
        {
            httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            channel = GrpcChannel.ForAddress("https://localhost:7107",
            new GrpcChannelOptions { HttpHandler = httpHandler });
        }

        public async Task AutoExecClientAsync(int containerCount)
        {
            for (int i = 0; i < containerCount; i++)
            {
                AutoExecList.Add(new Docker.DockerClient(channel).AutoExecStream());
            }
            for (int i = 0; i < AutoExecList.Count; i++)
            {
                _ = AutoExecClientAsync(AutoExecList[i], i);
            }

            while (true) { await Task.Delay(10000); }
            //無限ループでリクエストストリームを閉じない
            // foreach (var server in AutoExecList)
            // {
            //     await server.RequestStream.CompleteAsync();
            // }
        }
        //自動実行コンテナの作成とサーバーとの接続
        public async Task AutoExecClientAsync(AsyncDuplexStreamingCall<ExecutionResult, SubmissionInformation> server, int i)
        {
            try
            {
                if (server == null)
                {
                    return;
                }
                while (await server.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    AutoContainerArguments autoContainerArguments = new AutoContainerArguments(server.ResponseStream.Current, i);
                    // Console.WriteLine(server.ResponseStream.Current.ToString());
                    if (!autoContainerArguments.IsAvailableLanguage)
                    {
                        await server.RequestStream.WriteAsync(new ExecutionResult { SubmissionFile = "not available language.", AnswerFile = "not available language.", Correction = 0 });
                        continue;
                    }
                    else if (!autoContainerArguments.IsZip && !autoContainerArguments.IsAvailableExtension)
                    {
                        if (autoContainerArguments.MatchType == 0)
                        {
                            await server.RequestStream.WriteAsync(new ExecutionResult { SubmissionFile = "実行できません", AnswerFile = "", Correction = 0 });
                            continue;
                        }
                        else
                        {
                            await server.RequestStream.WriteAsync(new ExecutionResult { SubmissionFile = "実行できません", AnswerFile = "", Correction = 2 });
                            continue;
                        }
                    }

                    Command autoExec = new Command(autoContainerArguments.ContainerName, autoContainerArguments.Lang, true, autoContainerArguments.InputStr);
                    StandardCmd submissionFileResult = await autoExec.AutoExecAsync(autoContainerArguments.SubmissionFiles);
                    if (autoContainerArguments.MatchType == 0)
                    {
                        if (submissionFileResult.ExitCode == 0)
                        {
                            await server.RequestStream.WriteAsync(new ExecutionResult { SubmissionFile = submissionFileResult.Output, AnswerFile = "", Correction = 0 });
                            continue;
                        }
                        else
                        {
                            await server.RequestStream.WriteAsync(new ExecutionResult { SubmissionFile = submissionFileResult.Error, AnswerFile = "", Correction = 0 });
                            continue;
                        }
                    }

                    Comparison comparison = new();

                    StandardCmd answerFileResult = await autoExec.AutoExecAsync(autoContainerArguments.AnswerFiles);
                    ExecutionResult executionResult = new ExecutionResult();

                    if (submissionFileResult.ExitCode == 0)
                    {
                        autoContainerArguments.SubmissionFileResult = submissionFileResult.Output;
                    }
                    else
                    {
                        autoContainerArguments.SubmissionFileResult = submissionFileResult.Error;
                        autoContainerArguments.Correction = 2;
                    }
                    if (answerFileResult.ExitCode == 0)
                    {
                        autoContainerArguments.AnswerFileResult = answerFileResult.Output;
                    }
                    else
                    {
                        autoContainerArguments.AnswerFileResult = answerFileResult.Error;
                        autoContainerArguments.Correction = 2;
                    }

                    if (autoContainerArguments.Correction != 2)
                    {
                        if (autoContainerArguments.MatchType == 1)
                        {
                            autoContainerArguments.Correction
                                = comparison.CompareFilesExactMatch(autoContainerArguments.SubmissionFileResult, autoContainerArguments.AnswerFileResult);
                        }
                        else if (autoContainerArguments.MatchType == 2)
                        {
                            autoContainerArguments.Correction
                                = comparison.CompareFilesMatch(autoContainerArguments.SubmissionFileResult, autoContainerArguments.AnswerFileResult);
                        }
                    }

                    executionResult.SubmissionFile = autoContainerArguments.SubmissionFileResult;
                    executionResult.AnswerFile = autoContainerArguments.AnswerFileResult;
                    executionResult.Correction = autoContainerArguments.Correction;

                    await server.RequestStream.WriteAsync(executionResult);
                    // Console.WriteLine("-----end-----\n");
                }
                await server.RequestStream.CompleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return;
        }
        public async Task ManualExecAsync(int containerCount)
        {
            for (int i = 0; i < containerCount; i++)
            {
                ManualExecList.Add(new Docker.DockerClient(channel).ManualRequest(new Empty()));
            }
            for (int i = 0; i < ManualExecList.Count; i++)
            {
                _ = ManualExecAsync(ManualExecList[i], i);
            }

            while (true) { await Task.Delay(10000); }
        }
        public async Task ManualExecAsync(AsyncServerStreamingCall<SubmissionInformation> server, int i)
        {
            try
            {
                if (server == null)
                {
                    return;
                }
                while (await server.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    ManualContainerArguments manualContainerArguments = new ManualContainerArguments(server.ResponseStream.Current, i);

                    manualContainerArguments.ManualExecStream = new Docker.DockerClient(channel).ManualExecStream();

                    manualContainerArguments.IsSend = true;
                    await manualContainerArguments.RequestWriteAsync(manualContainerArguments.Hash);
                    manualContainerArguments.IsSend = false;
                    while (!manualContainerArguments.IsEndConnect)
                    {
                        StandardCmd result = new StandardCmd();
                        Command manualExec = new Command(manualContainerArguments.ContainerName, manualContainerArguments.Lang, manualContainerArguments.RequestWriteAsync, manualContainerArguments.SetStreamWriter);
                        int retryCount = 0;
                        for (; retryCount < 5; retryCount++)
                        {
                            result = await manualExec.ManualExecAsync(manualContainerArguments.SubmissionFiles, manualContainerArguments.IsAvailableExtension, manualContainerArguments.IsZip);
                            if (result.ExitCode == 0)
                            {
                                break;
                            }
                            manualContainerArguments.IsSend = true;
                            await manualContainerArguments.RequestWriteAsync("コンテナ作成に失敗しましたリトライします。\n");
                            manualContainerArguments.IsSend = false;
                            await manualExec.DiscardContainerAsync();
                            await Task.Delay(100);
                        }
                        if (4 <= retryCount)
                        {
                            manualContainerArguments.IsSend = true;
                            await manualContainerArguments.RequestWriteAsync("接続できません: " + result.Error + "\n");
                            manualContainerArguments.IsSend = false;
                            manualContainerArguments.IsEndConnect = true;
                            await manualContainerArguments.ManualExecStream.RequestStream.CompleteAsync();
                            break;
                        }
                        manualContainerArguments.IsSend = true;
                        await manualContainerArguments.RequestWriteAsync("コンテナに接続できました。\n");
                        if (manualContainerArguments.ShowWarning() != "")
                        {
                            await manualContainerArguments.RequestWriteAsync(manualContainerArguments.ShowWarning());
                        }
                        manualContainerArguments.IsSend = false;
                        string workingDirectory = (await manualExec.CurrentDirectoryContainerAsync()).Output.Trim();
                        manualContainerArguments.IsSend = true;
                        await manualContainerArguments.RequestWriteAsync("[" + workingDirectory + "]# ");
                        StandardCmd executeResult = new();
                        Task<StandardCmd> task;
                        while (await manualContainerArguments.ManualExecStream.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                        {
                            var manualExecStreamResponse = manualContainerArguments.ManualExecStream.ResponseStream.Current;
                            string executionInputStr = manualExecStreamResponse.ExecutionInputStr.ToString();
                            if (executionInputStr.ToLower() == "-exit")
                            {
                                manualContainerArguments.IsSend = false;
                                manualContainerArguments.IsEndConnect = true;
                                await manualContainerArguments.ManualExecStream.RequestStream.CompleteAsync();
                                break;
                            }
                            else if (executionInputStr.ToLower() == "-reboot")
                            {
                                manualContainerArguments.IsSend = false;
                                break;
                            }
                            else if (executionInputStr.ToLower().Contains("-tab"))
                            {
                                manualContainerArguments.IsSend = false;
                                string str = await manualExec.TabCompletionAsync(executionInputStr);
                                manualContainerArguments.IsSend = true;
                                await manualContainerArguments.RequestWriteAsync(str);
                                continue;
                            }
                            if (manualContainerArguments.IsDoProcess)
                            {
                                while (manualContainerArguments.InputStream == null) { await Task.Delay(10); }
                                if (manualContainerArguments.InputStream == null)
                                {
                                    return;
                                }
                                manualContainerArguments.InputStream.WriteLine(executionInputStr);
                            }
                            else
                            {
                                task = manualExec.CmdContainerAsync(executionInputStr);
                                _ = Task.Run(async () =>
                                {
                                    StandardCmd cmdContainerResult = await task;
                                    if (cmdContainerResult.ExitCode != 0)
                                    {
                                        await manualContainerArguments.RequestWriteAsync(cmdContainerResult.Error);
                                    }
                                    manualContainerArguments.IsDoProcess = false;
                                    manualContainerArguments.IsSend = false;
                                    string workingDirectory = (await manualExec.CurrentDirectoryContainerAsync()).Output.Trim();
                                    manualContainerArguments.IsSend = true;
                                    await manualContainerArguments.RequestWriteAsync("[" + workingDirectory + "]# ");
                                    return;
                                });
                                manualContainerArguments.IsDoProcess = true;
                            }
                        }
                        await manualExec.DiscardContainerAsync();
                    }
                    // Console.WriteLine("-----end-----\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return;
        }
    }
}