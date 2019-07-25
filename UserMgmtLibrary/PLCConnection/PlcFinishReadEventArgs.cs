using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;

namespace XFOPI_Library.PLCConnection
{
    public class PlcFinishReadEventArgs : EventArgs
    {
        public bool Successed { get; set; }
        public string ExpMsgBit { get; set; }
        public string ExpMsgWord { get; set; }

        public long ticks { get; set; }
        public long _ms { get; set; }
        public long _ms1 { get; set; }
        public long _ms2 { get; set; }

        public PlcFinishReadEventArgs()
        {

        }
    }
}
