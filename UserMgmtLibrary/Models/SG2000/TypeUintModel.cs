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
        public uint Value { get; private set; }
        public string ValueStr { get; private set; }
        public string ValueHexStr { get; private set; }
        public string AppCode { get; set; }
        public string WrtOnStop { get; set; }
        public string WrtOnStart { get; set; }
        public string WrtOnRun { get; set; }
        //public bool WrtOnStop { get; set; }
        //public bool WrtOnStart { get; set; }
        //public bool WrtOnRun { get; set; }
       
        /// <summary>
        /// Set UInt Value
        /// </summary>
        /// <param name="Value">Uint value</param>
        public void SetValue(uint Value)
        {
            this.Value = Value;
            ValueStr = Value.ToString();
            ValueHexStr = OmronFINsProcessor.HexConvert10(Value.ToString()).PadLeft(8,'0');
        }

        public void SetValue(string ValueStr)
        {
            this.ValueStr = ValueStr;
            Value = uint.Parse(ValueStr);
            ValueHexStr = OmronFINsProcessor.HexConvert10(ValueStr).PadLeft(8,'0');
        }

        /// <summary>
        /// Set UInt Value
        /// </summary>
        /// <param name="HexStr"></param>
        public void SetValueHex(string HexStr)
        {
            //HexStr = HexStr.PadLeft(8, '0');
            ////if (HexStr.Length==8)
            ////{
            //    string HexStr_LSB = HexStr.Substring(0, 4);
            //    string HexStr_MSB = HexStr.Substring(4, 4);
            //    Value = uint.Parse(HexStr_MSB + HexStr_LSB, System.Globalization.NumberStyles.HexNumber);
            Value = uint.Parse(HexStr, System.Globalization.NumberStyles.HexNumber);
            ValueStr = Value.ToString();
                ValueHexStr = HexStr.PadLeft(8,'0');
                //string DecimalStr = OmronFINsProcessor.DecimalConvert16(hexStr_MSB + hexStr_LSB);

                //Value = Int32.Parse(OmronFINsProcessor.DecimalConvert16(hexStr_MSB+hexStr_LSB));
            //}
            
            
        }

        /// <summary>
        /// Set UInt Value 
        /// </summary>
        /// <param name="HexStr_LSB">low address Word</param>
        /// <param name="HexStr_MSB">high address Word</param>
        public void SetValueHex(string HexStr_LSB, string HexStr_MSB)
        {
            HexStr_LSB = HexStr_LSB.PadLeft(4, '0');
            HexStr_MSB = HexStr_MSB.PadLeft(4, '0');

            Value = uint.Parse(HexStr_MSB + HexStr_LSB, System.Globalization.NumberStyles.HexNumber);
            
        }
       

        /// <summary>
        /// Get the decimal Value and convert to Hex string that can be passed to PLC
        /// </summary>
        /// <returns>Hex string, Length: 2 words (32bit)</returns>
        public string GetValueHexStr()
        {
            //assume PLC addressing 0..511, where LSB before MSB. i.e. W0+W1=LSB+MSB
            string temp = OmronFINsProcessor.HexConvert10(Value.ToString()).PadLeft(8, '0');
            return temp.Substring(4, 4) + temp.Substring(0, 4);//return LSB + MSB (low address + high address)

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
