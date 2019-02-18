using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.PLCConnection;

namespace XFOPI_Library.Models.SG2000
{
    public class TypeUintModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public uint Value { get; set; }
        public string WrtOnStop { get; set; }
        public string WrtOnStart { get; set; }
        public string WrtOnRun { get; set; }
        //public bool WrtOnStop { get; set; }
        //public bool WrtOnStart { get; set; }
        //public bool WrtOnRun { get; set; }
       

        public void SetValue(uint Value)
        {
            this.Value = Value;
        }
       

        /// <summary>
        /// Get the decimal Value and convert to Hex string that can be passed to PLC
        /// </summary>
        /// <returns>Hex string, Length: 2 words (32bit)</returns>
        public string GetValueHexStr()
        {
            //assume PLC addressing 0..511, where LSB before MSB. i.e. W0+W1=LSB+MSB
            string temp = OmronFINsProcessor.HexConvert10(Value.ToString()).PadLeft(8, '0');
            return temp.Substring(4, 4) + temp.Substring(0, 4);//sawp LSB and MSB
            //TODO: need to verify in PLC the 2words addressing is correct

            //assume PLC addressing 0..511, where MSB before LSB. i.e. W1+W0=LSB+MSB
            //return OmronFINsProcessor.HexConvert10(Value.ToString()).PadLeft(8, '0');
        }

        /// <summary>
        /// Full Ctor
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        /// <param name="Value">Value of the TypeUint</param>
        public TypeUintModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun, uint Value)
        {
            this.Name = Name;
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
            SetValue(Value);
            
            
        }

        /// <summary>
        /// Ctor without Value
        /// </summary>
        /// <param name="Name">Name of the TypeUint</param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        public TypeUintModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun)
        {
            this.Name = Name;
            SetValue(0);
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
        }

        /// <summary>
        /// Constructor with Name only, the default Value is 0, Write Permission is N, Read Permission is R
        /// </summary>
        /// <param name="Name">Name of the int Type value</param>
        public TypeUintModel(string Name)
        {
            this.Name = Name;
            SetValue(0);
            WrtOnRun = "N";
            WrtOnStart = "N";
            WrtOnStop = "N";
            //WrtOnRun = false;
            //WrtOnStart = false;
            //WrtOnStop = false;
        }

        /// <summary>
        /// Default constructor, the default Name is No Name, Value is 0, Write Permission is N, Read Permission is R
        /// </summary>
        public TypeUintModel()
        {
            this.Name = "No Name";
            SetValue(0);
            WrtOnRun = "N";
            WrtOnStart = "N";
            WrtOnStop = "N";
            //WrtOnRun = false;
            //WrtOnStart = false;
            //WrtOnStop = false;
        }

        private void SetWrtPermission(string WrtOnStop, string WrtOnStart, string WrtOnRun)
        {
            if (String.Equals("Y", WrtOnStop, StringComparison.OrdinalIgnoreCase))
                this.WrtOnStop = WrtOnStop;
            else this.WrtOnStop = "N";

            if (String.Equals("Y", WrtOnStart, StringComparison.OrdinalIgnoreCase))
                this.WrtOnStart = WrtOnStart;
            else this.WrtOnStart = "N";

            if (String.Equals("Y", WrtOnRun, StringComparison.OrdinalIgnoreCase))
                this.WrtOnRun = WrtOnRun;
            else this.WrtOnRun = "N";
            //if (String.Equals("Y", WrtOnStop, StringComparison.OrdinalIgnoreCase))
            //    this.WrtOnStop = true;
            //else this.WrtOnStop = false;

            //if (String.Equals("Y", WrtOnStart, StringComparison.OrdinalIgnoreCase))
            //    this.WrtOnStart = true;
            //else this.WrtOnStart = false;

            //if (String.Equals("Y", WrtOnRun, StringComparison.OrdinalIgnoreCase))
            //    this.WrtOnRun = true;
            //else this.WrtOnRun = false;
        }
    }
}
