using System;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer.Models
{
    public class ManualExecuteContainer
    {
        public ManualExecuteContainer(){
            ExecutionOutputStr = "error";
        }
        public ManualExecuteContainer(IAsyncStreamReader<ExecutionOutput> req, IServerStreamWriter<ExecutionInput> res, bool isEnd)
        {
            Req = req;
            Res = res;
            IsEnd = isEnd;
            ExecutionOutputStr = "";
        }
        public IAsyncStreamReader<ExecutionOutput>? Req { get; set; }
        public IServerStreamWriter<ExecutionInput>? Res { get; set; }
        public bool IsEnd { get; set; }
        public string ExecutionOutputStr { get; set; }
    }
}