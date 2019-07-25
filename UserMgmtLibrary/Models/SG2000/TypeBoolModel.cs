using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models.SG2000
{
    public class TypeBoolModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Value { get; private set; }
        public string ValueStr { get; private set; }
        public string ValueHexStr { get; private set; }
        public string AppCode { get; set; }
        //private bool _value { get; set; }
        public string WrtOnStop { get; set; }
        public string WrtOnStart { get; set; }
        public string WrtOnRun { get; set; }
        //public bool WrtOnStop { get; set; }
        //public bool WrtOnStart { get; set; }
        //public bool WrtOnRun { get; set; }

        /// <summary>
        /// Set the value of the TypeBool 1 or 0
        /// </summary>
        /// <param name="Value">Int Value 1 or 0</param>
        public void SetValue(int Value)
        {
            if (Value==1||Value==0)
            {
                this.Value = Value;
                ValueStr = Value.ToString();
                ValueHexStr = ValueStr;
            }
        }

        public void SetValue(string ValueStr)
        {
            int Value = int.Parse(ValueStr);
            if (Value==0||Value==1)
            {
                this.Value = Value;
                this.ValueStr = ValueStr;
                ValueHexStr = ValueStr;
            }
        }

        /// <summary>
        /// Get the current Bool State as String character
        /// </summary>
        /// <returns>String charater, 1(ON) or 0(OFF)</returns>
        public string GetValueStr()
        {
            if (Value==1)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public TypeBoolModel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Full ctor
        /// </summary>
        /// <param name="Name">Name of TypeBool</param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        /// <param name="Value">Value of TypeBool</param>
        public TypeBoolModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun, int Value)
        {
            this.Name = Name;
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
            SetValue(Value);
        }

        /// <summary>
        /// Ctor without State
        /// </summary>
        /// <param name="Name">Name of TypeBool</param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        public TypeBoolModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun)
        {
            this.Name = Name;
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
            SetValue(0);
        }

        /// <summary>
        /// default set to no name and all writable is false
        /// </summary>
        public TypeBoolModel()
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
