using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.Models;

namespace XFOPI_Library.PLCConnection
{
    public class PlcFinishWriteEventArgs : EventArgs
    {
        public List<PlcBitModel> BitsSent { get; set; } = new List<PlcBitModel>();
        public List<PlcWordModel> WordsSent { get; set; } = new List<PlcWordModel>();

        public bool Successed { get; set; }
        public string ExpMsgBit { get; set; }
        public string ExpMsgWord { get; set; }

        public long ticks { get; set; }
        public long _ms { get; set; }
        public long _ms1 { get; set; }
        public long _ms2 { get; set; }

        public PlcFinishWriteEventArgs()
        {

        }
    }
}
