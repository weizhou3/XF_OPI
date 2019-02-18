using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.Models.SG2000
{
    public class Sensor
    {
        public String Name { get; set; }
        public bool State { get; set; }
        public void SetState(bool State)
        {
            this.State = State;   
        }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="Name">Name of the sensor</param>
        /// <param name="State">State of the sensor True(ON) False(OFF)</param>
        public Sensor(string Name, bool State)
        {
            this.Name = Name;
            SetState(State);
        }

        /// <summary>
        /// Constructor with Name only, the default State is False(OFF)
        /// </summary>
        /// <param name="Name">Name of the sensor</param>
        public Sensor(string Name)
        {
            this.Name = Name;
            SetState(false);
        }

        /// <summary>
        /// Default constructor, the default Name is No Name, State is False(OFF)
        /// </summary>
        public Sensor()
        {
            this.Name = "No Name";
            SetState(false);
        }
    }
}
