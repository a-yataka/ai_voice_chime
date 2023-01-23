using AI.Talk.Editor.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WindowsFormsApp1
{
    class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>

        public static string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
        public static string configPath = exeDir + "\\aiv_chime_config.yml";
        public static Encoding encoding = Encoding.UTF8;
        //private string exeDir = Path.GetDirectoryName(Application.ExecutablePath);

        public static Dictionary<string, string[]> pathDictionary = new Dictionary<string, string[]>();
        public static string[] timeDirectories;

        //public Program()
        //{
        //    exeDir = Path.GetDirectoryName(Application.ExecutablePath);
        //    configPath = exeDir + "\\aiv_chime_config.yml";
        //}

        static void Main(String[] args)
        {

            //new Program();
            //System.Media.SystemSounds.Exclamation.Play();
            //string exeDir;
            //Debug.WriteLine(Properties.Settings.Default.AIv_CHIME_CONFIG_PATH);
            //Debug.WriteLine(Path.GetDirectoryName(Application.ExecutablePath));
            //File.Create(Path.GetDirectoryName(Application.ExecutablePath) + "/ai_v_config.yml");

            //InitializeConfig();

            Debug.WriteLine(exeDir);

            //わざわざ作らなくてもサンプルファイル置いておくだけでよさげ
            //WriteSample();

            //ここでRingScheduleを取得したい

            // 音声ファイルを取得するならvoiceディレクトリからwavを読み込むべき
            Chime[] chimeList = LoadConfig();

            //readWavFiles();

            //ValidateConfig(chimeList);

            ReadVoiceDirectory();

            //
            //RingSchedule ringSchedule = new RingSchedule();
            //new Ring(chimeList);

            //Ring ring = new Ring(chimeList);
            //Ring.RingWav(chimeList);


            VoiceUtil a = new VoiceUtil();
            //a.addChime(chimeList);
            CreateNotifyIcon();
            Application.Run();


        }

        private static Chime[] LoadConfig()
        {

            var streamReader = new StreamReader(configPath, encoding);
            string readToEnd = streamReader.ReadToEnd();

            Debug.WriteLine(readToEnd);

            var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
                                .Build();

            var deserializeObject = deserializer.Deserialize<Config>(readToEnd);



            Chime[] chimeList = deserializeObject.Chimes.ToArray();

            return chimeList;
        }

        // 設定ファイルの作成
        private static void InitializeConfig()
        {
            // 設定ファイル作成 開けたら閉める
            File.Create(configPath).Close();

            // 設定用ファイルパスの書き込み 開けたら閉める
            //Encoding enc = Encoding.GetEncoding("Shift_JIS");
            //StreamWriter writer = new StreamWriter(configPath, true, enc);
            //writer.WriteLine($"config_path: {configPath}");
            //writer.Close();

            // 設定ファイルのパスをアプリケーションに保存
            //Properties.Settings.Default.AIv_CHIME_CONFIG_PATH = configPath;
            //Properties.Settings.Default.Save();
        }

        private static void ValidateConfig(Chime[] chimeList)
        {
            List<string> timeList = new List<string>(chimeList.Length);
            foreach (Chime chime in chimeList)
            {
                // チェックしたい変数
                string time = chime.Time;
                // ついでに次処理用に配列作成
                timeList.Add(time);
                if (time.Length != 4)
                {
                    // time文字列の長さが4文字出ない場合はエラー
                }
            }
            // 時刻の重複をチェック
            if (chimeList.Length != timeList.Distinct().Count())
            {
                // timeに重複がある場合はエラー
            }

            //voiceディレクトリから 拡張子がwav かつ ファイル名が4字の半角数字 のファイルを読み込む
            //かつ0000-2400
            //voiceディレクトリがなかったらエラー

            //末尾が.wav かつ 0000-2400 かつ文字数は必ず7字
            //その前にconfigファイルで設定したものが見つからなかったらエラー
            //いやconfig読まなくてもvoiceにルールに沿ったファイルがあれば時報に加えるようにしよう
            //string[] files = Directory.GetFiles(exeDir, "*Help*");

        }

        private static void readWavFiles()
        {
            ////末尾が.wav かつ 0000-2400 かつ文字数は必ず7字

            //string[] filePathArray = Directory.GetFiles(exeDir, "*Help*");




            // 正規表現
            string pattern = @"^(([01][1-9]|2[0-3])[0-5][0-9].wav)$";
            pattern = @"^C";
            
            Debug.WriteLine(pattern);
            //string pattern = @"([01][0-9]|2[0-3])[0-5][0-9]\.wav";

            //正規表現でファイル名ではなくフルパスでmatchしてる
            //うまいことファイル名だけ取り出すか、フルパスで正規表現
            var e = Directory.EnumerateFiles(exeDir+"\\voice")
                .Where(x => Regex.IsMatch(x, pattern));

            // 結果の表示
            Console.WriteLine("正規表現");
            foreach (string f in e)
            {
                Debug.WriteLine($"{f}");
            }
            Debug.WriteLine("end");

        }


        // サンプルデータの書き込み
        private static void WriteSample()
        {
            // 書き込み 面倒なのでひたすらadd line
            StreamWriter writer = new StreamWriter(configPath, true, encoding);

            writer.WriteLine("chimes:");

            //// string型の配列を24個用意
            //// 書き方ダサいけどc#のforeachはインデックス取るのがちょっと面倒...
            //for (int i = 0; i < 24; i++)
            //{
            //    // 文字列を2桁の0埋め
            //    string zeroParsedTime = i.ToString("D2");
            //    writer.WriteLine($"  - time: {zeroParsedTime}00");

            //    // 偶数だったら茜、奇数なら葵
            //    string kotonohaPreset = (i % 2 == 0) ? "琴葉 茜" : "琴葉 葵";
            //    writer.WriteLine($"    preset: {kotonohaPreset}");
            //    writer.WriteLine($"    message: {i}時です");
            //}

            for (int i = 0; i < 12; i++)
            {
                string zeroParsedTime = i.ToString("D2");
                writer.WriteLine($"  - time: {zeroParsedTime}00");

                string kotonohaPreset = (i % 2 == 0) ? "琴葉 茜" : "琴葉 葵";
                writer.WriteLine($"    preset: {kotonohaPreset}");
                writer.WriteLine($"    message: 午前{i}時です");
            }
            for (int i = 12; i < 24; i++)
            {
                string zeroParsedTime = i.ToString("D2");
                writer.WriteLine($"  - time: {zeroParsedTime}00");

                string kotonohaPreset = (i % 2 == 0) ? "琴葉 茜" : "琴葉 葵";
                writer.WriteLine($"    preset: {kotonohaPreset}");
                writer.WriteLine($"    message: 午後{i-12}時です");
            }

            // 開けたら閉める
            writer.Close();
        }



        private static void CreateNotifyIcon()
        {
            // 常駐アプリ（タスクトレイのアイコン）を作成
            var icon = new NotifyIcon();
            icon.Icon = new Icon("Icon1.ico");

            icon.ContextMenuStrip = ContextMenu();
            icon.Text = "常駐アプリ";
            icon.Visible = true;
        }
        private static ContextMenuStrip ContextMenu()
        {
            // アイコンを右クリックしたときのメニューを返却
            var menu = new ContextMenuStrip();
            menu.Items.Add("ボイス生成", null, (s, e) => {
                // メイン機能
                DialogResult result = MessageBox.Show(
                    "aiv_chime_config.yml からボイスデータを作成します。",
                    "ボイスデータ作成",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                );
                if (result == DialogResult.OK)
                {
                    //セッティング
                    //なんかあったらエラーダイアログを返す
                    //ymlに文法エラーとか、プリセットが見つからないとか
                }
            });
            // 読み込み&ボイス生成
            menu.Items.Add("使い方", null, (s, e) => {
                // オフライン用ドキュメントorウェブサイトリンク
                ;
            });
            menu.Items.Add("終了", null, (s, e) => {
                // アプリの終了
                Application.Exit();
            });
            return menu;
        }

        private static void ReadVoiceDirectory()
        {
            string pattern = @"voice\\([01][1-9]|2[0-3])[0-5][0-9]$";

            IEnumerable<string> directories = Directory.EnumerateDirectories(exeDir + "\\voice")
                .Where(x => Regex.IsMatch(x, pattern));

            timeDirectories = new string[directories.Count()];
            Debug.WriteLine("element at");
            timeDirectories = directories.ToArray();

            Debug.WriteLine(timeDirectories[0]);




            ///////
            Debug.WriteLine("start");

            //DirectoryInfo directoryInfo = new DirectoryInfo(exeDir+"\\voice");
            //IEnumerable<DirectoryInfo> directoryInfos = directoryInfo.EnumerateDirectories();

            string pattern1 = @"voice\\([01][1-9]|2[0-3])[0-5][0-9]$";
            string pattern2 = @".wav$";

            //IEnumerable<string> directories = Directory.EnumerateDirectories(exeDir + "\\voice")
            //    .Where(x => Regex.IsMatch(x, pattern1));


            foreach (string directory in directories)
            {
                Debug.WriteLine(directory);
                IEnumerable<string> wavFiles = Directory.EnumerateFiles(directory)
                    .Where(x => Regex.IsMatch(x, pattern2));

                pathDictionary.Add("0000", new string[wavFiles.Count()]);
                Debug.WriteLine($"{wavFiles.Count()}");

                foreach (string wavFile in wavFiles)
                {
                    Debug.WriteLine($"{wavFile}");
                    //pathDictionary.Add("0000", new string[] {"aaa.wav", "bbb.wav"});
                }
            }



            
            //Debug.WriteLine(pattern);
            //string pattern = @"([01][0-9]|2[0-3])[0-5][0-9]\.wav";

            //正規表現でファイル名ではなくフルパスでmatchしてる
            //うまいことファイル名だけ取り出すか、フルパスで正規表現
            //var e = Directory.EnumerateFiles(exeDir + "\\voice")
            //    .Where(x => Regex.IsMatch(x, pattern));

            //// 結果の表示
            //Console.WriteLine("正規表現");
            //foreach (string f in e)
            //{
            //    Debug.WriteLine($"{f}");
            //}
            //Debug.WriteLine("end");

            //foreach (DirectoryInfo di in directoryInfos)
            //{
            //    Debug.WriteLine(di.FullName);
            //}
            Debug.WriteLine("end");

        }

    }

}
