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
        public static string configPath = exeDir + "\\voice.yml";
        public static Encoding encoding = Encoding.UTF8;
        public static Dictionary<string, string[]> pathDictionary = new Dictionary<string, string[]>();
        public static string[] timeDirectories;


        static void Main(String[] args)
        {
            Directory.CreateDirectory(exeDir + $"\\voice");
            // wavのあるディレクトリを読み込む
            //ReadVoiceDirectory();
            // 読み込んだディレクトリから定刻でwavを再生
            Ring ring = new Ring(timeDirectories, exeDir);

            CreateNotifyIcon();
            Application.Run();
        }

        private static void CreateNotifyIcon()
        {
            // 常駐アプリ（タスクトレイのアイコン）を作成
            var icon = new NotifyIcon();
            icon.Icon = new Icon("icon.ico");

            icon.ContextMenuStrip = ContextMenu();
            icon.Text = "A.I.Chime";
            icon.Visible = true;
        }
        private static ContextMenuStrip ContextMenu()
        {
            // アイコンを右クリックしたときのメニューを返却
            var menu = new ContextMenuStrip();
            menu.Items.Add("ボイス生成", null, (s, e) => {
                // メイン機能
                DialogResult result = MessageBox.Show(
                    "A.I.VOICE Editorを使ってボイスデータを作成します。",
                    "ボイスデータ作成",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                );
                if (result == DialogResult.OK)
                {
                    //セッティング
                    //なんかあったらエラーダイアログを返す
                    //ymlに文法エラーとか、プリセットが見つからないとか
                    VoiceUtil voiceUtil = new VoiceUtil(exeDir);
                    voiceUtil.ReadConfig(encoding);
                    voiceUtil.addVoice();
                }
            });
            // voiceディレクトリ表示
            // ふわっとしすぎな表現
            menu.Items.Add("ファイルを開く", null, (s, e) => {
                System.Diagnostics.Process.Start(exeDir);
            });
            ////
            //menu.Items.Add("wavファイルのリロード", null, (s, e) => {
            //    System.Diagnostics.Process.Start(exeDir);
            //});
            // 読み込み&ボイス生成
            menu.Items.Add("使い方", null, (s, e) => {
                // オフライン用ドキュメントorウェブサイトリンク
                System.Diagnostics.Process.Start("https://github.com/a-yataka/ai_voice_chime");
            });
            menu.Items.Add("終了", null, (s, e) => {
                // アプリの終了
                Application.Exit();
            });
            return menu;
        }

        public static void ReadVoiceDirectory()
        {
            // 時刻ごとに用意したディレクトリを読み込む
            string pattern = @"voice\\([01][1-9]|2[0-3])[0-5][0-9]$";

            IEnumerable<string> directories = Directory.EnumerateDirectories(exeDir + "\\voice")
                .Where(x => Regex.IsMatch(x, pattern));

            timeDirectories = directories.ToArray();
        }
    }
}
