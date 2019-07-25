using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    /// <summary>
    /// Holds the Word Address information
    /// </summary>
    public class PlcWordAddressModel
    {
        public string MemoryArea { get; set; }
        public int WordAddress { get; set; }
        public int? BitAddress { get; set; }

        public PlcWordAddressModel()
        {
            BitAddress = null;
        }

        public PlcWordAddressModel(string MemoryArea, int WordAddress, int? BitAddress )
        {
            this.MemoryArea = MemoryArea;
            this.WordAddress = WordAddress;
            this.BitAddress = BitAddress;
        }
    }
}
