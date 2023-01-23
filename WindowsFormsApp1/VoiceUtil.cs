using AI.Talk.Editor.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // A.I.VOICE使ってymlからボイスデータを自動作成する
    // 今のところフレーズ編集とかは考えておらず、プリセットのみ。あくまでお手軽設定
    class VoiceUtil
    {
        private TtsControl _ttsControl;
        private string[] hostList;
        private string vm2;

        public VoiceUtil()
        {


            _ttsControl = new TtsControl();
            hostList = _ttsControl.GetAvailableHostNames();
            Debug.WriteLine(hostList.Length);

            for (int i = 0; i < hostList.Length; i++)
            {
                Debug.WriteLine(hostList[i]);
            }

            try
            {
                _ttsControl.Initialize(hostList[0]);
                this.Startup();
            }catch(Exception e){
                Debug.WriteLine(e.Message);
            }
            //addMessages();
        }

        private void Startup()
        {
            try
            {
                if (_ttsControl.Status == HostStatus.NotRunning)
                {
                    // ホストプログラムを起動する
                    _ttsControl.StartHost();
                }

                // ホストプログラムに接続する
                _ttsControl.Connect();

                vm2 = "ホストバージョン: " + _ttsControl.Version;

                //this.RefreshVoiceNames();
                //this.RefreshVoicePresetNames();


                Debug.WriteLine("ホストへの接続を開始しました。");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ホストへの接続に失敗しました。" + Environment.NewLine + ex.Message);
            }
        }
        private void addMessages()
        {
            // configにある文字とリストの長さを追加
            // aivoice にあるリスト - configにあるリストは保存後削除

            //サンプルリスト
            _ttsControl.AddListItem("琴葉 茜", "こんにちは");
            _ttsControl.AddListItem("琴葉 葵", "こんにちは");
            _ttsControl.AddListItem("結月 ゆかり", "こんにちは");
            int chimeListLength = 3; 

            var n = _ttsControl.GetListCount();
            Debug.WriteLine(n);

            //任意の行を選択状態に
            _ttsControl.SetListSelectionRange(n-3, 3);
            var tt = _ttsControl.GetListSelectionIndices();
            Debug.WriteLine(tt.Length);
            Debug.WriteLine(tt[0]);
            Debug.WriteLine(tt[1]);
            Debug.WriteLine(tt[2]);
            _ttsControl.SetListSelectionIndices(tt);



            //_ttsControl.SaveAudioToFile("C:\\Users\\kasum\\Desktop");

            //int[] list = new int[] { 0,1,2,3,4,5,6 }; 
            //_ttsControl.SetListSelectionIndices(list);

            for (int i = 0; i < tt.Length; i++)
            {
                _ttsControl.SetListSelectionIndex(n-i-1);
                _ttsControl.SaveAudioToFile($"C:\\Users\\kasum\\Desktop\\voice\\{n-i-1}.wav");
                _ttsControl.RemoveListItem();
            }

            //foreach (int i in tt)
            //{
            //    var task = Task.Factory.StartNew(() =>
            //    {
            //        _ttsControl.SaveAudioToFile(i.ToString());
            //        try {
            //            _ttsControl.SaveAudioToFile($"C:\\Users\\kasum\\Desktop\\voice\\{i}.wav");
                        
            //        }
            //        catch(Exception e)
            //        {

            //        }
            //    });
            //}

            //_ttsControl.RemoveListItem();
            Debug.WriteLine("end");
        }
        public void addChime(Chime[] chimeList)
        {
            foreach (Chime chime in chimeList) {
                _ttsControl.AddListItem($"{chime.Preset}", $"{chime.Message}");
            }

            //追加したリストの長さ（もともと存在したAIVOICEのリストは含めない）
            int chimeLentgh = chimeList.Length;
            //AIVOIVEに含まれているすべてのリスト
            int listLength = _ttsControl.GetListCount();

            for (int i = 0; i < chimeLentgh; i++)
            {
                _ttsControl.SetListSelectionIndex(listLength - i - 1);
                _ttsControl.SaveAudioToFile($"C:\\Users\\kasum\\Desktop\\voice\\{chimeList[listLength - i - 1].Time}.wav");
            }
            for (int i =0; i < chimeLentgh; i++)
            {
                _ttsControl.RemoveListItem();
            }
        }
    }
}
