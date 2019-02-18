using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XFOPI_Library.PLCConnection
{
    /// <summary>
    /// The static class contains the connection and math methods used in Omron FINs communication
    /// </summary>
    public static class OmronFINsProcessor
    {
        /// <summary>
        /// Write FINS String to PLC via SerialPort
        /// </summary>
        /// <param name="TX">FINS String to be sent</param>
        /// <param name="PlcPort">Serial Port PLC is connected to</param>
        public static void SendPLC(string TX, System.IO.Ports.SerialPort PlcPort)
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

        /// <summary>
        /// Read PLC via SerialPort
        /// </summary>
        /// <param name="PlcPort">Serial Port address</param>
        /// <returns></returns>
        public static string ReadPLC(System.IO.Ports.SerialPort PlcPort)
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

        /// <summary>
        /// Generic write data string to PLC memory area
        /// </summary>
        /// <param name="MemoryArea">PLC memory area: CIO,WR,HR,AR,DM</param>
        /// <param name="xBeginAddress">The PLC hex address hhhh of first word to write</param>
        /// <param name="xEndAddress">The PLC hex address hhhh of last word to write</param>
        /// <param name="StoWrt">Data string to be sent</param>
        /// <param name="PlcPort">PLC serial port name</param>
        /// <returns>True: write successful False: Write failed</returns>
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
            count = Convert.ToString((Convert.ToInt32(xEndAddress, 16) - Convert.ToInt32(xBeginAddress, 16) + 1), 16).PadLeft(4, '0');
            xBeginAddress = xBeginAddress.PadRight(6, '0');
            try
            {
                SendPLC(OmronFINsClass.FINSh + OmronFINsClass.Wrt + MA + xBeginAddress + count + StoWrt, PlcPort);
                Thread.Sleep(10);
                string RXS = ReadPLC(PlcPort);
                if (RXS == "")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Generic read data string from PLC memory area
        /// </summary>
        /// <param name="MemoryArea">PLC memory area: CIO,WR,HR,AR,DM</param>
        /// <param name="xBeginAddress">The PLC hex address hhhh of first word to read</param>
        /// <param name="xEndAddress">The PLC hex address hhhh of last word to read</param>
        /// <param name="PlcPort">PLC serial port name</param>
        /// <returns>Data string read from PLC</returns>
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
            catch (Exception exp)
            {
                return null;
            }
        }

        /// <summary>
        /// Return the FCS check of a string
        /// </summary>
        /// <param name="S">String to be checked</param>
        /// <returns>FCS check result</returns>
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

        /// <summary>
        /// Convert Word string to string array, each index contains a Word
        /// </summary>
        /// <param name="S">Word string</param>
        /// <returns>Array of Words</returns>
        public static string[] ConvWordStrToWordArray(string S)
        {
            if (S.Length > 512*4)
            {
                return null;
            }
            else
            {
                string[] StrArray=new string[512];
                for (int i = 0; i < S.Length / 4 - 1; i++)
                {
                    StrArray[i] = S.Substring(4 + 4 * i, 4);
                }
                return StrArray;
            }

        }

        /// <summary>
        /// Convert Binary string to Hex string
        /// </summary>
        /// <param name="str2">Binary string</param>
        /// <returns>Hex string</returns>
        public static string HexConvert2(string str2)
        {
            string str16 = Convert.ToInt32(str2, 2).ToString("X");
            return str16;
        }

        /// <summary>
        /// Convert Decimal string to Hex string
        /// </summary>
        /// <param name="str10">Decimal string</param>
        /// <returns>Hex string</returns>
        public static string HexConvert10(string str10)
        {
            string str16 = Convert.ToInt32(str10, 10).ToString("X");
            return str16;
        }

        /// <summary>
        /// Convert Hex string to Binary string
        /// </summary>
        /// <param name="str16">Hex string</param>
        /// <returns>Binary string</returns>
        public static string BinaryConvert16(string str16)
        {
            //string str2 = Convert.ToInt64(str16, 16).ToString();
            string str2 = String.Join(String.Empty,
             str16.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            return str2;
        }

        /// <summary>
        /// Replace Char at S.index with newChar
        /// </summary>
        /// <param name="s">String to be modified</param>
        /// <param name="index">Index of the Char to be repalced</param>
        /// <param name="newChar">New Char</param>
        /// <returns></returns>
        public static string ReplaceAt(string s, int index, char newChar)//
        {
            char[] chars = s.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        /// <summary>
        /// Convert Binary string to ASCII string
        /// </summary>
        /// <param name="bin">Binary string</param>
        /// <returns>ASCII string</returns>
        public static string BinaryToASCII(string bin)
        {
            string ascii = string.Empty;

            for (int i = 0; i < bin.Length; i += 8)
            {
                ascii += (char)BinaryToDecimal(bin.Substring(i, 8));
            }

            return ascii;
        }

        /// <summary>
        /// Convert Binary to Decimal string
        /// </summary>
        /// <param name="bin">Binary string</param>
        /// <returns>Decimal string</returns>
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
