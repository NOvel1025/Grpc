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
            int containerCount = 3;
            ExecContainer execContainer = new();
            var task = Task.Run(async () =>
            {
                await execContainer.AutoExecClientAsync(containerCount);
            });
            var task2 = Task.Run(async () =>
            {
                await execContainer.ManualExecAsync(containerCount);
            });
            List<Task> taskList = new List<Task>();
            taskList.Add(task);
            taskList.Add(task2);
            await Task.WhenAll(taskList);
            Console.WriteLine("done");
        }
    }
}
