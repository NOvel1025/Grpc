using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcEClientManual;
using System.Security.Cryptography;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ManualExecute manualExecute = new ManualExecute();
        SubmissionInformation executeFile = new SubmissionInformation();
        executeFile.SubmissionFiles.Add( new FileInformation
        {
            FileName = "HelloWorld.java",
            FileId = "51"
        });
        executeFile.SubmissionFiles.Add(new FileInformation
        {
            FileName = "test.txt",
            FileId = "52"
        });
        executeFile.Lang = "java11";
        string className = "Program";
        Action<string> writeResult = ((string str)=>{
            Console.Write(str);
        });
        Func<string> readInput = (()=>{
            // Console.Write(className + ":");
            return Console.ReadLine();
        });
        await manualExecute.manualExec(executeFile, writeResult, readInput);
        // while(true){
        //     await Task.Delay(5000);
        //     Console.WriteLine("-----1-----");
        // }
    }
}