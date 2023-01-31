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
            if (AvailableLanguages.Any(a => a == Lang))
            {
                IsAvailableLanguage = true;
            }
            else
            {
                IsAvailableLanguage = false;
            }
            
            Correction = 0;
            CorrectionBool = false;
            IsZip = false;
            IsAvailableExtension = false;
            foreach (FileInformation fileInformation in SubmissionFiles)
            {
                string fileName = fileInformation.FileName;
                if(!IsAvailableExtension || !IsZip){
                    switch(Lang){
                        case "java11":
                            if(fileName.ToLower().Contains(".java")){
                                IsAvailableExtension = true;
                            }
                            break;
                        case "clang":
                            if(fileName.ToLower().Contains(".c")){
                                IsAvailableExtension = true;
                            }
                            break;
                        case "php":
                            if(fileName.ToLower().Contains(".php")){
                                IsAvailableExtension = true;
                            }
                            break;
                        case "python3":
                            if(fileName.ToLower().Contains(".py")){
                                IsAvailableExtension = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if(fileName.ToLower().Contains(".zip")){
                        IsZip = true;
                        continue;
                    }
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
        public bool IsAvailableLanguage { get; set; }
        public bool IsAvailableExtension { get; set; }
        public int Correction { get; set; }
        public bool CorrectionBool { get; set; }
        public bool IsZip { get; set; }
    }
}