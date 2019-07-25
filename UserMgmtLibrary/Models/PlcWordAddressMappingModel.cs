using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    public class PlcWordAddressMappingModel
    {
        public string DataName { get; set; }
        public PlcWordModel Word { get; set; }

        public PlcWordAddressMappingModel(string name, PlcWordModel wordModel)
        {
            DataName = name;
            Word = wordModel;
        }
        public PlcWordAddressMappingModel()
        {

        }
    }
}
