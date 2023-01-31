using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcEClientManual;
using System.Security.Cryptography;

namespace GrpcEClientManual
{
    class ManualExecute
    {
        public async Task manualExec(SubmissionInformation executeFile, Action<string> writeResult, Func<string> readInput)
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("https://localhost:7107",
                new GrpcChannelOptions { HttpHandler = httpHandler });
            var docker = new Docker.DockerClient(channel);

            executeFile.InputStr = GetRandomPassword(20);
            Console.WriteLine(executeFile.InputStr);
            var manualExec = docker.ManualEClientStream();
            manualExec.RequestStream.WriteAsync(executeFile);
            string str = "";
            _ = Task.Run(async () =>
            {
                while (await manualExec.ResponseStream.MoveNext(cancellationToken: CancellationToken.None))
                {
                    var value = manualExec.ResponseStream.Current;
                    writeResult(value.ExecutionOutputStr);
                }
            });
            while ((str = Console.ReadLine()) != null)
            {
                await manualExec.RequestStream.WriteAsync(new SubmissionInformation { InputStr = str });
                if (str.ToLower() == "-exit")
                {
                    await manualExec.RequestStream.CompleteAsync();
                    break;
                }
            }
            manualExec.RequestStream.CompleteAsync();
            Console.WriteLine("Press any key to exit...");
        }
        public static string GetRandomPassword(int length)
        {
            byte[] rgb = new byte[length];
            RNGCryptoServiceProvider rngCrypt = new RNGCryptoServiceProvider();
            rngCrypt.GetBytes(rgb);
            return Convert.ToBase64String(rgb);
        }
    }
}