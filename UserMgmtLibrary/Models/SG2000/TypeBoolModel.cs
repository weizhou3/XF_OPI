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
        public bool State { get; set; }
        public bool WrtOnStop { get; set; }
        public bool WrtOnStart { get; set; }
        public bool WrtOnRun { get; set; }

        public void SetState(bool State)
        {
            this.State = State;
        }

        /// <summary>
        /// Get the current Bool State as String character
        /// </summary>
        /// <returns>String charater, 1(ON) or 0(OFF)</returns>
        public string GetStateStr()
        {
            if (State)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// Full ctor
        /// </summary>
        /// <param name="Name">Name of TypeBool</param>
        /// <param name="WrtOnStop">If able to write on STOP</param>
        /// <param name="WrtOnStart">If able to write on START</param>
        /// <param name="WrtOnRun">If able to write on RUN</param>
        /// <param name="State">State of TypeBool</param>
        public TypeBoolModel(string Name, string WrtOnStop, string WrtOnStart, string WrtOnRun, bool State)
        {
            this.Name = Name;
            SetWrtPermission(WrtOnStop, WrtOnStart, WrtOnRun);
            SetState(State);
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
            SetState(false);
        }

        /// <summary>
        /// default set to no name and all writable is false
        /// </summary>
        public TypeBoolModel()
        {
            this.Name = "No Name";
            SetState(false);
            WrtOnRun = false;
            WrtOnStart = false;
            WrtOnStop = false;
        }

        private void SetWrtPermission(string WrtOnStop, string WrtOnStart, string WrtOnRun)
        {
            if (String.Equals("Y", WrtOnStop, StringComparison.OrdinalIgnoreCase))
                this.WrtOnStop = true;
            else this.WrtOnStop = false;

            if (String.Equals("Y", WrtOnStart, StringComparison.OrdinalIgnoreCase))
                this.WrtOnStart = true;
            else this.WrtOnStart = false;

            if (String.Equals("Y", WrtOnRun, StringComparison.OrdinalIgnoreCase))
                this.WrtOnRun = true;
            else this.WrtOnRun = false;
        }
    }
}
