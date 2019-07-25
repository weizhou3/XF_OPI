﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using XFOPI_Library.PLCConnection



namespace XFOPI_Library
{
    /// <summary>
    /// A static class stores Constants used in Omron FINs communication 
    /// </summary>
    public static class OmronFINsClass
    {
        public static int Size_WR = 512;
        public static int StartingId_WR = 0;
        public static int Size_HR = 512;
        public static int StartingId_HR = 0;
        public static int Size_DM = 1000;//Max size is 32768
        public static int StartingId_DM = 17000;//0~38768
        public const string MemArea_WR = "W";
        public const string MemArea_HR = "H";
        public const string MemArea_DM = "D";
        public const int Width_Word = 16;
        public const string FINSh = "@00FA000000000";//unit# 00, header code = FA, response time = 0x10ms, ICF, DA2, SA2, SID
        public const string Wrt = "0102";
        public const string Rd = "0101";
        public const string DM = "82";//DM Area, Word address D00000-D32767/000000-7FFF00
        public const string WR = "B1";//Work Area, Word address W000-W511/000000-01FF00
        public const string WR_bit = "31";//Work Area, Bit address W00000-W51115/000000-01FF0F
        public const string HR = "B2";//Holding Area, Word address H000-H511/000000-01FF00
        public const string HR_bit = "32";//Holding Area, Bit address H00000-H51115/000000-01FF0F

        public const string getMUBA = FINSh + Rd + DM + "16A900" + "0023";//D5801~5835
        public const string getPLCD = FINSh + "010182271100000A";//D10001~10010: temperature, 10 words
        public const string getPLCH = FINSh + "0101B201AE000001";//H430: Comm mode, 1 word 
        public const string getPLCW = FINSh + "0101B1017C00000C";

        //The max read per FINS cmd is 498 words
        public const string GetTop300_B1 = FINSh + Rd + WR +"000000"+"012C";//read W0~W299
        public const string GetBottom_B1 = FINSh + Rd + WR + "012C00" + "00D4";//read W300~W511

        public const string GetTop300_B2 = FINSh + Rd + HR + "000000" + "012C";//read H0~H299
        public const string GetBottom_B2 = FINSh + Rd + HR + "012C00" + "00D4";//read H300~H511

        public const string getPLCCW = FINSh + "0101B1017B000005"; //W area W379~W383, W379 is the control unit
        public const string setPLCW379 = FINSh + "0102B1017B000001";//W379 1 word
        public const string setPLCW = FINSh + "0102B10180000008";//W384~391: BIN EOT, 8 words
        public const string getComMode = FINSh + "0101B201AE000001"; //H area H430.01
        public const string getPLCdata = FINSh + "0101B1017C00000C"; //W area W380~W391    //"@00RD00030001"; //c-mode
        //setPLCdata = FINSh + "0102B1017C00000C" + SOT1 + SOT2 + SOT3 + SOT4 + EOT1 + EOT2 + EOT3 + EOT4 + BIN1 + BIN2 + BIN3 + BIN4; 
        public const string setPLCdata = FINSh + "0102B1017C00000C";//set SOT EOT BIN
        public const string setPLCSOT = FINSh + "0102B1017C000004";//set SOT
        public const string setPLCBINEOT = FINSh + "0102B10180000008";//set BIN and EOT   
        public const string setPLCBIN = FINSh + "0102B10180000004";//set BIN 
        

        //public string AddrBegin { get; set; }
        //public string AddrEnd { get; set; }

        //public string[] SOT { get; set; }
        //public string[] EOT { get; set; }
        //public string[] BIN { get; set; }
        //public string ComMode { get; set; }
        //public string[] Temperature { get; set; }

        //public string getMUBA { get; }
        //public string getPLCD { get; }
        //public string getPLCH { get; }
        //public string getPLCW { get; }
        //public string getPLCCW { get; }
        //public string setPLCD { get; }
        //public string setPLCH { get; }
        //public string setPLCW { get; }
        //public string setPLCW379 { get; }

        //public string getComMode { get; }
        //public string getPLCdata { get; }
        //public string setPLCdata { get; }
        //public string setPLCSOT { get; }
        //public string setPLCBINEOT { get; }
        //public string setPLCBIN { get; }

        /*public OmronFINsClass()
        {
            ComMode = "0000";
            //SOT = "0000000000000000";
            SOT = new string[4];
            //EOT = "0000000000000000";
            EOT = new string[4];
            BIN = new string[4] { "0000", "0000", "0000", "0000" };//BIN CS1,2,3,4
            Temperature = new string[10];


            //PLC W area address range 000000 to 01FF00
            //FINS command: cmd codr + address + number of items + data

            AddrBegin = null;
            AddrEnd = null;

            getMUBA = FINSh + Rd + DM + "16A900" + "0023";//D5801~5835
            getPLCD = FINSh + "010182271100000A";//D10001~10010: temperature, 10 words
            getPLCH = FINSh + "0101B201AE000001";//H430: Comm mode, 1 word 
            getPLCW = FINSh + "0101B1017C00000C";
            getPLCCW = FINSh + "0101B1017B000005"; //W area W379~W383, W379 is the control unit
            setPLCW379 = FINSh + "0102B1017B000001";//W379 1 word
            setPLCW = FINSh + "0102B10180000008";//W384~391: BIN EOT, 8 words
            getComMode = FINSh + "0101B201AE000001"; //H area H430.01
            getPLCdata = FINSh + "0101B1017C00000C"; //W area W380~W391    //"@00RD00030001"; //c-mode
            //setPLCdata = FINSh + "0102B1017C00000C" + SOT1 + SOT2 + SOT3 + SOT4 + EOT1 + EOT2 + EOT3 + EOT4 + BIN1 + BIN2 + BIN3 + BIN4; 
            setPLCdata = FINSh + "0102B1017C00000C";//set SOT EOT BIN
            setPLCSOT = FINSh + "0102B1017C000004";//set SOT
            setPLCBINEOT = FINSh + "0102B10180000008";//set BIN and EOT   
            setPLCBIN = FINSh + "0102B10180000004";//set BIN 
        }*/


        //PLCcommand PLCcmd = new PLCcommand();



    }
}
