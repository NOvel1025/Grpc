using System;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer.Models
{
    public class AutoExecuteContainer
    {
        public AutoExecuteContainer(IAsyncStreamReader<ExecutionResult> req, IServerStreamWriter<SubmissionInformation> res, bool available)
        {
            Req = req;
            Res = res;
            Available = available;
            Submission = "";
            Answer = "";
            Correction = 0;
        }
        public IAsyncStreamReader<ExecutionResult>? Req { get; set; }
        public IServerStreamWriter<SubmissionInformation>? Res { get; set; }
        public bool Available { get; set; }
        public string Submission { get; set; }
        public string Answer { get; set; }
        public int Correction { get; set; }
    }
}