using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class PlcAddressModel
    {
        public string MemoryArea { get; set; }
        public int WordAddress { get; set; }
        public int? BitAddress { get; set; }

        public PlcAddressModel()
        {
            BitAddress = null;
        }
    }
}
