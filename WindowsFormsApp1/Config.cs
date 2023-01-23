using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Config
    {
        public List<Chime> Chimes { get; set; }
        
        public Config()
        {
            this.Chimes = new List<Chime>();
        }

        public Config(List<Chime> Chimes)
        {
            this.Chimes = Chimes;
        }

    }
    public class Chime
    {
        public string Time { get; set; }
        public string Preset { get; set; }
        public string Message { get; set; }
        
        public Chime()
        {

        }
    }
}
