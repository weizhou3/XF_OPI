using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace XF_OPI
{
    class DataIO
    {
        public struct FtpSetting
        {
            public string Server { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string FileName { get; set; }
            public string FullName { get; set; }
            public bool Upload { get; set; }//0: download  1: upload
            public string Localdest { get; set; }
        }


        public void WriteToFile(string sOutputFileName, string sToWrite)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(sOutputFileName, true))
            {
                file.WriteLine(sToWrite);
                
            }
        }

        public string[] ReadFromFile(string sFileName)
        {
            string[] result;
            result = File.ReadAllLines(sFileName);
            return result;
        }

    }
}
