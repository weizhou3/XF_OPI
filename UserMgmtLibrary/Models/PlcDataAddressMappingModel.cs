using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models
{
    /// <summary>
    /// The complete address mapping model, = PlcDataAddressRecordModel + PlcWordAddressModel
    /// to hold a single line record of DataName <> PlcAddress String <> PlcAddress in RAM    
    /// </summary>
    public class PlcDataAddressMappingModel
    {
        public PlcDataAddressRecordModel PlcDataAddressRecord { get; set; }
        public PlcWordAddressModel PlcWordAddress { get; set; }
        
        public PlcDataAddressMappingModel()
        {
            PlcWordAddress = new PlcWordAddressModel();
        }

        public PlcDataAddressMappingModel ShallowCopy()
        {
            return (PlcDataAddressMappingModel) this.MemberwiseClone();
        }

        public PlcDataAddressMappingModel DeepCopy()
        {
            PlcDataAddressMappingModel other = (PlcDataAddressMappingModel)this.MemberwiseClone();
            other.PlcDataAddressRecord = new PlcDataAddressRecordModel(PlcDataAddressRecord.id, PlcDataAddressRecord.DataName, PlcDataAddressRecord.PlcAddress);
            other.PlcWordAddress = new PlcWordAddressModel(PlcWordAddress.MemoryArea, PlcWordAddress.WordAddress, PlcWordAddress.BitAddress);
            return other;
        }
    }
}
