using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace XFOPI_Library.Models
{
    public class PlcPortSettingModel
    {
        //private int _id;
        //private string _name;
        //private int _baudRate;
        //private int _dataBits;
        //private Parity _parity;
        //private string _portName;
        //private StopBits _stopBits;

        public int ID { get; set; }
        public string Name { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public string PortName { get; set; }
        public StopBits StopBits { get; set; }
        //public int ID { get { return _id; } }
        //public string Name { get { return _name; }set { _name = Name; } }
        //public string BaudRate { get { return _baudRate.ToString(); }; set; }
        //public string DataBits { get; set; }
        //public string Parity { get; set; }
        //public string PortName { get; set; }
        //public string StopBits { get; set; }
        //public string Name { get; set; }
        //public string BaudRate { get; set; }
        //public string DataBits { get; set; }
        //public string Parity { get; set; }
        //public string PortName { get; set; }
        //public string StopBits { get; set; }

        //public PlcPortSettingModel(string name, int baudRate, int dataBits, Parity parity, string portName, StopBits stopBits)
        //{
        //    _name = name;
        //    _baudRate = baudRate;
        //    _dataBits = dataBits;
        //    _parity = parity;
        //    _portName = portName;
        //    _stopBits = stopBits;
        //}
        public PlcPortSettingModel()
        {

        }

        //public void SetAllFields()
        //{
        //    _name = Name;
        //    _baudRate = BaudRate;
        //    _dataBits = DataBits;
        //    _parity = Parity;
        //    _portName = PortName;
        //    _stopBits = StopBits;
        //}


    }
}
