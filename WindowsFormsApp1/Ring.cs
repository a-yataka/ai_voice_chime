using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Ring
    {
        // タイマー
        private System.Timers.Timer timer;
        // ５秒ごとに更新される現在時刻
        private DateTime dateTimeNow;
        // voiceディレクトリ配下の時刻名ディレクトリ
        private string[] timeDirectories;
        // exeのおいてあるディレクトリ
        private string exeDir;

        public Ring(string[] timeDirectories, string exeDir)
        {
            dateTimeNow = DateTime.Now;
            this.timeDirectories = timeDirectories;
            this.exeDir = exeDir;

            timer = new System.Timers.Timer(5000);
            // 定期実行する処理
            timer.Elapsed += OnElapsed;
            timer.Start();
        }
        private void OnElapsed(object senderObject, ElapsedEventArgs e)
        {
            // 現在時刻
            dateTimeNow = DateTime.Now;

            // 毎分チェック
            if((0 <= dateTimeNow.Second ) && (dateTimeNow.Second < 5))
            {
                // もうちょっと何とかさ...
                string parsedTime = $"{dateTimeNow.Hour.ToString().PadLeft(2, '0')}{dateTimeNow.Minute.ToString().PadLeft(2, '0')}";

                // 配列に含まれる時刻と、今の時刻が一致する
                // 無理やりパスに変換して一致することを確認。ふつう逆？
                if (timeDirectories.Contains(exeDir + "\\voice\\" + parsedTime)) {
                    RandomRing(parsedTime);
                };
            }
        }
        private async void RandomRing(string parsedTime)
        {
            //ファイル内が空なら何もしない
            //一つでもあれば鳴らす
            //複数あったら配列から乱数指定
            // 正規表現
            string pattern = @"\.wav$";

            //正規表現でファイル名ではなくフルパスでmatchしてる
            //うまいことファイル名だけ取り出すか、フルパスで正規表現
            var e = Directory.EnumerateFiles(exeDir + "\\voice\\" + parsedTime + "\\")
                .Where(x => Regex.IsMatch(x, pattern));

            Random random = new System.Random();

            SoundPlayer player = new SoundPlayer(e.ElementAt(random.Next(0, e.Count())));

            // c#の同期非同期よくわかってない。この書き方は微妙そう
            player.Play();
            await Task.Delay(50000);
            player.Stop();
            // メモリ開放...?
            player.Dispose();
        }      
    }
}
