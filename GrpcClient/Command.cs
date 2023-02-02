using System.Diagnostics;
using System;
using GrpcClient.Models;
using Con = System.Console;
using System.Text;
using System.IO;
using Grpc.Core;
using Grpc.Net.Client;
using Hnx8.ReadJEnc;
using System.Text.RegularExpressions;

namespace GrpcClient
{
    public class Command
    {

        /// <summary>
        /// コンテナ名、コンテナを生成するときにautoかmanualに言語、番号をつけて生成
        /// </summary>
        private string _containerName = "";
        /// <summary>
        /// 言語
        /// </summary>
        private string _lang = "";
        /// <summary>
        /// 自動実行用か手動実行用かのフラグ
        /// </summary>
        private bool _isAuto = false;
        /// <summary>
        /// 入力文字列
        /// </summary>
        private string _inputStr = "";
        /// <summary>
        /// 基本ディレクトリのパス
        /// </summary>
        private string _path = "/opt/data";
        /// <summary>
        /// Ex2のダウンロードコントローラのAPI
        /// </summary>
        private string _ex2PlusUrl = "http://10.40.112.110:5556/api/GrpcDownload/";
        /// <summary>
        /// Ex2のダウンロードAPIのキー
        /// </summary>
        private string _apiKey = "?apiKey=grpcdesu";
        private string _generateDirectoryPath = "";
        /// <summary>
        /// 手動実行用のリクエストストリームへの送信のデリゲート
        /// </summary>
        private Func<string, Task>? _requestWriteAsync;
        /// <summary>
        /// ストリームライターに手動実行のプロセスのinputをセットするデリゲート
        /// </summary>
        private Action<StreamWriter>? _setStreamWriter;
        /// <summary>
        /// コンパイルしない言語の列
        /// </summary>
        /// <value></value>
        private string[] _notCompileLanguage = new string[]{
            "php", "python3"
        };
        public Command(string containerName, string lang)
        {
            this._containerName = containerName;
            this._lang = lang;
        }
        /// <summary>
        /// コマンドクラスのコンストラクタ
        /// </summary>
        /// <param name="containerName">コンテナの名前</param>
        /// <param name="lang">言語</param>
        /// <param name="isAuto">自動実行フラグ</param>
        /// <param name="inputStr">入力文字列</param>
        public Command(string containerName, string lang, bool isAuto, string inputStr)
        {
            this._containerName = containerName;
            this._lang = lang;
            this._isAuto = isAuto;
            this._inputStr = inputStr;
        }
        /// <summary>
        /// コマンドクラスのコンストラクタ
        /// </summary>
        /// <param name="containerName">コンテナの名前</param>
        /// <param name="lang">言語</param>
        /// <param name="requestWriteAsync">リクエストストリームの送信用デリゲート</param>
        /// <param name="setStreamWriter">プロセスの入力ストリームをセットするデリゲート</param>
        public Command(string containerName, string lang, Func<string, Task> requestWriteAsync, Action<StreamWriter> setStreamWriter)
        {
            this._requestWriteAsync = requestWriteAsync;
            this._setStreamWriter = setStreamWriter;
            this._containerName = containerName;
            this._lang = lang;
        }
        /// <summary>
        /// 自動実行用プロセス
        /// </summary>
        /// <param name="fileInformations">ファイル情報の列</param>
        /// <returns>自動実行プロセスの実行結果</returns>
        public async Task<StandardCmd> AutoExecAsync(FileInformation[] fileInformations)
        {
            StandardCmd result = new();
            string mainFile = "";
            string directoryPath = "./executeFiles/" + _containerName + "/data";
            for (int i = 0; i < 5; i++)
            {
                if ((result = await BuildExecuteEnvironmentAsync()).ExitCode == 0)
                {
                    foreach (FileInformation fileInformation in fileInformations)
                    {
                        string filePath = directoryPath + "/" + fileInformation.FileName;
                        await DownloadFileAsync(_ex2PlusUrl + fileInformation.FileId + _apiKey, filePath);
                        if (fileInformation.FileName.ToLower().Contains("zip"))
                        {
                            if ((result = await UnzipAsync(filePath)).ExitCode == 0)
                            {
                                mainFile = Path.GetFileName(await JudgeUnzipMainFileAsync(directoryPath));
                            }
                        }
                        else if (JudgeMainFile(filePath))
                        {
                            mainFile = fileInformation.FileName;
                        }
                    }
                    if (_notCompileLanguage.Any(value => value == _lang) && mainFile == "")
                    {
                        if ((mainFile = JudgeMainFile(fileInformations)) == "")
                        {
                            return new StandardCmd("File not found.", "File not found.", -1);
                        }
                    }
                    else
                    {
                        if (mainFile != "")
                        {
                            if (_lang == "clang")
                            {
                                result = await CompileClangAsync(directoryPath, mainFile);
                            }
                            if (_lang == "java11")
                            {
                                result = await CompileJavaAsync(directoryPath, mainFile);
                            }
                        }
                    }
                    if (result.ExitCode == 0 && mainFile != "")
                    {
                        if (mainFile != "")
                        {
                            result = await ExecuteFileAsync(mainFile);
                        }
                        else
                        {
                            result = new StandardCmd("File not found.", "File not found.", -1);
                        }
                    }

                    break;
                }
                await DiscardContainerAsync();
                await Task.Delay(100);
            }
            await DiscardContainerAsync();

            return result;
        }
        /// <summary>
        /// 手動実行プロセス
        /// </summary>
        /// <param name="fileInformations">ファイル情報の列</param>
        /// <returns>手動実行の実行結果</returns>
        public async Task<StandardCmd> ManualExecAsync(FileInformation[] fileInformations, bool isAvailableExtension, bool isZip)
        {

            StandardCmd result = new();
            string mainFile = "";
            string directoryPath = "./executeFiles/" + _containerName + "/data";
            if ((result = await BuildExecuteEnvironmentAsync()).ExitCode == 0)
            {
                foreach (FileInformation fileInformation in fileInformations)
                {
                    string filePath = directoryPath + "/" + fileInformation.FileName;
                    await DownloadFileAsync(_ex2PlusUrl + fileInformation.FileId + _apiKey, filePath);
                    if (isZip)
                    {
                        if ((result = await UnzipAsync(filePath)).ExitCode == 0)
                        {
                            mainFile = Path.GetFileName(await JudgeUnzipMainFileAsync(directoryPath));
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        if (JudgeMainFile(filePath))
                        {
                            mainFile = fileInformation.FileName;
                        }
                    }
                }
                if (_notCompileLanguage.Any(value => value == _lang) && mainFile == "")
                {
                    if ((mainFile = JudgeMainFile(fileInformations)) == "")
                    {
                        return new StandardCmd("Main File not found.", "Main File not found.", 0);
                    }
                }
                else
                {
                    if (isZip && mainFile == "") { }
                    else
                    {
                        if (mainFile != "")
                        {
                            if (_lang == "clang")
                            {
                                result = await CompileClangAsync(directoryPath, mainFile);
                            }
                            if (_lang == "java11")
                            {
                                result = await CompileJavaAsync(directoryPath, mainFile);
                            }
                        }
                        else
                        {
                            return new StandardCmd("Main File not found.", "Main File not found.", 0);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// コンテナに必要なフォルダを作ってコンテナを立てる
        /// コンテナを立てる処理はバッシュファイルに記述(./bashfile/container.sh)
        /// </summary>
        /// <returns>実行結果</returns>
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
        /// <summary>
        /// コンテナ関連のフォルダを削除してコンテナを閉じる
        /// </summary>
        /// <returns>実行結果</returns>
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
            string build = "./bashfile/container.sh " + _lang + " " + _containerName;
            return await ExecuteAsync(build);
        }
        public async Task<StandardCmd> StopContainerAsync()
        {
            string cmdStr = "-c \"docker stop -t 0 " + _containerName + " \"";
            return await ExecuteAsync(cmdStr);
        }
        public async Task<StandardCmd> RmContainerAsync()
        {
            string cmdStr = "-c \"docker rm " + _containerName + " \"";
            return await ExecuteAsync(cmdStr);
        }
        public async Task<StandardCmd> MkdirExecuteFoldersAsync()
        {
            string mkdir = "./bashfile/mkdirExecuteFolder.sh " + _containerName;
            return await ExecuteAsync(mkdir);
        }
        public async Task<StandardCmd> CpExecuteFoldersAsync()
        {
            string cp = "./bashfile/cpExecuteFolder.sh " + _lang + " " + _containerName;
            return await ExecuteAsync(cp);
        }
        public async Task<StandardCmd> RmContainerFilesAsync()
        {
            string rmFiles = "-c \"docker exec -i -w /opt/data " + _containerName + " bash -c 'rm -fR *'\"";
            return await ExecuteAsync(rmFiles);
        }
        public async Task<StandardCmd> RmExecuteFoldersAsync()
        {
            string rmFolder = "./bashfile/rmExecuteFolder.sh " + _containerName;
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
                string cmdStr = "-c \"docker exec -i -w " + _path + " " + _containerName + " bash -c '" + line + "'\"";
                return await ExecuteAsync(cmdStr);
            }
        }
        public async Task<StandardCmd> CdAsync(string line)
        {
            string cmdStr = "-c \"docker exec -i -w " + _path + " " + _containerName + " bash -c '" + line + " && pwd'\"";
            StandardCmd standardCmd = await ExecuteAsync(cmdStr);
            if (standardCmd.ExitCode == 0)
            {
                _path = standardCmd.Output.Trim();
            }
            return standardCmd;

        }
        public async Task<StandardCmd> CurrentDirectoryContainerAsync()
        {
            string pwd = "-c \"docker exec -i -w " + _path + " " + _containerName + " bash -c 'basename `pwd`'\"";
            StandardCmd pwdResult = await ExecuteAsync(pwd);
            return pwdResult;
        }
        public async Task<StandardCmd> UnzipAsync(string filePath)
        {
            try
            {
                string unzipPath = Path.GetDirectoryName(filePath) ?? "";
                string unzip = "-c \"unzip -d " + unzipPath + " " + filePath + "\"";
                StandardCmd unzipResult = await ExecuteAsync(unzip);
                if (unzipResult.ExitCode != 0)
                {
                    return unzipResult;
                }
                string rmzip = "-c \"rm -fR " + filePath + "\"";
                StandardCmd rmzipResult = await ExecuteAsync(rmzip);
                if (rmzipResult.ExitCode != 0)
                {
                    return rmzipResult;
                }
                return rmzipResult;
            }
            catch (Exception)
            {
                return new StandardCmd("can't open zip file.", "can't open zip file.", -1);
            }
        }
        public async Task<StandardCmd> RmAsync(string fileName)
        {
            string cmdStr = "-c \"docker exec -i -w /opt/data " + _containerName + " bash -c 'rm -Rf " + fileName + "\"";
            return await ExecuteAsync(cmdStr);
        }
        public async Task<string> PressTabAsync(string str)
        {
            _isAuto = true;
            bool isFirstArgument = true;
            bool isSecondEmpty = false;
            Regex reg = new Regex("-tab");
            str = reg.Replace(str, "", 1);
            if (str == "")
            {
                isFirstArgument = false;
            }
            string[] command = str.TrimEnd().Split(" ");
            foreach (string s in command)
            {
                str = s.Trim();
            }
            string judgeFolder = "-c \"docker exec -i -w " + _path + " " + _containerName + " bash -c 'ls -l | grep ^d'\"";
            StandardCmd result = await ExecuteAsync(judgeFolder);
            List<string> folders = new List<string>();
            folders = result.Output.TrimEnd().Split("\n").ToList();
            for (int i = 0; i < folders.Count; i++)
            {
                Console.WriteLine("folder[i].Split(\" \")[folder[i].Split(\" \").Length - 1]:" + folders[i].Split(" ")[folders[i].Split(" ").Length - 1]);
                folders[i] = folders[i].Split(" ")[folders[i].Split(" ").Length - 1];
                Console.WriteLine("folder[i]:" + folders[i]);
            }
            Console.WriteLine("folder[folder.length - 1]:" + folders[0]);
            Console.ReadKey();
            string filenamePrediction = "-c \"docker exec -i -w " + _path + " " + _containerName + " bash -c 'ls -d " + str + "*'\"";
            result = await ExecuteAsync(filenamePrediction);
            if (result.ExitCode != 0)
            {
                isSecondEmpty = true;
                string filenamePredictionRetry = "-c \"docker exec -i -w " + _path + " " + _containerName + " bash -c 'ls'\"";
                result = await ExecuteAsync(filenamePredictionRetry);
            }
            if (result.ExitCode != 0)
            {
                _isAuto = false;
                return "";
            }
            else
            {
                string[] files = result.Output.TrimEnd().Split("\n");
                if (files.Length == 1)
                {
                    Regex reg2 = new Regex(str);
                    str = reg2.Replace(files[0], "", 1);
                    _isAuto = false;
                    return str;
                }
                else
                {
                    if (isSecondEmpty)
                    {
                        str = "";
                    }
                    else
                    {
                        Comparison comparison = new Comparison();
                        str = comparison.FirstMatchString(files);
                    }

                    string filesString = "";
                    string[] fileNamesString = result.Output.Split("\n");
                    Console.WriteLine("result.Output:" + result.Output);
                    Console.WriteLine("filesString.Length:" + filesString.Length);
                    for (int i = 0; i < fileNamesString.Length; i++)
                    {
                        Console.WriteLine("Path:" + Path.GetFileName(fileNamesString[i]));
                        fileNamesString[i] = Path.GetFileName(fileNamesString[i]);
                        for (int j = 0; j < folders.Count; j++)
                        {
                            if (fileNamesString[i] == folders[j])
                            {
                                fileNamesString[i] += "/";
                                folders.Remove(folders[j]);
                            }
                        }

                    }
                    filesString = "\n";
                    foreach (string fileNameString in fileNamesString)
                    {
                        Console.WriteLine(fileNameString);
                        filesString += fileNameString + "  ";
                    }
                    filesString.TrimEnd();
                    filesString += "\n[" + (await CurrentDirectoryContainerAsync()).Output.Trim() + "]# ";
                    Console.WriteLine("command.Length:" + command.Length);
                    if (isSecondEmpty)
                    {
                        filesString += command[0] + " ";
                    }
                    else if (!isFirstArgument) { }
                    else if (command.Length == 1)
                    {
                        filesString += str;
                    }
                    else
                    {
                        if (command.Length >= 2)
                        {
                            Regex option = new Regex(@"^-[a-zA-Z]+");
                            foreach (string s in command)
                            {
                                //ここ編集
                                if(option.IsMatch(s)){

                                }
                            }
                        }
                        filesString += command[0] + " " + str;
                    }
                    _isAuto = false;
                    return filesString;
                }
            }
        }
        public async Task<StandardCmd> CompileJavaAsync(string directoryPath, string fileName)
        {
            string filePath = "";
            string generateDirectoryPath = "";
            string insideContainerFilePath = "";
            if ((generateDirectoryPath = await ReplaceJavaPackageAsync(directoryPath)) == "")
            {
                filePath = directoryPath + "/" + fileName;
                insideContainerFilePath = "/opt/data" + "/" + fileName;
            }
            else
            {
                filePath = directoryPath + "/" + generateDirectoryPath;
                insideContainerFilePath = "/opt/data/" + generateDirectoryPath;
                _generateDirectoryPath = generateDirectoryPath;
            }
            string encode = judgeEncode(filePath);
            string className = Path.GetFileNameWithoutExtension(fileName);
            if (encode.ToLower().Contains("utf-8"))
            {
                string compile = "-c \"docker exec -w " + Path.GetDirectoryName(insideContainerFilePath) + " " + _containerName + " bash -c 'javac -encoding UTF-8 " + fileName + "'\"";
                StandardCmd compileResult = await ExecuteAsync(compile);
                if (compileResult.ExitCode == 0)
                {
                    return compileResult;
                }
            }
            if (encode.ToLower().Contains("sjis"))
            {
                string compileSjis = "-c \"docker exec -w " + Path.GetDirectoryName(insideContainerFilePath) + " " + _containerName + " bash -c 'javac -encoding Shift_JIS " + fileName + "'\"";
                StandardCmd compileSjisResult = await ExecuteAsync(compileSjis);
                if (compileSjisResult.ExitCode == 0)
                {
                    return compileSjisResult;
                }
            }
            return new StandardCmd("compile error encoding " + encode, "compile errorencoding " + encode, -1);
        }
        public async Task<StandardCmd> CompileClangAsync(string directoryPath, string mainFile)
        {
            string encode = judgeEncode(directoryPath + "/" + mainFile);
            if (encode.ToLower().Contains("utf-8"))
            {
                string compile = "-c \"docker exec -w /opt/bin " + _containerName + " bash compile.sh ";
                compile = GenerateCompileString(directoryPath, mainFile, compile);
                StandardCmd compileResult = await ExecuteAsync(compile);
                if (compileResult.ExitCode == 0)
                {
                    return compileResult;
                }
            }
            if (encode.ToLower().Contains("sjis"))
            {
                string compileSjis = "-c \"docker exec -w /opt/bin " + _containerName + " bash compile_sjis.sh ";
                compileSjis = GenerateCompileString(directoryPath, mainFile, compileSjis);
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
            string cmdStr = "";
            if (_generateDirectoryPath == "")
            {
                string mainFilePath = Path.GetDirectoryName(fileName) == "" ? "" : "/" + Path.GetDirectoryName(fileName);
                string className = Path.GetFileNameWithoutExtension(fileName);
                cmdStr = "-c \"docker exec -i -w /opt/bin " + _containerName + " bash -c 'bash execute.sh " + className + "'\"";
            }
            else
            {
                //Package記述のあるJavaファイルに対応する
                string mainFilePath = Path.GetDirectoryName(fileName) == "" ? "" : "/" + Path.GetDirectoryName(fileName);
                string className = Path.GetFileNameWithoutExtension(fileName);
                string[] executeDirectoryPath = _generateDirectoryPath.Split("/");
                cmdStr = "-c \"docker exec -i -w /opt/data " + _containerName + " bash -c 'bash /opt/bin/execute.sh " + Path.GetDirectoryName(_generateDirectoryPath) + "/" + className + "'\"";
            }

            return await ExecuteAsync(cmdStr);
        }
        /// <summary>
        /// コマンドの文字列を受け取り実行するプロセス
        /// </summary>
        /// <param name="cmdStr">コマンドの文字列</param>
        /// <returns>実行結果</returns>
        private async Task<StandardCmd> ExecuteAsync(string cmdStr)
        {
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

                if (_isAuto)
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
#pragma warning disable CS8602 //コンストラクタでセットしてるから大丈夫
            _setStreamWriter(sw);
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

            return str;
        }
        private async Task<string> ProcessInOutAutoAsync(Process process)
        {
            string str = "";
            string[] strIn = _inputStr.Split("\n");
            int ch = -1;
            int i = 0;
            bool isTimeOut = true;

            var task = Task.Run(() =>
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
            int ch = 0;
            string str = "";
            StreamReader sr = process.StandardOutput;
            try
            {
                while ((ch = sr.Read()) != -1)
                {
                    str = str + (char)ch;
                    // Console.Write((char)ch);
                    await Task.Delay(1);
#pragma warning disable CS8602 //コンストラクタでセットするから大丈夫
                    await _requestWriteAsync(((char)ch).ToString());
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
        private string judgeEncode(string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(filePath);
                string name = "";
                // ファイル自動判別読み出しクラスを生成
                using (Hnx8.ReadJEnc.FileReader reader = new FileReader(file))
                {
                    // 判別読み出し実行。判別結果はReadメソッドの戻り値で把握できます
                    try
                    {
                        Hnx8.ReadJEnc.CharCode charCode = reader.Read(file);
                        // 戻り値のNameプロパティから文字コード名を取得できます
                        name = charCode.Name;
                    }
                    catch (System.NotSupportedException)
                    {
                        name = "sjis";
                    }
                }
                return name;
            }
            catch (Exception)
            {
                return "not Found File";
            }
        }
        private bool JudgeMainFile(string filePath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string fileText = sr.ReadToEnd();
                    if (_lang == "java11")
                    {
                        if (fileText.ToLower().Contains("void main"))
                        {
                            return true;
                        }
                    }
                    if (_lang == "clang")
                    {
                        if (fileText.ToLower().Contains("main("))
                        {
                            return true;
                        }
                    }
                    if (_lang == "python3")
                    {
                        if (fileText.ToLower().Contains("if __name__ == '__main__'"))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                return false;
            }
            return false;
        }
        private string JudgeMainFile(FileInformation[] fileInformations)
        {
            string mainFile = "";
            foreach (FileInformation fileInformation in fileInformations)
            {
                if (_lang == "python3" && Path.GetExtension(fileInformation.FileName) == ".py")
                {
                    mainFile = fileInformation.FileName;
                    break;
                }
                if (_lang == "php" && Path.GetExtension(fileInformation.FileName) == ".php")
                {
                    mainFile = fileInformation.FileName;
                    break;
                }
            }
            return mainFile;
        }
        private async Task<string> JudgeUnzipMainFileAsync(string directoryPath)
        {
            string mainFile = "";
            List<string> filePaths = GetFilePaths(directoryPath);
            foreach (string filePath in filePaths)
            {
                if (JudgeMainFile(filePath))
                {
                    mainFile = filePath;
                }
            }
            if (mainFile != "" && Path.GetFileName(Path.GetDirectoryName(mainFile)) != "data")
            {
                await moveFileDirectoryAsync(mainFile, directoryPath);
            }
            else if (mainFile == "")
            {
                string firstFilePath = GetFirstFilePath(directoryPath);
                await moveFileDirectoryAsync(firstFilePath, directoryPath);
            }

            return mainFile;
        }
        private string GenerateCompileString(string diretoryPath, string mainFile, string compile)
        {
            List<string> filesPath = GetFilePaths(diretoryPath);
            foreach (string filePath in filesPath)
            {
                if (Path.GetFileName(filePath) == mainFile)
                {
                    compile += ("../data/" + Path.GetFileNameWithoutExtension(mainFile) + " ");
                    break;
                }
            }
            foreach (string filePath in filesPath)
            {
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
        private List<string> GetFilePaths(string directoryPath)
        {
            List<string> filePaths = new List<string>();
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                filePaths.Add(file);
            }
            string[] directories = Directory.GetDirectories(directoryPath);
            foreach (string directory in directories)
            {
                foreach (string file in GetFilePaths(directory))
                {
                    filePaths.Add(file);
                }
            }
            return filePaths;
        }
        private string GetFirstFilePath(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            foreach (string file in files)
            {
                return file;
            }
            string[] directories = Directory.GetDirectories(directoryPath);
            foreach (string directory in directories)
            {
                return GetFirstFilePath(directory);
            }
            return "";
        }
        private async Task moveFileDirectoryAsync(string filePath, string directoryPath)
        {
            string mv = "-c \"mv " + Path.GetDirectoryName(filePath) + "/* " + directoryPath;
            await ExecuteAsync(mv);
            string rm = "-c \"rm -fR " + Path.GetDirectoryName(filePath);
            await ExecuteAsync(rm);
        }
        private async Task<string> ReplaceJavaPackageAsync(string directoryPath)
        {
            string mainFilePath = "";
            try
            {
                List<string> filePaths = GetFilePaths(directoryPath);
                Regex search = new Regex(@"package ([a-zA-Z0-9\.]*);");
                Regex replaceDottoSlash = new Regex(@"\.");

                foreach (string filePath in filePaths)
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string fileGenerationPath = "";
                        string mvFile = "";

                        string fileText = sr.ReadToEnd();
                        if (fileText.ToLower().Contains("package"))
                        {
                            fileGenerationPath = (search.Match(fileText)).Value.ToString();
                            fileGenerationPath = search.Replace(fileGenerationPath, "$1");
                            fileGenerationPath = replaceDottoSlash.Replace(fileGenerationPath, "/");
                            string mkdir = "-c \"mkdir -p " + directoryPath + "/" + fileGenerationPath + "\"";
                            await ExecuteAsync(mkdir);
                            mvFile = "-c \"mv " + filePath + " " + directoryPath + "/" + fileGenerationPath + "/\"";
                            await ExecuteAsync(mvFile);
                            if (JudgeMainFile(directoryPath + "/" + fileGenerationPath + "/" + Path.GetFileName(filePath)))
                            {
                                mainFilePath = fileGenerationPath + "/" + Path.GetFileName(filePath);
                            }
                        }
                    }
                }
                return mainFilePath;
            }
            catch (Exception)
            {
                return mainFilePath;
            }
        }
    }
}