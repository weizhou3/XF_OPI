using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    /// <summary>
    /// to hold a single line record of DataName <> PlcAddress String <> PlcAddress in RAM
    /// </summary>
    public class PlcDataAddressMappingModel
    {
        public PlcDataAddressRecordModel PlcDataAddressRecord { get; set; }
        public PlcAddressModel PlcAddress { get; set; }



        public PlcDataAddressMappingModel()
        {
            PlcAddress = new PlcAddressModel();
        }
    }
}
