using AI.Talk.Editor.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class VoiceUtil
    {
        private string name;
        private TtsControl _ttsControl;

        public VoiceUtil(string name)
        {
            _ttsControl = new TtsControl();
            this.name = name;
            Debug.WriteLine(name);
        }
        public string getName()
        {
            return name;
        }
    }
}
