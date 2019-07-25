using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class PlcBitAddressMappingModel
    {
        public string DataName { get; set; }
        public PlcBitModel Bit { get; set; }

        public PlcBitAddressMappingModel(string name, PlcBitModel bitModel)
        {
            DataName = name;
            Bit = bitModel;
        }
        public PlcBitAddressMappingModel()
        {

        }
    }
}
