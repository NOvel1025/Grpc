using System;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer.Models
{
    public class ManualEx2Client
    {
        public ManualEx2Client(IAsyncStreamReader<SubmissionInformation> req, IServerStreamWriter<ExecutionOutput> res, bool available)
        {
            Req = req;
            Res = res;
            Available = available;
            ExecutionOutputStr = "";
        }
        public IAsyncStreamReader<SubmissionInformation>? Req { get; set; }
        public IServerStreamWriter<ExecutionOutput>? Res { get; set; }
        public bool Available { get; set; }
        public string ExecutionOutputStr { get; set; }
    }
}