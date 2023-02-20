using Grpc.Core;
using GrpcServer;
using GrpcServer.Models;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer.Services;

public class DockerService : Docker.DockerBase
{
    // private static string _fileName = "";
    // private string _path = "./testData/";
    // private static bool isFile = true;
    // private static bool isEndProcess = true;
    private readonly ILogger<DockerService> _logger;
    private readonly DockerController _con = new DockerController();
    // static bool isEnd = false;    
    public DockerService(ILogger<DockerService> logger)
    {
        _logger = logger;
    }
    public override async Task AutoExecStream(IAsyncStreamReader<ExecutionResult> req, IServerStreamWriter<SubmissionInformation> res, ServerCallContext context)
    {
        
        // Console.WriteLine("-----自動実行接続完了-----");
        _con.ConnectServer(req, res);
        while(true){await Task.Delay(10000);}
    }
    public override async Task<ExecutionResult> AutoExec(SubmissionInformation req, ServerCallContext context)
    {
        ExecutionResult ans = await _con.AutoExecStreamAsync(req);
        ans.SubmissionFile = ans.SubmissionFile;
        return ans;
    }
    public override async Task ManualRequest(Empty req, IServerStreamWriter<SubmissionInformation> res, ServerCallContext context){
        // Console.WriteLine("-----手動実行クライアント接続完了-----");
        _con.ConnectServer(req, res);
        while(true){await Task.Delay(10000);}
    }
    public override async Task ManualExecStream(IAsyncStreamReader<ExecutionOutput> req, IServerStreamWriter<ExecutionInput> res, ServerCallContext context){
        // Console.WriteLine("-----手動実行コンテナ接続完了-----");
        await _con.ConnectServer(req, res);
        // Console.WriteLine("-----手動実行コンテナ接続終了-----");
    }
    public override async Task ManualEClientStream(IAsyncStreamReader<SubmissionInformation> req, IServerStreamWriter<ExecutionOutput> res, ServerCallContext context){
        // Console.WriteLine("-----手動実行Ex2クライアント接続完了-----");
        await _con.ConnectServer(req, res);
        // Console.WriteLine("-----手動実行Ex2クライアント接続終了-----");
    }
}