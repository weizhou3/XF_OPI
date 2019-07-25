using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.PLCConnection
{
    public class RlistDataModel
    {
        public string Value { get; set; }
        public string AppCode { get; set; }

        public RlistDataModel(string value, string appCode)
        {
            Value = value;
            AppCode = appCode;
        }
    }
}
