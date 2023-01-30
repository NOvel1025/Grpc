using System;

namespace GrpcClient.Models{
    public class StandardCmd{
        public StandardCmd(){
            Output = "";
            Error = "";
            ExitCode = 0;
        }
        public StandardCmd(string output, string error, int exitCode){
            Output = output;
            Error  = error;
            ExitCode = exitCode;
        }
        public StandardCmd errorResult(){
            return new StandardCmd("error", "error", -1);
        }
        public string Output {get; set;}
        public string Error {get; set;}
        public int ExitCode {get; set;}
    }
}