using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Visa;

namespace XFOPI_Library.TesterIFConnection
{
    public class NIGpibConnector : ITesterIFConnection
    {
  
        public string Protocol { get; set; }
        public string ReadFromTester()
        {
            //TODO -  add GPIB Write code
            throw new NotImplementedException();
        }

        public bool SendToTester(string String)
        {
            //TODO -  add GPIB Read code
            throw new NotImplementedException();
        }
    }
}
