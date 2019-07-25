using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.PLCConnection;

namespace XFOPI_Library.Models.SG2000
{
    public class TypeUshortModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ValueHexStr { get; private set; }
        public string ValueStr { get; private set; }
        public ushort Value { get; private set; }
        public string AppCode { get; set; }
        public string WrtOnStop { get; set; }
        public string WrtOnStart { get; set; }
        public string WrtOnRun { get; set; }
        //public bool WrtOnStop { get; set; }
        //public bool WrtOnStart { get; set; }
        //public bool WrtOnRun { get; set; }
        public void SetValue(ushort Value)
        {
            this.Value = Value;
            ValueHexStr = OmronFINsProcessor.HexConvert10(Value.ToString()).PadLeft(4,'0');
            ValueStr = Value.ToString();
        }

        public void SetValue(string ValueStr)
        {
            this.ValueStr = ValueStr;
            Value = ushort.Parse(ValueStr);
            ValueHexStr = OmronFINsProcessor.HexConvert10(ValueStr).PadLeft(4,'0');
        }

        public void SetValueHex(string ValueHexStr)
        {
            //ValueHexStr = ValueHexStr.PadLeft(4, '0');
            Value = ushort.Parse(ValueHexStr, System.Globalization.NumberStyles.HexNumber);
            ValueStr = Value.ToString();
            this.ValueHexStr = ValueHexStr.PadLeft(4,'0');
            //this.Value = int.Parse(OmronFINsProcessor.DecimalConvert16(ValueStr));
        }


        /// <summary>
        /// Get the decimal Value and convert to Hex string that can be passed to PLC
        /// </summary>
        /// <returns>Hex string, Length: 1 word (16bit)</returns>
        public string GetValueHexStr()
        {
            return OmronFINsProcessor.HexConvert10(Value.ToString()).PadLeft(4, '0');
        }

        /// <summary>
        /// Full Ctor
        /// </summary>
        /// <param name="Name">Name of the TypeUshort</param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        /// <param name="Value"></param>
        public TypeUshortModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun, ushort Value)
        {
            this.Name = Name;
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
            SetValue(Value);
        }

        /// <summary>
        /// Ctor with name and write permission
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        public TypeUshortModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun)
        {
            this.Name = Name;
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
            SetValue(0);
        }

        public TypeUshortModel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Default constructor, the default Name is No Name, Value is 0,
        /// </summary>
        public TypeUshortModel()
        {
            this.Name = "No Name";
            WrtOnRun = "N";
            WrtOnStart = "N";
            WrtOnStop = "N";
            //WrtOnRun = false;
            //WrtOnStart = false;
            //WrtOnStop = false;
            SetValue(0);
        }

        private void SetWrtPermission(string WrtOnStop, string WrtOnStart, string WrtOnRun)
        {
            if (String.Equals("Y", WrtOnStop, StringComparison.OrdinalIgnoreCase))
                this.WrtOnStop = "Y";
            else this.WrtOnStop = "N";

            if (String.Equals("Y", WrtOnStart, StringComparison.OrdinalIgnoreCase))
                this.WrtOnStart = "Y";
            else this.WrtOnStart = "N";

            if (String.Equals("Y", WrtOnRun, StringComparison.OrdinalIgnoreCase))
                this.WrtOnRun = "Y";
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

