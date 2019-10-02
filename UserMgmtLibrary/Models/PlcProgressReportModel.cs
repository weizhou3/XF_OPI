using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.PLCConnection;

namespace XFOPI_Library.Models
{
    public class PlcProgressReportModel
    {
        public int PercetageComplete { get; set; } = 0;
        public int CountedNumber { get; set; } = 0;
        public string Msg { get; set; } = null;
        public Dictionary<string, RlistDataModel> Rlist { get; set; } = new Dictionary<string, RlistDataModel>();
        public Dictionary<string, string> Wlist { get; set; } = new Dictionary<string, string>();
        public bool WrtCompleted { get; set; } = false;
        public bool ReadCompleted { get; set; } = false;
        public void ClrReport()
        {
            PercetageComplete = 0;
            CountedNumber = 0;
            Msg = "";
            Rlist.Clear();
            Wlist.Clear();
            WrtCompleted = false;
            ReadCompleted = false;
        }

    }
}
