﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFOPI_Library.TesterIFConnection
{
    public static class MTGpibProcessor
    {
        
        private static string[] _SQBByte;

        public static string[] SQBByte
        {
            get { return _SQBByte; }
            set { _SQBByte = new string[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?" }; }
        }

        public static int[] DUT_CS { get; set; }



    }
}
