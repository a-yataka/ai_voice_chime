using AI.Talk.Editor.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WindowsFormsApp1
{
    // A.I.VOICE使ってymlからボイスデータを自動作成する
    // 今のところフレーズ編集とかは考えておらず、プリセットのみ。あくまでお手軽設定
    class VoiceUtil
    {
        private TtsControl _ttsControl;
        private string[] hostList;
        private Chime[] chimeList;
        private int AIVoiceIndex;
        private string exeDir;

        public VoiceUtil(string exeDir)
        {
            this.exeDir = exeDir;

            _ttsControl = new TtsControl();
            hostList = _ttsControl.GetAvailableHostNames();
            //Debug.WriteLine(hostList.Length);

            for (int i = 0; i < hostList.Length; i++)
            {
                Debug.WriteLine(hostList[i]);
                if (hostList[i] == "A.I.VOICE Editor")
                {
                    AIVoiceIndex = i;
                }
            }

            try
            {
                _ttsControl.Initialize(hostList[AIVoiceIndex]);
                Startup();
            }catch(Exception e){
                Debug.WriteLine(e.Message);
            }

            _ttsControl.TextEditMode = TextEditMode.List;

        }


        private void Startup()
        {
            Directory.CreateDirectory(exeDir + $"\\voice");

            try
            {
                if (_ttsControl.Status == HostStatus.NotRunning)
                {
                    // ホストプログラムを起動する
                    _ttsControl.StartHost();
                }

                // ホストプログラムに接続する
                _ttsControl.Connect();

                Debug.WriteLine("ホストへの接続を開始しました。");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ホストへの接続に失敗しました。" + Environment.NewLine + ex.Message);
            }
        }


        public void addVoice()
        {
            //追加したリストの長さ（もともと存在したAIVOICEのリストは含めない）
            //AIVOIVEに含まれているすべてのリスト
            foreach (Chime chime in chimeList)
            {
                // 
                Directory.CreateDirectory(exeDir + $"\\voice\\{chime.Time}");
                // voice.ymlのボイステキストをAIVOICEに追加
                _ttsControl.AddListItem($"{chime.Preset}", $"{chime.Message}");

            }

            int chimeLentgh = chimeList.Length;
            int listLength = _ttsControl.GetListCount();

            // リストの最後尾から保存
            for (int i = 0; i < chimeLentgh; i++)
            {
                _ttsControl.SetListSelectionIndex(listLength - i - 1);
                // exeのあるdir // voice // 時間 // wavファイル
                _ttsControl.SaveAudioToFile($"{exeDir}\\voice\\{chimeList[chimeLentgh - i -1].Time}\\{chimeList[chimeLentgh - i -1].Preset}_{chimeList[chimeLentgh - i -1].Message}.wav");
            }

            // 追加したリストを削除
            for (int i = 0; i < chimeLentgh; i++)
            {
                _ttsControl.SetListSelectionIndex(listLength - i - 1);
                _ttsControl.RemoveListItem();
            }
        }

        public void ReadConfig(Encoding encoding)
        {
            var streamReader = new StreamReader(exeDir + "\\voice.yml", encoding);
            string readToEnd = streamReader.ReadToEnd();

            Debug.WriteLine(readToEnd);

            var deserializer = new DeserializerBuilder()
                                .WithNamingConvention(CamelCaseNamingConvention.Instance)  // see height_in_inches in sample yml 
                                .Build();

            var deserializeObject = deserializer.Deserialize<Config>(readToEnd);

            chimeList = deserializeObject.Chimes.ToArray();

            ValidateConfig(chimeList);

            streamReader.Close();
        }

        public void ValidateConfig(Chime[] chimeList)
        {
            List<Chime> validateChimeList = new List<Chime>(chimeList.Length);
            foreach (Chime chime in chimeList)
            {
                // チェックしたい変数
                Chime time = chime;

                if (time.Time.Length == 4)
                {
                    // バリデーションに当てはまる要素だけ抜き取る
                    validateChimeList.Add(time);
                }
            }
            this.chimeList = validateChimeList.ToArray();
        }
    }
}
