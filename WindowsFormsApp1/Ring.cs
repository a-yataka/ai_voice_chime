using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Ring
    {
        private System.Timers.Timer timer;
        private DateTime dateTimeLastCheck;
        private DateTime dateTimeExec;
        private DateTime dateTimeNow;
        private Chime[] chimeList;
        private int chimeIndex;

        public Ring(Chime[] chimeList)
        {
            dateTimeLastCheck = DateTime.Now;
            dateTimeExec = DateTime.Now;
            dateTimeNow = DateTime.Now;
            this.chimeList = chimeList;
            Array.Sort(this.chimeList, (n, m) => n.Time.CompareTo(m.Time));

            chimeIndex = 0;

            timer = new System.Timers.Timer(10000);
            // 定期実行する処理
            timer.Elapsed += OnElapsed;
            timer.Start();
        }
        private void OnElapsed(object senderObject, ElapsedEventArgs e)
        {
            // ここで１分ごとにディレクトリチェックする？
            // メモリがもったいない気がする
            // ディレクトリチェックするくらいならさすがにdict型で最初に確保しておくべき
            // 

            //dateTimeExec = DateTime.Parse("02:11:00");
            // 現在時刻の取得
            dateTimeNow = DateTime.Now;

            string time = chimeList[chimeIndex].Time;
            dateTimeExec = DateTime.Parse(time.Substring(0,2) + ":" + time.Substring(2) + ":00");

            //Debug.WriteLine("##########");
            //Debug.WriteLine(dateTimeLastCheck);
            //Debug.WriteLine(dateTimeExec);
            Debug.WriteLine(dateTimeNow);
            //Debug.WriteLine("#####################");
            // wavファイルを読み込む
            //// ファイルパスは./{時刻.wav}
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(Path.GetDirectoryName(Application.ExecutablePath) + "\\20230118-005702_琴葉 茜_テスト音声.wav");

            //// 非同期再生する
            //player.Play();

            //player = null;
            ////player.Dispose();

            // 最終チェック時刻（5秒前）< 指定時刻 < 現在時刻
            // 
            if (dateTimeLastCheck < dateTimeExec && dateTimeExec < dateTimeNow)
            {
                Debug.WriteLine("eeeexxxeeecccc###########################");
                Debug.WriteLine(dateTimeExec);
                Debug.WriteLine("eeeexxxeeecccc###########################");
                //// wavファイルを読み込む
                //// ファイルパスは./{時刻.wav}
                //System.Media.SoundPlayer player = new System.Media.SoundPlayer(Path.GetDirectoryName(Application.ExecutablePath) + "\\20230118-005702_琴葉 茜_テスト音声.wav");
                //// 非同期再生する
                //player.Play();

                //RingWav(chimeList);
            }
            // 処理が終わってから最終チェック時刻を更新
            dateTimeLastCheck = dateTimeNow;
        }
        public void DisposeTimer()
        {
            timer.Stop();
        }
        public void RingWav(Chime[] chimeList) {
            //整列
            //chimeList.Sort((n,m) => n.Time - m.Time);
            //Array.Sort(chimeList, (n,m)=> n.Time.CompareTo(m.Time));

            // ring[chimeIndex]

            // 鳴らすchimeを管理
            if (chimeIndex == chimeList.Length - 1)
            {
                chimeIndex = 0;
            } else {
                chimeIndex++;
            }
        }
            
    }

}
