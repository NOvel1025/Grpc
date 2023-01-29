using Grpc.Core;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using GrpcServer.Models;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer
{
    public class DockerController
    {
        //自動実行用コンテナのリスト　20個
        private static List<AutoExecuteContainer> _autoExecContainerList = new List<AutoExecuteContainer>();
        //自動実行用ファイルの列
        private static List<SubmissionInformation> _autoExecFileList = new List<SubmissionInformation>();
        //手動実行用のサーバーストリーミングの待ちクライアント　10個
        private static List<ManualExecuteRequestClient> _manualClientList = new List<ManualExecuteRequestClient>();
        //手動実行用コンテナのリスト
        private static List<ManualExecuteContainer> _manualExecContainerList = new List<ManualExecuteContainer>();
        //自動実行用コンテナリストに追加
        public async Task ConnectServer(IAsyncStreamReader<ExecutionResult> req, IServerStreamWriter<SubmissionInformation> res)
        {
            AutoExecuteContainer autoExecuteContainer = new AutoExecuteContainer(req, res, true);
            _autoExecContainerList.Add(autoExecuteContainer);
        }
        //手動実行用のサーバーストリーミングの待ちクライアントリストに追加
        public async Task ConnectServer(Empty req, IServerStreamWriter<SubmissionInformation> res)
        {
            ManualExecuteRequestClient manualExecuteRequestClient = new ManualExecuteRequestClient(res, true, true);
            _manualClientList.Add(manualExecuteRequestClient);
        }
        //手動実行用のコンテナリストに追加
        public async Task ConnectServer(IAsyncStreamReader<ExecutionOutput> req, IServerStreamWriter<ExecutionInput> res)
        {
            ManualExecuteContainer manualExecContainer = new ManualExecuteContainer(req, res, false);
            _manualExecContainerList.Add(manualExecContainer);
            await _manualExecContainerList[_manualExecContainerList.IndexOf(manualExecContainer)].Req.MoveNext();
            var manualExecContainerCurrent = _manualExecContainerList[_manualExecContainerList.IndexOf(manualExecContainer)].Req.Current;
            _manualExecContainerList[_manualExecContainerList.IndexOf(manualExecContainer)].ExecutionOutputStr = manualExecContainerCurrent.ExecutionOutputStr;
            while (!_manualExecContainerList[_manualExecContainerList.IndexOf(manualExecContainer)].IsEnd) { await Task.Delay(1); }
        }
        //手動実行開始
        public async Task ConnectServer(IAsyncStreamReader<SubmissionInformation> req, IServerStreamWriter<ExecutionOutput> res)
        {
            ManualEx2Client manualEClient = new ManualEx2Client(req, res, true);
            await ManualExecution(manualEClient);
        }
        //自動実行用のコンテナにファイルを送信、実行結果を取得　引数；実行ファイル情報　戻り値；実行結果
        public async Task<ExecutionResult> AutoExecStreamAsync(SubmissionInformation execFile)
        {
            _autoExecFileList.Add(execFile);

            while ((!IsAvailableAutoContainer() || _autoExecFileList[0] != execFile)) { await Task.Delay(10); }
            _autoExecFileList.Remove(execFile);
            foreach (AutoExecuteContainer cClient in _autoExecContainerList)
            {
                if (execFile == null)
                {
                    return new ExecutionResult { SubmissionFile = "", AnswerFile = "", Correction = 0 };
                }
                if (cClient.Available)
                {
                    cClient.Available = false;
                    Console.WriteLine(execFile.ToString());
                    await cClient.Res.WriteAsync(execFile);
                    if (await cClient.Req.MoveNext())
                    {
                        var cClientRequestCurrent = cClient.Req.Current;
                        string submissionFileResult = cClientRequestCurrent.SubmissionFile.ToString();
                        string answerFileResult = cClientRequestCurrent.AnswerFile.ToString();
                        int correction = cClientRequestCurrent.Correction;
                        ExecutionResult ans =
                            new ExecutionResult { SubmissionFile = submissionFileResult, AnswerFile = answerFileResult, Correction = correction };
                        cClient.Available = true;

                        return ans;
                    }
                }
            }
            return new ExecutionResult { SubmissionFile = "error", AnswerFile = "error", Correction = 2 };
        }
        //手動実行用コンテナを受け取りそこにコマンドを送り、結果を受け取る　引数；EX2側のクライアント　戻り値；実行結果（1文字ずつ）
        public async Task ManualExecution(ManualEx2Client manualEx2Client)
        {
            while (!IsAvailableManualContainer()) { await Task.Delay(10); }
            ManualExecuteContainer manualContainer = new ManualExecuteContainer();

            foreach (ManualExecuteRequestClient manualExecuteRequestClient in _manualClientList)
            {
                if (manualExecuteRequestClient.Available)
                {
                    manualExecuteRequestClient.Available = false;
                    try
                    {
                        await manualEx2Client.Req.MoveNext();
                        var manualEClientReqCurrent = manualEx2Client.Req.Current;
                        string hash = manualEClientReqCurrent.InputStr;
                        await manualExecuteRequestClient.Res.WriteAsync( manualEClientReqCurrent );
                        while (_manualExecContainerList.Count == 0) { await Task.Delay(10); }
                        manualContainer = await ReturnManualExecContainer(hash);
                        var task = Task.Run(async () =>
                        {
                            while (await manualContainer.Req.MoveNext())
                            {
                                var value = manualContainer.Req.Current;
                                Console.Write(value.ExecutionOutputStr);
                                manualEx2Client.Res.WriteAsync(new ExecutionOutput { ExecutionOutputStr = value.ExecutionOutputStr.ToString() });
                            }
                        });
                        while (await manualEx2Client.Req.MoveNext())
                        {
                            manualEClientReqCurrent = manualEx2Client.Req.Current;
                            string executionInputStr = manualEClientReqCurrent.InputStr;
                            await manualContainer.Res.WriteAsync(new ExecutionInput { ExecutionInputStr = executionInputStr });
                        }
                        await task;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    manualContainer.IsEnd = true;
                    await Task.Delay(15);
                    _manualExecContainerList.Remove(manualContainer);
                    manualExecuteRequestClient.Available = true;
                    break;
                }
            }
        }
        //自動実行用コンテナの空きを判定　引数；なし　戻り値；使用可能コンテナ
        public bool IsAvailableAutoContainer()
        {
            foreach (var cClient in _autoExecContainerList)
            {
                if (cClient.Available)
                {
                    return true;
                }
            }
            return false;
        }
        //手動実行用サーバーストリーミングの空きを判定　引数；なし　戻り値；使用可能サーバーストリーミング
        public bool IsAvailableManualContainer()
        {
            foreach (var manualClient in _manualClientList)
            {
                if (manualClient.Available)
                {
                    return true;
                }
            }
            return false;
        }
        // 手動実行用コンテナからのハッシュ値を受け取り送る前のハッシュ値と比較し適当なコンテナを返す　引数；ハッシュ値　戻り値；手動実行用コンテナ
        private async Task<ManualExecuteContainer> ReturnManualExecContainer(string hash)
        {
            int i = 0;
            while (i < 1000)
            {
                foreach (ManualExecuteContainer manualContainer in _manualExecContainerList)
                {
                    if (manualContainer.ExecutionOutputStr == hash)
                    {
                        return manualContainer;
                    }
                }
                await Task.Delay(10);
            }
            return new ManualExecuteContainer();
        }
    }
}