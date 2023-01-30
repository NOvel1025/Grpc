using System;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer.Models
{
    public class ManualExecuteRequestClient
    {
        public ManualExecuteRequestClient()
        {
            Available = true;
            IsEnd = true;
        }
        public ManualExecuteRequestClient(IServerStreamWriter<SubmissionInformation> res, bool available, bool isEnd){
            Res = res;
            Available = available;
            IsEnd = isEnd;
        }
        public IServerStreamWriter<SubmissionInformation>? Res { get; set; }
        public bool Available { get; set; }
        public bool IsEnd { get; set; }
    }
}