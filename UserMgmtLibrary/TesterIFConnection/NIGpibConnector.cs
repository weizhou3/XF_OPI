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

        public void RunTestSequence(TesterIFType iFType, TesterIFProtocol iFProtocol)
        {
            switch (iFProtocol)
            {
                case TesterIFProtocol.MTGPIB:
                    //runMt sequence
                    break;
                case TesterIFProtocol.RSGPIB:
                    //run RS sequence
                    break;
                case TesterIFProtocol.RSRS232:
                    break;
                case TesterIFProtocol.TTL:
                    break;
                default:
                    break;
            }
            
        }

        public bool SendToTester(string String)
        {
            //TODO -  add GPIB Read code
            throw new NotImplementedException();
        }

        public void StopTestSequence()
        {
            throw new NotImplementedException();
        }
    }
}
