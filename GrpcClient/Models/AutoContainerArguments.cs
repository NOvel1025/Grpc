namespace GrpcClient.Models
{
    class AutoContainerArguments
    {
        public AutoContainerArguments(SubmissionInformation submissionInformation, int i)
        {
            SubmissionFiles = submissionInformation.SubmissionFiles.ToArray();
            AnswerFiles = submissionInformation.AnswerFiles.ToArray();
            Lang = submissionInformation.Lang.ToString();
            MatchType = submissionInformation.MatchType;
            InputStr = submissionInformation.InputStr.ToString();
            SubmissionFileCount = SubmissionFiles.Length;
            AnswerFileCount = AnswerFiles.Length;
            ContainerName = "auto" + Lang + i;
            SubmissionFileResult = "";
            AnswerFileResult = "";
            AvailableLanguages = new string[]{
                "java11", "java17", "clang", "php", "python3"
            };
            Correction = 0;
            CorrectionBool = false;
            foreach (FileInformation fileInformation in SubmissionFiles)
            {
                if (fileInformation.FileName.ToLower().Contains("zip"))
                {
                    IsZip = true;
                    break;
                }
                else
                {
                    IsZip = false;
                }
            }
        }
        public FileInformation[] SubmissionFiles { get; set; }
        public FileInformation[] AnswerFiles { get; set; }
        public string Lang { get; set; }
        public int MatchType { get; set; }
        public string InputStr { get; set; }
        public int SubmissionFileCount { get; set; }
        public int AnswerFileCount { get; set; }
        public string ContainerName { get; set; }
        public string SubmissionFileResult { get; set; }
        public string AnswerFileResult { get; set; }
        public string[] AvailableLanguages { get; set; }
        public int Correction { get; set; }
        public bool CorrectionBool { get; set; }
        public bool IsZip { get; set; }
    }
}