using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    /// <summary>
    /// Directly import from DB, to hold a single record of DataName <> PlcAddress string relationship
    /// </summary>
    public class PlcDataAddressRecordModel
    {
        public int id { get; set; }
        public string DataName { get; set; }
        public string PlcAddress { get; set; }
    }
}
