using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.TesterIFConnection
{
    public interface ITesterIFConnection
    {
        /// <summary>
        /// Send the SOT string to tester
        /// </summary>
        /// <param name="String"></param>
        /// <returns>Sent successful or not</returns>
        bool SendToTester(string String);

        /// <summary>
        /// Read test results from tester
        /// </summary>
        /// <returns>Tester replied string</returns>
        string ReadFromTester();

        /// <summary>
        /// Start the test communication cycle
        /// </summary>
        /// <param name="iFType">Tester interface type</param>
        /// <param name="iFProtocol">Tester interface protocol</param>
        void RunTestSequence(TesterIFType iFType,TesterIFProtocol iFProtocol);

        /// <summary>
        /// Stop and abort the current test cycle 
        /// </summary>
        void StopTestSequence();
    }
}
