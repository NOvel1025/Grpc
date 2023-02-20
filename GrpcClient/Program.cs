using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcClient;
using GrpcClient.Models;

namespace GrpClient
{
    internal class Program
    {

        private static async Task Main(string[] args)
        {
            //クライアントの個数
            int autoContainerCount = 20;
            int manualContainerCount = 6;
            ExecContainer execContainer = new();
            var autoExecuteTask = Task.Run(async () =>
            {
                await execContainer.AutoExecClientAsync(autoContainerCount);
            });
            var manualExecuteTask = Task.Run(async () =>
            {
                await execContainer.ManualExecAsync(manualContainerCount);
            });
            List<Task> taskList = new List<Task>();
            taskList.Add(autoExecuteTask);
            taskList.Add(manualExecuteTask);
            await Task.WhenAll(taskList);
            // Console.WriteLine("done");
        }
    }
}
