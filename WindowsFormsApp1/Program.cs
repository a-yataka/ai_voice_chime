using AI.Talk.Editor.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        private static string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
        private static string configPath = exeDir + "\\aiv_chime_config.yml";
        private static Encoding encoding = Encoding.UTF8;
        //private string exeDir = Path.GetDirectoryName(Application.ExecutablePath);

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

            InitializeConfig();

            Debug.WriteLine(exeDir);

            //わざわざ作らなくてもサンプルファイル置いておくだけでよさげ
            WriteSample();
            LoadConfig();

            //VoiceUtil a = new VoiceUtil("abc");
            //CreateNotifyIcon();
            //Application.Run();
        }

        private static void LoadConfig()
        {

            var streamReader = new StreamReader(configPath, encoding);
            string readToEnd = streamReader.ReadToEnd();

            Debug.WriteLine(readToEnd);

            var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
                                .Build();

            var deserializeObject = deserializer.Deserialize<Config>(readToEnd);
            //Debug.WriteLine(result);

            //var p = deserializer.Deserialize<Config>(sy);

            Debug.WriteLine(deserializeObject.Chimes[0].Time);
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
            menu.Items.Add("終了", null, (s, e) => {
                Application.Exit();
            });
            return menu;
        }
    }

}
