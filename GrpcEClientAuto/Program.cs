using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcEClientAuto;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        var channel = GrpcChannel.ForAddress("https://localhost:7107",
            new GrpcChannelOptions { HttpHandler = httpHandler });
        var docker = new Docker.DockerClient(channel);
        SubmissionInformation SubmissionInformation = new SubmissionInformation();
        SubmissionInformation.SubmissionFiles.Add(new FileInformation{FileName = "HelloWorld.zip", FileId = "99"});
        SubmissionInformation.AnswerFiles.Add(new FileInformation{FileName = "HelloWorld.zip", FileId = "99"});
        SubmissionInformation.Lang = "clang";
        SubmissionInformation.MatchType = 1;
        SubmissionInformation.InputStr = "22\r\n30\r\n5\r\n8\r\n7";
        // Console.Write("ファイル名>");
        // string fileName = Console.ReadLine();
        // if (_fileName.ToLower().Equals("end")) { break; }
        // Console.Write("ファイルID>");
        // string fileId = Console.ReadLine();
        // if (fileId.ToLower().Equals("end")) { break; }
        // Console.Write("言語>");
        // string lang = Console.ReadLine();
        // if (lang.ToLower().Equals("end")) { break; }
        // if (fileName == null || fileId == null || lang == null)
        // {
        //     fileName = "";
        //     fileId = "";
        //     lang = "";
        // }
        List<Task> taskList = new List<Task>();
        // for(int i = 0; i < 6; i++){
        //     Console.WriteLine("-----接続中-----");
        //     await Task.Delay(1000);
        //     var task = Task.Run(async () => {
        //         var AutoExec = docker.AutoExec(new GrpcEClient.FileInfomation { FileName = fileName, FileId = fileId, Lang = lang, MatchType = 0 });
        //         Console.WriteLine("docker res: " + AutoExec.SubmissionFile);
        //     });
        //     taskList.Add(task);
        // }
        // Task.WhenAll(taskList);

        for (int i = 0; i < 1; i++)
        {
            await Task.Delay(2000);
            var task = Task.Run(async () =>
            {
                // Console.WriteLine("-----接続中-----");
                var AutoExec = new Docker.DockerClient(channel).AutoExec( SubmissionInformation );
                // Console.WriteLine(x + "docker res: " + AutoExec.SubmissionFile);
                Console.WriteLine("docker res\n" + AutoExec.SubmissionFile + "\n" + AutoExec.AnswerFile.ToString() + "\n" + AutoExec.Correction);
            });
            taskList.Add(task);
        }
        await Task.WhenAll(taskList);

        // var AutoExec = docker.AutoExec(new GrpcEClient.FileInfomation { FileName = fileName, FileId = fileId, Lang = lang, MatchType = 0 });
        // Console.WriteLine("docker res: " + AutoExec.SubmissionFile);
        Console.WriteLine("Press any key to exit...");
        // Console.ReadKey();
    }
}