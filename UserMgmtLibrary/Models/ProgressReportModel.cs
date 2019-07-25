using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class ProgressReportModel
    {
        public int PercetageComplete { get; set; } = 0;
        public int CountedNumber { get; set; } = 0;
        public string Msg { get; set; } = null;
        public Dictionary<string, string> Wlist { get; set; } = new Dictionary<string, string>();
        public bool WrtCompleted { get; set; } = false;
        public bool ReadCompleted { get; set; } = false;

    }
}
