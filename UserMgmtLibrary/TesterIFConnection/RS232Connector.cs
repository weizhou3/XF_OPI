using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XFOPI_Library.TesterIFConnection
{
    public class RS232Connector : ITesterIFConnection
    {
        public string Protocol { get; set; }
        public string ReadFromTester()
        {
            throw new NotImplementedException();
        }

        public bool SendToTester(string String)
        {
            throw new NotImplementedException();
        }
    }
}
