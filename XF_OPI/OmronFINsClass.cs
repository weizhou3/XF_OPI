using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace XF_OPI
{
    class OmronFINsClass
    {
        public class PLCcommand//PLC command set
        {
            public string FINSh { get; }
            public string Wrt { get; }
            public string Rd { get; }
            public string DM { get; }
            public string WR { get; }
            public string HR { get; }
            public string AddrBegin { get; set; }
            public string AddrEnd { get; set; }

            public string[] SOT { get; set; }
            public string[] EOT { get; set; }
            public string[] BIN { get; set; }
            public string ComMode { get; set; }
            public string[] Temperature { get; set; }

            public string getMUBA { get; }
            public string getPLCD { get; }
            public string getPLCH { get; }
            public string getPLCW { get; }
            public string getPLCCW { get; }
            public string setPLCD { get; }
            public string setPLCH { get; }
            public string setPLCW { get; }
            public string setPLCW379 { get; }

            public string getComMode { get; }
            public string getPLCdata { get; }
            public string setPLCdata { get; }
            public string setPLCSOT { get; }
            public string setPLCBINEOT { get; }
            public string setPLCBIN { get; }


            public PLCcommand()
            {
                ComMode = "0000";
                //SOT = "0000000000000000";
                SOT = new string[4];
                //EOT = "0000000000000000";
                EOT = new string[4];
                BIN = new string[4] { "0000", "0000", "0000", "0000" };//BIN CS1,2,3,4
                Temperature = new string[10];
                DM = "DM";
                HR = "HR";
                WR = "WR";

                //PLC W area address range 000000 to 01FF00
                //FINS command: cmd codr + address + number of items + data
                FINSh = "@00FA000000000";//unit# 00, header code = FA, response time = 0x10ms, ICF, DA2, SA2, SID
                Wrt = "0102";
                Rd = "0101";
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
            }

        }
        PLCcommand PLCcmd = new PLCcommand();

        public bool GenericWrtPLC(string MemoryArea, string xBeginAddress, string xEndAddress, string StoWrt, System.IO.Ports.SerialPort PlcPort)
        {
            string MA = null;
            string count = null;
            switch (MemoryArea)
            {
                case "CIO":
                    MA = "B0";
                    break;
                case "WR":
                    MA = "B1";
                    break;
                case "HR":
                    MA = "B2";
                    break;
                case "AR":
                    MA = "B3";
                    break;
                case "DM":
                    MA = "82";
                    break;
            }
            count = Convert.ToString((Convert.ToInt32(xEndAddress, 16) - Convert.ToInt32(xBeginAddress, 16)), 16).PadRight(6, '0');
            try
            {
                SendPLC(PLCcmd.FINSh + PLCcmd.Wrt + MA + xBeginAddress + count + StoWrt, PlcPort);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GenericRdPLC(string MemoryArea, string xBeginAddress, string xEndAddress, System.IO.Ports.SerialPort PlcPort)
        {
            string MA = null;
            string count = null;
            switch (MemoryArea)
            {
                case "CIO":
                    MA = "B0";
                    break;
                case "WR":
                    MA = "B1";
                    break;
                case "HR":
                    MA = "B2";
                    break;
                case "AR":
                    MA = "B3";
                    break;
                case "DM":
                    MA = "82";
                    break;
            }
            count = Convert.ToString((Convert.ToInt32(xEndAddress, 16) - Convert.ToInt32(xBeginAddress, 16) + 1), 16).PadLeft(4, '0');
            xBeginAddress = xBeginAddress.PadRight(6, '0');
            try
            {
                SendPLC(PLCcmd.FINSh + PLCcmd.Rd + MA + xBeginAddress + count, PlcPort);
                Thread.Sleep(10);
                string result = ReadPLC(PlcPort);
                if (result == null)
                    return null;
                else
                    return result;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetFCS(string S) //check frame check sequence
        {
            int L;
            //int i=0;
            int A;
            string FCS;
            string TJ;
            L = S.Length;
            A = 0;
            for (int i = 0; i < L; i++)
            {
                TJ = S.Substring(i, 1);
                A = (int)TJ[0] ^ A;
            }
            FCS = A.ToString("X");
            if (FCS.Length == 1)
            {
                FCS = "0" + FCS;
            }

            return FCS;
        }
        public void SendPLC(string TX, System.IO.Ports.SerialPort PlcPort)//, string FCS)//send data to PLC
        {
            string BufferTX;
            string FCS;
            //BufferTX = TX + FCS + "*" + '\r'; //C-MODE
            FCS = GetFCS(TX);
            BufferTX = TX + FCS + "*" + '\r';

            try
            {
                PlcPort.Write(BufferTX);
            }
            catch (Exception)
            {
                
            }
        }

        public string ReadPLC(System.IO.Ports.SerialPort PlcPort)//read PLC response
        {
            string FCS_rxd;
            string FCS = "";
            string RXD = "";
            string data = "";

            try
            {
                RXD = "";
                data = "";
                PlcPort.ReadTimeout = 1000;
                RXD = PlcPort.ReadTo("\r");
            }
            catch (Exception exp)
            {
                return null;
            }

            FCS_rxd = RXD.Substring(RXD.Length - 3, 2);
            if (RXD.Substring(0, 1) == "@" && RXD.Length > 3) //response has data
            {
                RXD = RXD.Substring(0, RXD.Length - 3);
            }
            /*else if (RXD.Substring(0, 1) == "@" && RXD.Length == 11)//reponse has no data but error
            {
                MessageBox.Show("PLC comm error");
                return data;
            }*/
            else if (RXD == "")
            {
                return null;
            }

            FCS = GetFCS(RXD);
            if (FCS == FCS_rxd)
            {
                data = RXD.Substring(23, RXD.Length - 23);
                return data;
            }
            else
            {
                return null;
            }
        }

        public string HexConvert2(string str2)//convert binary string to hex string
        {
            string str16 = Convert.ToInt32(str2, 2).ToString("X");
            return str16;
        }
        public string HexConvert10(string str10)//convert decimal string to hex string
        {
            string str16 = Convert.ToInt32(str10, 10).ToString("X");
            return str16;
        }
        public string BinaryConvert16(string str16)//convert Hexdecimal string to binary string
        {
            //string str2 = Convert.ToInt64(str16, 16).ToString();
            string str2 = String.Join(String.Empty,
             str16.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            return str2;
        }
        public string ReplaceAt(string s, int index, char newChar)//replace char at s.index with newChar
        {
            char[] chars = s.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
        public string BinaryToASCII(string bin)
        {
            string ascii = string.Empty;

            for (int i = 0; i < bin.Length; i += 8)
            {
                ascii += (char)BinaryToDecimal(bin.Substring(i, 8));
            }

            return ascii;
        }
        private int BinaryToDecimal(string bin)
        {
            int binLength = bin.Length;
            double dec = 0;

            for (int i = 0; i < binLength; ++i)
            {
                dec += ((byte)bin[i] - 48) * Math.Pow(2, ((binLength - i) - 1));
            }

            return (int)dec;
        }


    }
}
