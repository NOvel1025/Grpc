using System.Diagnostics;
using System;
using GrpcClient.Models;
using Con = System.Console;
using System.Text;
using System.IO;
using Grpc.Core;
using Grpc.Net.Client;
using Hnx8.ReadJEnc;

namespace GrpcClient
{
    public class Command
    {
        private string containerName = "";
        private string lang = "";
        private bool isAuto = false;
        private string inputStr = "";
        private string path = "/opt/data";
        private Func<string, Task> requestWriteAsync;
        private Action<StreamWriter> setStreamWriter;
        private string[] notCompileLanguage = new string[]{
            "php", "python3"
        };
        public Command() { }
        public Command(string containerName, string lang)
        {
            this.containerName = containerName;
            this.lang = lang;
        }
        public Command(string containerName, string lang, bool isAuto, string inputStr)
        {
            this.containerName = containerName;
            this.lang = lang;
            this.isAuto = isAuto;
            this.inputStr = inputStr;
        }
        public Command(string containerName, string lang, Func<string, Task> requestWriteAsync, Action<StreamWriter> setStreamWriter)
        {
            this.requestWriteAsync = requestWriteAsync;
            this.setStreamWriter = setStreamWriter;
            this.containerName = containerName;
            this.lang = lang;
        }
        public async Task<StandardCmd> AutoExecAsync(FileInformation[] fileInformations)
        {
            StandardCmd result = new();
            string mainFile = "";
            string downloadPath = "./executeFiles/" + containerName + "/data";
            for (int i = 0; i < 5; i++)
            {
                if ((result = await BuildExecuteEnvironmentAsync()).ExitCode == 0)
                {
                    foreach (FileInformation fileInformation in fileInformations)
                    {
                        string downloadFilePath = "./executeFiles/" + containerName + "/data/" + fileInformation.FileName;
                        // await DownloadFileAsync("http://10.40.112.110:5556/api/GrpcDownload/"
                        // + fileInformation.FileId + "?apiKey=grpcdesu", downloadPath);
                        if (fileInformation.FileName.ToLower().Contains("zip"))
                        {
                            if ((result = await UnzipAsync(downloadFilePath)).ExitCode == 0)
                            {
                                mainFile = Path.GetFileName(await JudgeMainFileUnzipAsync(Path.GetDirectoryName(downloadFilePath)));
                            }
                        }
                        else if (JudgeMainFile(downloadFilePath))
                        {
                            mainFile = fileInformation.FileName;
                        }
                    }
                    Console.WriteLine("------" + mainFile + "-----");
                    if (notCompileLanguage.Any(value => value == lang))
                    {
                        mainFile = fileInformations[0].FileName;
                    }
                    else
                    {
                        if (mainFile != "")
                        {
                            if (lang == "clang")
                            {
                                result = await CompileClangAsync(downloadPath, mainFile);
                            }
                            if (lang == "java11")
                            {
                                result = await CompileJavaAsync(mainFile);
                            }
                        }
                    }
                    if (result.ExitCode == 0 && mainFile != "")
                    {
                        result = await ExecuteFileAsync(mainFile);
                    }
                    else
                    {
                        result = new StandardCmd("File not found.", "File not found.", -1);
                    }
                    break;
                }
                await DiscardContainerAsync();
                await Task.Delay(100);
            }
            await DiscardContainerAsync();

            return result;
        }
        public async Task<StandardCmd> ManualExecAsync(FileInformation[] fileInformations)
        {

            StandardCmd result = new();
            string mainFile = "";
            bool isZip = false;
            string downloadPath = "./executeFiles/" + containerName + "/data";
            if ((result = await BuildExecuteEnvironmentAsync()).ExitCode == 0)
            {
                foreach (FileInformation fileInformation in fileInformations)
                {
                    string downloadFilePath = "./executeFiles/" + containerName + "/data/" + fileInformation.FileName;
                    await DownloadFileAsync("http://10.40.112.110:5556/api/GrpcDownload/"
                        + fileInformation.FileId + "?apiKey=grpcdesu", downloadFilePath);
                    if (fileInformation.FileName.ToLower().Contains("zip"))
                    {
                        isZip = true;
                        if ((result = await UnzipAsync(fileInformation.FileName)).ExitCode == 0)
                        {
                            mainFile = await JudgeMainFileUnzipAsync("./executeFiles/" + containerName + "/data/");
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        if (JudgeMainFile(downloadFilePath))
                        {
                            mainFile = fileInformation.FileName;
                        }
                    }
                }
                if (notCompileLanguage.Any(value => value == lang))
                {
                    mainFile = fileInformations[0].FileName;
                }
                else
                {
                    if (isZip && mainFile == "") { }
                    else
                    {
                        if (mainFile != "")
                        {
                            if (lang == "clang")
                            {
                                result = await CompileClangAsync(downloadPath, mainFile);
                            }
                            if (lang == "java11")
                            {
                                result = await CompileJavaAsync(mainFile);
                            }
                        }
                        else
                        {
                            result = new StandardCmd("File not found.", "File not found.", -1);
                        }
                    }
                }
            }
            return result;
        }
        public async Task<StandardCmd> BuildExecuteEnvironmentAsync()
        {
            StandardCmd mkdirResult = await MkdirExecuteFoldersAsync();
            if (mkdirResult.ExitCode != 0)
            {
                return mkdirResult;
            }
            StandardCmd cpResult = await CpExecuteFoldersAsync();
            if (cpResult.ExitCode != 0)
            {
                return cpResult;
            }
            StandardCmd buildResult = await BuildContainerAsync();
            if (buildResult.ExitCode != 0)
            {
                return buildResult;
            }
            return buildResult;
        }
        public async Task DiscardContainerAsync()
        {
            StandardCmd rmContainerFiles = await RmContainerFilesAsync();
            StandardCmd stopResult = await StopContainerAsync();
            //自動でコンテナは削除される
            // StandardCmd RmContainerResult = await RmContainerAsync();
            StandardCmd rmFolderResult = await RmExecuteFoldersAsync();
            return;
        }
        public async Task<StandardCmd> BuildContainerAsync()
        {
            string build = "./bashfile/container.sh " + lang + " " + containerName;
            return await ExecuteAsync(build);
        }
        public async Task<StandardCmd> StopContainerAsync()
        {
            string cmdStr = "-c \"docker stop -t 0 " + containerName + " \"";
            return await ExecuteAsync(cmdStr);
        }
        public async Task<StandardCmd> RmContainerAsync()
        {
            string cmdStr = "-c \"docker rm " + containerName + " \"";
            return await ExecuteAsync(cmdStr);
        }
        public async Task<StandardCmd> MkdirExecuteFoldersAsync()
        {
            string mkdir = "./bashfile/mkdirExecuteFolder.sh " + containerName;
            return await ExecuteAsync(mkdir);
        }
        public async Task<StandardCmd> CpExecuteFoldersAsync()
        {
            string cp = "./bashfile/cpExecuteFolder.sh " + lang + " " + containerName;
            return await ExecuteAsync(cp);
        }
        public async Task<StandardCmd> RmContainerFilesAsync()
        {
            string rmFiles = "-c \"docker exec -i -w /opt/data " + containerName + " bash -c 'rm -fR *'\"";
            return await ExecuteAsync(rmFiles);
        }
        public async Task<StandardCmd> RmExecuteFoldersAsync()
        {
            string rmFolder = "./bashfile/rmExecuteFolder.sh " + containerName;
            return await ExecuteAsync(rmFolder);
        }
        public async Task<StandardCmd> CmdContainerAsync(string line)
        {
            if (line.Contains("cd"))
            {
                return await CdAsync(line);
            }
            else
            {
                string cmdStr = "-c \"docker exec -i -w " + path + " " + containerName + " bash -c '" + line + "'\"";
                return await ExecuteAsync(cmdStr);
            }
        }
        public async Task<StandardCmd> CdAsync(string line)
        {
            string cmdStr = "-c \"docker exec -i -w " + path + " " + containerName + " bash -c '" + line + " && pwd'\"";
            StandardCmd standardCmd = await ExecuteAsync(cmdStr);
            if (standardCmd.ExitCode == 0)
            {
                path = standardCmd.Output.Trim();
            }
            return standardCmd;

        }
        public async Task<StandardCmd> CurrentDirectoryContainerAsync()
        {
            string pwd = "-c \"docker exec -i -w " + path + " " + containerName + " bash -c 'basename `pwd`'\"";
            StandardCmd pwdResult = await ExecuteAsync(pwd);
            return pwdResult;
        }
        public async Task<StandardCmd> UnzipAsync(string downLoadPath)
        {
            string unzipPath = Path.GetDirectoryName(downLoadPath);
            string unzip = "-c \"unzip -d " + unzipPath + " " + downLoadPath + "\"";
            StandardCmd unzipResult = await ExecuteAsync(unzip);
            if (unzipResult.ExitCode != 0)
            {
                return unzipResult;
            }
            string rmzip = "-c \"rm -fR " + downLoadPath + "\"";
            StandardCmd rmzipResult = await ExecuteAsync(rmzip);
            if (rmzipResult.ExitCode != 0)
            {
                return rmzipResult;
            }
            return rmzipResult;
        }
        public async Task<StandardCmd> RmAsync(string fileName)
        {
            string cmdStr = "-c \"docker exec -i -w /opt/data " + containerName + " bash -c 'rm -Rf " + fileName + "\"";
            return await ExecuteAsync(cmdStr);
        }
        public async Task<StandardCmd> CompileJavaAsync(string fileName)
        {
            string downloadPath = "./executeFiles/" + containerName + "/data/" + fileName;
            string encode = await judgeEncodeAsync(downloadPath);
            string className = Path.GetFileNameWithoutExtension(fileName);
            if (encode.ToLower().Contains("utf-8"))
            {
                string compile = "-c \"docker exec -w /opt/bin " + containerName + " bash compile.sh " + className + "\"";
                StandardCmd compileResult = await ExecuteAsync(compile);
                if (compileResult.ExitCode == 0)
                {
                    return compileResult;
                }
            }
            if (encode.ToLower().Contains("sjis"))
            {
                string compileSjis = "-c \"docker exec -w /opt/bin " + containerName + " bash compile_sjis.sh " + className + "\"";
                StandardCmd compileSjisResult = await ExecuteAsync(compileSjis);
                if (compileSjisResult.ExitCode == 0)
                {
                    return compileSjisResult;
                }
            }
            return new StandardCmd("compile error", "compile error", -1);
        }
        public async Task<StandardCmd> CompileClangAsync(string downloadPath, string mainFile)
        {
            string encode = await judgeEncodeAsync(downloadPath + "/" + mainFile);
            if (encode.ToLower().Contains("utf-8"))
            {
                string compile = "-c \"docker exec -w /opt/bin " + containerName + " bash compile.sh ";
                compile = GenerateCompileString(downloadPath, mainFile, compile);
                StandardCmd compileResult = await ExecuteAsync(compile);
                if (compileResult.ExitCode == 0)
                {
                    return compileResult;
                }
            }
            if (encode.ToLower().Contains("sjis"))
            {
                string compileSjis = "-c \"docker exec -w /opt/bin " + containerName + " bash compile_sjis.sh ";
                compileSjis = GenerateCompileString(downloadPath, mainFile, compileSjis);
                StandardCmd compileSjisResult = await ExecuteAsync(compileSjis);
                if (compileSjisResult.ExitCode == 0)
                {
                    return compileSjisResult;
                }
            }
            return new StandardCmd("compile error", "compile error", -1);
        }
        public async Task<StandardCmd> ExecuteFileAsync(string fileName)
        {
            string mainFilePath = Path.GetDirectoryName(fileName) == "" ? "" : "/" + Path.GetDirectoryName(fileName);
            string className = Path.GetFileNameWithoutExtension(fileName);
            string cmdStr = "-c \"docker exec -i -w /opt/bin" + mainFilePath + " " + containerName + " bash -c 'bash execute.sh " + className + "'\"";
            return await ExecuteAsync(cmdStr);
        }
        private async Task<StandardCmd> ExecuteAsync(string cmdStr)
        {
            bool isTimeOut = true;
            Process? process = new();
            var task = Task.Run(async () =>
            {
                ProcessStartInfo p1 = new ProcessStartInfo("bash", cmdStr);

                Console.WriteLine("bash " + cmdStr);

                p1.RedirectStandardOutput = true;
                p1.RedirectStandardError = true;
                p1.RedirectStandardInput = true;
                p1.UseShellExecute = false;
                process = Process.Start(p1);

                if (process == null)
                {
                    return new StandardCmd();
                }

                string str = "";

                if (isAuto)
                {
                    str = await ProcessInOutAutoAsync(process);
                }
                else
                {
                    str = await ProcessInOutAsync(process);
                }


                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                int exitCode = process.ExitCode;

                process.Close();

                Console.WriteLine("output:" + output);
                Console.WriteLine("error:" + error);
                Console.WriteLine("exitCode:" + exitCode);

                return new StandardCmd(str, error, exitCode);
            });
            return await task;
        }
        //手動実行用の実行プロセス
        private async Task<string> ProcessInOutAsync(Process process)
        {
            StreamWriter sw = process.StandardInput;
            StreamReader sr = process.StandardOutput;
            string str = "";
            try
            {
                setStreamWriter(sw);
                int ch;
                bool isTimeOut = true;

                var task = Task.Run(async () =>
                {
                    str = await PrintOutAsync(process);
                    isTimeOut = false;

                });
                int i = 0;
                while (i++ < 6000 && isTimeOut)
                {
                    await Task.Delay(10);
                }
                if (6000 <= i)
                {
                    process.Kill();
                    return "this is time out";
                }
            }
            finally
            {
                // sr.Close();
                // sw.Close();
            }

            return str;
        }
        private async Task<string> ProcessInOutAutoAsync(Process process)
        {
            string str = "";
            string[] strIn = inputStr.Split("\n");
            string strOut = "";
            int ch = -1;
            int i = 0;
            bool isTimeOut = true;

            var task = Task.Run(async () =>
            {
                StreamWriter sw = process.StandardInput;
                StreamReader sr = process.StandardOutput;
                try
                {
                    bool isFirst = true;
                    while (true)
                    {
                        var cts = new CancellationTokenSource();
                        CancellationToken token = cts.Token;
                        //非同期でインプットストリームへ書き込み、タイムアウトで書き込みキャンセル
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                if (isFirst)
                                {
                                    for (int i = 0; i < 200; i++)
                                    {
                                        await Task.Delay(1);
                                        if (token.IsCancellationRequested)
                                        {
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    //タイムアウトを1msごとに監視tokenがキャンセルになったら次へ
                                    for (int i = 0; i < 50; i++)
                                    {
                                        await Task.Delay(1);
                                        if (token.IsCancellationRequested)
                                        {
                                            return;
                                        }
                                    }
                                }


                                for (int i = 0; i < strIn.Length; i++)
                                {
                                    strIn[i] = strIn[i].Trim();
                                }

                                if (i < strIn.Length)
                                {
                                    if (strIn[i] == null)
                                    {
                                        return;
                                    }
                                    if (strIn[i] != null)
                                    {
                                        sw.WriteLine(strIn[i]);
                                        Console.WriteLine(strIn[i]);
                                        str += strIn[i] + "\n";
                                    }
                                    i++;
                                }
                            }
                            catch (NullReferenceException ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }

                            return;
                        });
                        //一文字取り出してファイルの最後じゃなかったら次へ
                        if ((ch = sr.Read()) != -1)
                        {
                            str += (char)ch;
                            Console.Write((char)ch);
                            cts.Cancel();
                        }
                        else
                        {
                            break;
                        }
                        isFirst = false;
                    }
                    isTimeOut = false;
                    return str;
                }
                finally
                {
                    // sr.Close();
                    // sw.Close();
                }
            });
            int j = 0;
            while (j++ < 1000 && isTimeOut)
            {
                await Task.Delay(10);
            }
            if (1000 <= j)
            {
                process.Kill();
                return "this is time out";
            }
            return await task;
        }
        private async Task<string> PrintOutAsync(Process process)
        {
            int ch;
            string str = "";
            StreamReader sr = process.StandardOutput;
            try
            {
                while ((ch = sr.Read()) != -1)
                {
                    str = str + (char)ch;
                    // Console.Write((char)ch);
                    await Task.Delay(1);
                    requestWriteAsync(((char)ch).ToString());
                }
            }
            finally
            {
                // sr.Close();
            }

            return str;
        }
        //
        private async Task DownloadFileAsync(string url, string downloadPath)
        {
            Console.WriteLine(url);
            Console.WriteLine(downloadPath);
            //画像をダウンロード
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            //ステータスコードで成功したかチェック
            if (response.StatusCode != System.Net.HttpStatusCode.OK) return;

            //画像を保存
            using var stream = await response.Content.ReadAsStreamAsync();
            using var outStream = File.Create(downloadPath);
            stream.CopyTo(outStream);
        }
        private async Task<string> judgeEncodeAsync(string downloadPath)
        {
            try
            {

                FileInfo file = new FileInfo(downloadPath);
                string name = "";
                // ファイル自動判別読み出しクラスを生成
                using (Hnx8.ReadJEnc.FileReader reader = new FileReader(file))
                {
                    // 判別読み出し実行。判別結果はReadメソッドの戻り値で把握できます
                    try
                    {
                        Hnx8.ReadJEnc.CharCode c = reader.Read(file);
                        // 戻り値のNameプロパティから文字コード名を取得できます
                        name = c.Name;
                    }
                    catch (System.NotSupportedException)
                    {
                        name = "sjis";
                    }
                }
                return name;
            }
            catch (Exception ex)
            {
                return "not Found File";
            }
        }
        private bool JudgeMainFile(string downloadPath)
        {
            StreamReader sr = new StreamReader(downloadPath);
            try
            {
                if (lang == "java11")
                {
                    if (sr.ReadToEnd().ToLower().Contains("void main"))
                    {
                        return true;
                    }
                }
                if (lang == "clang")
                {
                    if (sr.ReadToEnd().ToLower().Contains("main("))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                // sr.Close();
            }
            return false;
        }
        private async Task<string> JudgeMainFileUnzipAsync(string downLoadPath)
        {
            string mainFile = "";

            string[] files = Directory.GetFiles(downLoadPath);
            foreach (string file in files)
            {
                if (JudgeMainFile(file))
                {
                    return file;
                }
            }

            string[] directories = Directory.GetDirectories(downLoadPath);
            foreach (string directory in directories)
            {
                mainFile = await JudgeMainFileUnzipAsync(directory);
            }
            if (mainFile != "" && Path.GetFileName(Path.GetDirectoryName(mainFile)) != "data")
            {
                string mv = "-c \"mv " + Path.GetDirectoryName(mainFile) + "/* " + downLoadPath;
                await ExecuteAsync(mv);
                string rm = "-c \"rm -fR " + Path.GetDirectoryName(mainFile);
                await ExecuteAsync(rm);
            }

            return mainFile;
        }
        private string GenerateCompileString(string downloadPath, string mainFile, string compile)
        {
            List<string> filesPath = GetFilesPath(downloadPath);
            foreach (string filePath in filesPath)
            {
                if (Path.GetFileName(filePath) == mainFile)
                {
                    compile += ("../data/" + Path.GetFileNameWithoutExtension(mainFile) + " ");
                    break;
                }
            }
            foreach(string filePath in filesPath){
                if (Path.GetFileName(filePath) == mainFile)
                {
                    compile += ("../data/" + Path.GetFileName(mainFile) + " ");
                    break;
                }
                else
                {
                    if (Path.GetExtension(filePath) == ".c")
                    {
                        compile += ("../data/" + Path.GetFileName(filePath) + " ");
                    }
                }
            }
            compile.TrimEnd();
            compile += "\"";
            return compile;
        }
        private List<string> GetFilesPath(string path)
        {
            List<string> filePath = new List<string>();
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                filePath.Add(file);
            }
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                foreach(string file in GetFilesPath(directory)){
                    filePath.Add(file);
                }
            }
            return filePath;
        }
    }
}