using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XFOPI_Library.PLCConnection
{
    public static class OmronFINsProcessor
    {
        public static void SendPLC(string TX, System.IO.Ports.SerialPort PlcPort)//, string FCS)//send data to PLC
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

        public static string ReadPLC(System.IO.Ports.SerialPort PlcPort)//read PLC response
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
            catch (Exception)
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

        public static bool GenericWrtPLC(string MemoryArea, string xBeginAddress, string xEndAddress, string StoWrt, System.IO.Ports.SerialPort PlcPort)
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
                SendPLC(OmronFINsClass.FINSh + OmronFINsClass.Wrt + MA + xBeginAddress + count + StoWrt, PlcPort);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GenericRdPLC(string MemoryArea, string xBeginAddress, string xEndAddress, System.IO.Ports.SerialPort PlcPort)
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
                SendPLC(OmronFINsClass.FINSh + OmronFINsClass.Rd + MA + xBeginAddress + count, PlcPort);
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

        public static string GetFCS(string S) //check frame check sequence
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

        public static string HexConvert2(string str2)//convert binary string to hex string
        {
            string str16 = Convert.ToInt32(str2, 2).ToString("X");
            return str16;
        }

        public static string HexConvert10(string str10)//convert decimal string to hex string
        {
            string str16 = Convert.ToInt32(str10, 10).ToString("X");
            return str16;
        }

        public static string BinaryConvert16(string str16)//convert Hexdecimal string to binary string
        {
            //string str2 = Convert.ToInt64(str16, 16).ToString();
            string str2 = String.Join(String.Empty,
             str16.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            return str2;
        }

        public static string ReplaceAt(string s, int index, char newChar)//replace char at s.index with newChar
        {
            char[] chars = s.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public static string BinaryToASCII(string bin)
        {
            string ascii = string.Empty;

            for (int i = 0; i < bin.Length; i += 8)
            {
                ascii += (char)BinaryToDecimal(bin.Substring(i, 8));
            }

            return ascii;
        }

        public static int BinaryToDecimal(string bin)
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
