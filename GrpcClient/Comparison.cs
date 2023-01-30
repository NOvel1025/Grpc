using GrpcClient;
using System;
using System.Text.RegularExpressions;

namespace GrpcClient
{
    class Comparison
    {
        public bool CompareFilesMatch(string submissionFile, string answerFile){
            Regex reg = new Regex("[\\s]");
            string submissionResult = reg.Replace(submissionFile, "");
            string answerFileResult = reg.Replace(answerFile, "");
            return Compare(submissionResult, answerFileResult);
        }
        public bool CompareFilesExactMatch(string submissionFile, string answerFile)
        {
            return Compare(submissionFile, answerFile);
        }
        private bool Compare(string submissionFile, string answerFile){
            diff_match_patch dmp = new();
            List<Diff> diffList = dmp.diff_main(submissionFile, answerFile);
            foreach (Diff diff in diffList)
            {
                if (diff.operation.ToString() == "EQUAL")
                {
                    continue;
                }

                return false;

            }
            return true;
        }
    }
}