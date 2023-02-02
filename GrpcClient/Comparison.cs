using GrpcClient;
using System;
using System.Text.RegularExpressions;

namespace GrpcClient
{
    class Comparison
    {
        public bool CompareFilesMatch(string submissionFile, string answerFile)
        {
            Regex reg = new Regex("[\\s]");
            string submissionResult = reg.Replace(submissionFile, "");
            string answerFileResult = reg.Replace(answerFile, "");
            return Compare(submissionResult, answerFileResult);
        }
        public bool CompareFilesExactMatch(string submissionFile, string answerFile)
        {
            return Compare(submissionFile, answerFile);
        }
        private bool Compare(string submissionFile, string answerFile)
        {
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
        public string FirstMatchString(string[] fileNames)
        {
            diff_match_patch dmp = new();
            string matchString = "";
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                if (matchString == "")
                {
                    List<Diff> diffList = dmp.diff_main(fileNames[i - 1], fileNames[i]);
                    foreach (Diff diff in diffList)
                    {
                        if (diff.operation.ToString() == "EQUAL")
                        {
                            matchString = diff.text;
                            break;
                        }
                    }
                }
                else
                {
                    List<Diff> diffList = dmp.diff_main(fileNames[i], matchString);
                    foreach (Diff diff in diffList)
                    {
                        if (diff.operation.ToString() == "EQUAL")
                        {
                            matchString = diff.text;
                            break;
                        }
                    }
                }
            }

            return matchString;
        }
    }
}