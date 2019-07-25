﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XFOPI_Library.Models;
using System.IO.Ports;
using System.Diagnostics;

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
            Stopwatch stopwatch = new Stopwatch();
            string BufferTX;
            string FCS;
            //BufferTX = TX + FCS + "*" + '\r'; //C-MODE
            FCS = GetFCS(TX);
            BufferTX = TX + FCS + "*" + '\r';

            try
            {
                //stopwatch.Start();
                PlcPort.Write(BufferTX);//<30ms
                //stopwatch.Stop();
            }
            catch (Exception exp)
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

            Stopwatch stopwatch = new Stopwatch();

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
        public static bool GenericWrtPLC(PlcMemArea MemoryArea, string BeginAddress, string EndAddress, string StoWrt, SerialPort PlcPort, out string expMsg)
        {
            string MA = null;
            string count = null;
            switch (MemoryArea)
            {
                case PlcMemArea.CIO:
                    MA = "B0";
                    break;
                case PlcMemArea.WR:
                    MA = OmronFINsClass.WR;
                    break;
                case PlcMemArea.WR_bit:
                    MA = OmronFINsClass.WR_bit;
                    break;
                case PlcMemArea.HR:
                    MA = "B2";
                    break;
                case PlcMemArea.AR:
                    MA = "B3";
                    break;
                case PlcMemArea.DM:
                    MA = "82";
                    break;
            }
            //count = Convert.ToString((Convert.ToInt32(xEndAddress, 16) - Convert.ToInt32(xBeginAddress, 16) + 1), 16).PadLeft(4, '0');
            count = HexConvert10((int.Parse(EndAddress) - int.Parse(BeginAddress) + 1).ToString()).PadLeft(4,'0');
            //xBeginAddress = xBeginAddress.PadRight(6, '0');
            string xBeginAddress = HexConvert10(BeginAddress).PadLeft(4, '0').PadRight(6, '0');
            try
            {
                //Stopwatch stopwatch = new Stopwatch();
                
                SendPLC(OmronFINsClass.FINSh + OmronFINsClass.Wrt + MA + xBeginAddress + count + StoWrt, PlcPort);//<80ms
                
                //Thread.Sleep(10);
                //stopwatch.Start();
                string RXS = ReadPLC(PlcPort);
                //stopwatch.Stop();
                if (RXS == "")
                {
                    expMsg = null;
                    return true;
                }

                else
                {
                    expMsg = RXS;
                    return false;
                }
                    
            }
            catch (Exception exp)
            {
                expMsg = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// Write a single bit data to PLC
        /// </summary>
        /// <param name="MemoryArea">Target memory area</param>
        /// <param name="WordxAddress">Target word hex address</param>
        /// <param name="BitxAddress">Target bit hex address</param>
        /// <param name="value">Value to write 0/1 will be converted to 00/01</param>
        /// <param name="PlcPort">Plc serial port</param>
        /// <param name="expMsg">Exception message</param>
        /// <returns>If successed</returns>
        public static bool BitWrtPLC(PlcMemArea MemoryArea, string WordxAddress, string BitxAddress, int value, SerialPort PlcPort, out string expMsg)
        {
            string MA = null;
            string count = "0001";
            switch (MemoryArea)
            {
                case PlcMemArea.CIO:
                    MA = "B0";
                    break;
                case PlcMemArea.WR:
                    MA = OmronFINsClass.WR;
                    break;
                case PlcMemArea.WR_bit:
                    MA = OmronFINsClass.WR_bit;
                    break;
                case PlcMemArea.HR:
                    MA = "B2";
                    break;
                case PlcMemArea.AR:
                    MA = "B3";
                    break;
                case PlcMemArea.DM:
                    MA = "82";
                    break;
                case PlcMemArea.HR_bit:
                    MA = OmronFINsClass.HR_bit;
                    break;
                default:
                    break;
            }

            try
            {
                SendPLC(OmronFINsClass.FINSh + OmronFINsClass.Wrt + MA + WordxAddress + BitxAddress 
                    + count + value.ToString().PadLeft(2,'0'), PlcPort);
                //Thread.Sleep(10);
                string RXS = ReadPLC(PlcPort);
                if (RXS == "")
                {
                    expMsg = null;
                    return true;
                }

                else
                {
                    expMsg = RXS;
                    return false;
                }
                    
            }
            catch (Exception exp)
            {
                expMsg = exp.Message;
                return false;
            }
        }

        /// <summary>
        /// Generic read data as word string from PLC memory area
        /// </summary>
        /// <param name="MemoryArea">PLC memory area: CIO,WR,HR,AR,DM</param>
        /// <param name="xBeginAddress">The PLC decimal address string of first word to read</param>
        /// <param name="xEndAddress">The PLC decimal address string of last word to read</param>
        /// <param name="PlcPort">PLC serial port</param>
        /// <returns>Data string read from PLC</returns>
        public static string GenericRdPLC(PlcMemArea MemoryArea, string BeginAddress, string EndAddress, System.IO.Ports.SerialPort PlcPort)
        {
            string MA = null;
            string count = null;
            switch (MemoryArea)
            {
                case PlcMemArea.CIO:
                    MA = "B0";
                    break;
                case PlcMemArea.WR:
                    MA = OmronFINsClass.WR;
                    break;
                case PlcMemArea.WR_bit:
                    MA = OmronFINsClass.WR_bit;
                    break;
                case PlcMemArea.HR:
                    MA = "B2";
                    break;
                case PlcMemArea.AR:
                    MA = "B3";
                    break;
                case PlcMemArea.DM:
                    MA = "82";
                    break;
            }
            //count = Convert.ToString((Convert.ToInt32(xEndAddress, 16) - Convert.ToInt32(xBeginAddress, 16) + 1), 16).PadLeft(4, '0');
            count = HexConvert10((int.Parse(EndAddress) - int.Parse(BeginAddress) + 1).ToString()).PadLeft(4,'0');
            string xBeginAddress = HexConvert10(BeginAddress).PadLeft(4, '0').PadRight(6, '0');
             //= string.Format("{0:x}", BeginAddress).PadRight(6, '0');
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                
                //string debugSendStr = OmronFINsClass.FINSh + OmronFINsClass.Rd + MA + xBeginAddress + count;
                
                SendPLC(OmronFINsClass.FINSh + OmronFINsClass.Rd + MA + xBeginAddress + count, PlcPort);
                
                
                //Thread.Sleep(10);
                //stopwatch.Start();
                string result = ReadPLC(PlcPort);
                //stopwatch.Stop();
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
        /// Generic read data as bits string
        /// </summary>
        /// <param name="MemoryArea">PLC memory area: CIO,WR,HR,AR,DM</param>
        /// <param name="WordAddressBegin">The PLC decimal word address string of first bit to read to reside in</param>
        /// <param name="BitAddressBegin">The PLC decimal bit address, first</param>
        /// <param name="WordAddressEnd">The PLC decimal word address string of last bit to read to reside in</param>
        /// <param name="BitAddressEnd">The PLC decimal bit address, last</param>
        /// <param name="PlcPort">Plc serial port</param>
        /// <returns>Data string read from PLC</returns>
        public static string GenericBitsRdPLC(PlcMemArea MemoryArea, string WordAddressBegin, 
            string BitAddressBegin, string WordAddressEnd, string BitAddressEnd, SerialPort PlcPort)
        {
            string MA = null;
            string count = null;
            switch (MemoryArea)
            {
                case PlcMemArea.CIO:
                    MA = "B0";
                    break;
                case PlcMemArea.WR:
                    MA = OmronFINsClass.WR;
                    break;
                case PlcMemArea.WR_bit:
                    MA = OmronFINsClass.WR_bit;
                    break;
                case PlcMemArea.HR:
                    MA = "B2";
                    break;
                case PlcMemArea.AR:
                    MA = "B3";
                    break;
                case PlcMemArea.DM:
                    MA = "82";
                    break;
                case PlcMemArea.HR_bit:
                    MA = OmronFINsClass.HR_bit;
                    break;
                default:
                    break;
            }
            //count = Convert.ToString((Convert.ToInt32(xEndAddress, 16) - Convert.ToInt32(xBeginAddress, 16) + 1), 16).PadLeft(4, '0');
            count = HexConvert10(((int.Parse(WordAddressEnd) - int.Parse(WordAddressBegin)) * OmronFINsClass.Width_Word 
                + int.Parse(BitAddressEnd) - int.Parse(BitAddressBegin) + 1).ToString()).PadLeft(4, '0');
            string xBeginAddress = HexConvert10(WordAddressBegin).PadLeft(4, '0') + HexConvert10(BitAddressBegin).PadLeft(2, '0');

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

        public static List<PlcWordModel> ConvWordStrToWordList(PlcMemArea memArea, string S)
        {
            if (S.Length > 512 * 4)
            {
                return null;
            }
            else
            {
                List<PlcWordModel> retList = new List<PlcWordModel>();
                
                for (int i = 0; i < S.Length / 4 - 1; i++)
                {
                    retList.Add(new PlcWordModel(memArea, i, S.Substring(4 + 4 * i, 4)));
                }
                return retList;
            }
        }

        public static void UpdateStringToWords(PlcMemArea memArea, SortedList<int,string> sList)
        {
            string MemArea = null;
            List<PlcWordModel> listInUse = new List<PlcWordModel>();
            List<PlcBitModel> bitListInUse = new List<PlcBitModel>();
            switch (memArea)
            {
                case PlcMemArea.WR:
                    listInUse = PlcDataMapper.PlcWRWords;
                    MemArea = "W";
                    break;
                case PlcMemArea.HR:
                    listInUse = PlcDataMapper.PlcHRWords;
                    MemArea = "H";
                    break;
                case PlcMemArea.DM:
                    listInUse = PlcDataMapper.PlcDMWords;
                    MemArea = "D";
                    break;
                case PlcMemArea.CIO:
                    break;
                case PlcMemArea.WR_bit:
                    bitListInUse = PlcDataMapper.PlcBits;
                    MemArea = "W";
                    break;
                case PlcMemArea.HR_bit:
                    bitListInUse = PlcDataMapper.PlcBits;
                    MemArea = "H";
                    break;
                case PlcMemArea.AR:
                    break;
                default:
                    //listInUse = new List<PlcWordModel>();
                    break;
            }

            if (memArea == PlcMemArea.DM)
            {
                foreach (var addrStr in sList)
                {
                    int address = addrStr.Key;
                    for (int i = 0; i < addrStr.Value.Length / 4; i++)
                    {
                        listInUse.Find(x => x.WordAddress == address)
                            .SetValue(addrStr.Value.Substring(4 * i, 4));
                        address++;
                    }
                } 
            }
            if (memArea==PlcMemArea.HR_bit || memArea==PlcMemArea.WR_bit)
            {
                foreach (var addrStr in sList)
                {
                    int wordAddress = addrStr.Key;
                    int bitAddress = 0;
                    string binStr = BinaryConvert16(addrStr.Value);
                    for (int i = 0; i < binStr.Length; i++)
                    {
                        PlcBitModel tempBit = PlcDataMapper.PlcBits.FirstOrDefault(x => x.MemArea == MemArea && x.WordAddress == wordAddress
                        && x.BitAddress == bitAddress);
                        if (tempBit!=null)
                        {
                            tempBit.SetValue(binStr.Substring(i, 1));
                        }
                        bitAddress++;
                        if (bitAddress==16)
                        {
                            wordAddress++;
                            bitAddress = 0;
                        }
                    }
                }
            }

            //  if (memArea == PlcMemArea.DM)
            // {


            //for (int i = 0; i < S.Length / 4 - 1; i++)
            //{
            //    PlcDataMapper.PlcDMWords.Find(x => x.MemoryArea == MemArea && x.WordAddress == OmronFINsClass.StartingId_DM + i)
            //        .SetValue(S.Substring(4 * i, 4));
            //    retList.Add(new PlcWordModel(memArea, i, S.Substring(4 + 4 * i, 4)));
            //}
            // }
            //else if (memArea == PlcMemArea.HR)
            //{
            //    foreach (var addrStr in sList)
            //    {
            //        int address = addrStr.Key;
            //        for (int i = 0; i < addrStr.Value.Length / 4; i++)
            //        {
            //            PlcDataMapper.PlcHRWords.Find(x => x.WordAddress == address)
            //                .SetValue(addrStr.Value.Substring(4 * i, 4));
            //            address++;
            //        }
            //    }
            //    //for (int i = 0; i < S.Length / 4 - 1; i++)
            //    //{
            //    //    PlcDataMapper.PlcHRWords.Find(x => x.MemoryArea == MemArea && x.WordAddress == OmronFINsClass.StartingId_HR + i)
            //    //        .SetValue(S.Substring(4 * i, 4));
            //    //    //retList.Add(new PlcWordModel(memArea, i, S.Substring(4 + 4 * i, 4)));
            //    //}
            //}
            //else if (memArea == PlcMemArea.WR)
            //{
            //    foreach (var addrStr in sList)
            //    {
            //        int address = addrStr.Key;
            //        for (int i = 0; i < addrStr.Value.Length / 4; i++)
            //        {
            //            PlcDataMapper.PlcWRWords.Find(x => x.WordAddress == address)
            //                .SetValue(addrStr.Value.Substring(4 * i, 4));
            //            address++;
            //        }
            //    }
            //}


        }



        public static void WordsToBits(PlcMemArea memArea)
        {
            string MemArea = null;
            int size = 0;
            List<PlcWordModel> wordList = new List<PlcWordModel>();

            switch (memArea)
            {
                case PlcMemArea.CIO:
                    MemArea = "C";
                    break;
                case PlcMemArea.WR:
                    MemArea = "W";
                    size = OmronFINsClass.Size_WR;
                    wordList = PlcDataMapper.PlcWRWords;
                    break;
                case PlcMemArea.HR:
                    MemArea = "H";
                    size = OmronFINsClass.Size_HR;
                    wordList = PlcDataMapper.PlcHRWords;
                    break;
                case PlcMemArea.AR:
                    MemArea = "A";
                    break;
                case PlcMemArea.DM:
                    MemArea = "D";
                    break;
                case PlcMemArea.WR_bit:
                    MemArea = "W";
                    size = OmronFINsClass.Size_WR;
                    wordList = PlcDataMapper.PlcWRWords;
                    break;
                case PlcMemArea.HR_bit:
                    size = OmronFINsClass.Size_HR;
                    wordList = PlcDataMapper.PlcHRWords;
                    MemArea = "H";
                    break;
                default:
                    break;
            }

            Stopwatch stopwatch = new Stopwatch();
            
            foreach (var word in wordList)
            {
                for (int j = 0; j < OmronFINsClass.Width_Word; j++)
                {
                    stopwatch.Start();
                    PlcDataMapper.PlcBits.Find(b => b.MemArea == MemArea
                    && b.WordAddress == word.WordAddress && b.BitAddress == j).SetValue(word.Bits[j]);
                    stopwatch.Stop();
                }
                //for (int j = 0; j < OmronFINsClass.Width_Word; j++)
                //{
                //    PlcDataMapper.PlcBits=word.plcBits;
                //}

                //PlcDataMapper.PlcBits.FindAll(b => b.MemArea == MemArea) = word.plcBits;

            }
            

        }

        public static void UpdateStringToBits(PlcMemArea memArea, string S)
        {
            string MemArea = null;
            switch (memArea)
            {
                case PlcMemArea.CIO:
                    MemArea = "C";
                    break;
                case PlcMemArea.WR:
                    MemArea = "W";
                    break;
                case PlcMemArea.HR:
                    MemArea = "H";
                    break;
                case PlcMemArea.AR:
                    MemArea = "A";
                    break;
                case PlcMemArea.DM:
                    MemArea = "D";
                    break;
                case PlcMemArea.WR_bit:
                    MemArea = "W";
                    break;
                case PlcMemArea.HR_bit:
                    MemArea = "H";
                    break;
                default:
                    break;
            }
            if (S.Length == 512 * OmronFINsClass.Width_Word)
            {
                int startingId = 0;
                if (memArea==PlcMemArea.HR_bit)
                {
                    startingId = OmronFINsClass.StartingId_HR;
                }
                else if(memArea == PlcMemArea.WR_bit)
                {
                    startingId = OmronFINsClass.StartingId_WR;
                }
                for (int i = 0; i < S.Length/OmronFINsClass.Width_Word; i++)
                {
                    for (int j = 0; j < OmronFINsClass.Width_Word; j++)
                    {
                        PlcDataMapper.PlcBits.Find(b => b.MemArea == MemArea
                        && b.WordAddress == startingId + i
                        && b.BitAddress == j).SetValue(S.Substring(OmronFINsClass.Width_Word * i + j, 1));
                    }
                }
            }

        }

        public static string ConvWordListToStr(List<PlcWordModel> wordsList)
        {
            string retStr = null;

            foreach (var word in wordsList)
            {
                retStr += word.ValueStrHex;
            }
            return retStr;
        }

        public static string GenerateStrFromWords(PlcMemArea memArea)
        {
            string retStr = null;
            string MemArea = null;
            switch (memArea)
            {
                case PlcMemArea.CIO:
                    MemArea = "C";
                    break;
                case PlcMemArea.WR:
                    MemArea = "W";
                    break;
                case PlcMemArea.HR:
                    MemArea = "H";
                    break;
                case PlcMemArea.AR:
                    MemArea = "A";
                    break;
                case PlcMemArea.DM:
                    MemArea = "D";
                    break;
                default:
                    break;
            }

            foreach (var word in PlcDataMapper.PlcDMWords)
            {
                if (MemArea==word.MemoryArea)
                {
                    retStr += word.ValueStrHex; 
                }
            }
            return retStr;
        }

        /// <summary>
        /// Read all WR area in PLC
        /// </summary>
        /// <param name="PlcSerialPort">Serial port to be used</param>
        /// <returns>All WR words list</returns>
        //public static List<PlcWordModel> 
        public static void ReadAllWords(SerialPort PlcSerialPort, out long debugT)
        {
            //PLCCommDataModel PlcData = new PLCCommDataModel();

            //note: after testing the max read length is 269 words
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //string StrRx5 = GenTestStr(512 * 2);
            string StrRx5 = GenericRdPLC(PlcMemArea.DM, OmronFINsClass.StartingId_DM.ToString(),
            (OmronFINsClass.StartingId_DM + 255).ToString(), PlcSerialPort);//17000-17255
            sw.Stop();
            //string StrRx6 = GenTestStr(512 * 2);
            string StrRx6 = GenericRdPLC(PlcMemArea.DM, (OmronFINsClass.StartingId_DM + 256).ToString(),
            (OmronFINsClass.StartingId_DM + 511).ToString(), PlcSerialPort);//17256-17511

            //string StrRx5 = GenericRdPLC(PlcMemArea.DM, "4268", "4367", PlcSerialPort);//17000-17255
            //string StrRx6 = GenericRdPLC(PlcMemArea.DM, "4368", "4467", PlcSerialPort);//17256-17511

            //string StrRx1 = GenTestStr(512 * 2);
            string StrRx1 = GenericRdPLC(PlcMemArea.WR, (OmronFINsClass.StartingId_WR).ToString(),
            (OmronFINsClass.StartingId_WR + 255).ToString(), PlcSerialPort);//0000-0255
            //string StrRx2 = GenTestStr(512 * 2);
            string StrRx2 = GenericRdPLC(PlcMemArea.WR, (OmronFINsClass.StartingId_WR + 256).ToString(),
            (OmronFINsClass.StartingId_WR + 511).ToString(), PlcSerialPort);//0256-0511


            //string StrRx3 = GenTestStr(512 * 2);
            string StrRx3 = GenericRdPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR).ToString(),
            (OmronFINsClass.StartingId_HR + 255).ToString(), PlcSerialPort);//0000-0255
            //string StrRx4 = GenTestStr(512 * 2);
            string StrRx4 = GenericRdPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR + 256).ToString(),
            (OmronFINsClass.StartingId_HR + 511).ToString(), PlcSerialPort);//0256-0511



            //string StrRx = StrRx1 + StrRx2;
            //PlcData.AllW = ConvWordStrToWordArray(StrRx);
            //PlcData.GetRetValue();
            //return ConvWordStrToWordList(PlcMemArea.WR, StrRx);

            lock (PlcDataMapper._WordsListLock)
            {

                //UpdateStringToWords(PlcMemArea.WR, StrRx1 + StrRx2);
                //UpdateStringToWords(PlcMemArea.HR, StrRx3 + StrRx4);
                //UpdateStringToWords(PlcMemArea.DM, StrRx5 + StrRx6);

            }

            debugT = sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Read all WR, HR, DM words from PLC
        /// </summary>
        /// <param name="PlcSerialPort">PLC serial port</param>
        /// <param name="WRstr">return WR string</param>
        /// <param name="HRstr">return HR string</param>
        /// <param name="DMstr">return DM string</param>
        /// <returns>successful or not</returns>
        public static bool ReadAllPlcWords(SerialPort PlcSerialPort, out string WRstr, out string HRstr, out string DMstr)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //string StrRx5 = GenTestStr(512 * 2);
            string StrRx5 = GenericRdPLC(PlcMemArea.DM, OmronFINsClass.StartingId_DM.ToString(),
            (OmronFINsClass.StartingId_DM + 255).ToString(), PlcSerialPort);//17000-17255
            sw.Stop();
            //string StrRx6 = GenTestStr(512 * 2);
            string StrRx6 = GenericRdPLC(PlcMemArea.DM, (OmronFINsClass.StartingId_DM + 256).ToString(),
            (OmronFINsClass.StartingId_DM + 511).ToString(), PlcSerialPort);//17256-17511

            //string StrRx5 = GenericRdPLC(PlcMemArea.DM, "4268", "4367", PlcSerialPort);//17000-17255
            //string StrRx6 = GenericRdPLC(PlcMemArea.DM, "4368", "4467", PlcSerialPort);//17256-17511

            //string StrRx1 = GenTestStr(512 * 2);
            string StrRx1 = GenericRdPLC(PlcMemArea.WR, (OmronFINsClass.StartingId_WR).ToString(),
            (OmronFINsClass.StartingId_WR + 255).ToString(), PlcSerialPort);//0000-0255
            //string StrRx2 = GenTestStr(512 * 2);
            string StrRx2 = GenericRdPLC(PlcMemArea.WR, (OmronFINsClass.StartingId_WR + 256).ToString(),
            (OmronFINsClass.StartingId_WR + 511).ToString(), PlcSerialPort);//0256-0511


            //string StrRx3 = GenTestStr(512 * 2);
            string StrRx3 = GenericRdPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR).ToString(),
            (OmronFINsClass.StartingId_HR + 255).ToString(), PlcSerialPort);//0000-0255
            //string StrRx4 = GenTestStr(512 * 2);
            string StrRx4 = GenericRdPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR + 256).ToString(),
            (OmronFINsClass.StartingId_HR + 511).ToString(), PlcSerialPort);//0256-0511

            WRstr = StrRx1 + StrRx2;
            HRstr = StrRx3 + StrRx4;
            DMstr = StrRx5 + StrRx6;
            if (WRstr.Length>0&&HRstr.Length>0&&DMstr.Length>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool ReadAllPlcWords(SerialPort PlcSerialPort, 
            SortedList<int, int> addrDM, SortedList<int, int> addrHR, SortedList<int, int> addrWR, 
            out SortedList<int, string> strDM, out SortedList<int, string> strHR, out SortedList<int, string> strWR)
        {
            strWR = new SortedList<int, string>();
            strHR = new SortedList<int, string>();
            strDM = new SortedList<int, string>();

            foreach (var address in addrWR)
            {
                strWR.Add(address.Key,
                    GenericRdPLC(PlcMemArea.WR, address.Key.ToString(), address.Value.ToString(), PlcSerialPort));
            }

            foreach (var address in addrHR)
            {
                strHR.Add(address.Key,
                    GenericRdPLC(PlcMemArea.HR, address.Key.ToString(), address.Value.ToString(), PlcSerialPort));
            }

            foreach (var address in addrDM)
            {
                strDM.Add(address.Key, 
                    GenericRdPLC(PlcMemArea.DM, address.Key.ToString(), address.Value.ToString(), PlcSerialPort));
            }
            
            //foreach (var item in strWR)
            //{
            //    WRstr += item.Value;
            //}
            //foreach (var item in strHR)
            //{
            //    HRstr += item.Value;
            //}
            //foreach (var item in strDM)
            //{
            //    DMstr += item.Value;
            //}
            if (strDM.Count > 0 && strHR.Count > 0 && strWR.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static string GenTestStr(int length)
        {
            string ret = null;
            for (int i = 0; i < length; i++)
            {
                ret = ret + '1';
            }
            return ret;
        }

        /// <summary>
        /// Read all DM data from DM area to Words
        /// </summary>
        /// <param name="PlcSerialPort">Plc serial port</param>
        //public static void ReadAllDMWords(SerialPort PlcSerialPort)
        //{
        //    //PLCCommDataModel PlcData = new PLCCommDataModel();

        //    //note: after testing the max read length is 269 words

        //    string StrRx5 = GenericRdPLC(PlcMemArea.DM, OmronFINsClass.StartingId_DM.ToString(),
        //        (OmronFINsClass.StartingId_DM + 255).ToString(), PlcSerialPort);//17000-17255
        //    string StrRx6 = GenericRdPLC(PlcMemArea.DM, (OmronFINsClass.StartingId_DM + 256).ToString(),
        //        (OmronFINsClass.StartingId_DM + 511).ToString(), PlcSerialPort);//17256-17511

        //    lock (PlcDataMapper._WordsListLock)
        //    {
        //        //UpdateStringToWords(PlcMemArea.DM, StrRx5 + StrRx6);
        //    }
        //}

        public static void ReadAllBits(SerialPort PlcSerialPort)
        {
            //PLCCommDataModel PlcData = new PLCCommDataModel();

            //note: after testing the max read length is 269 words


            //string StrRx5 = GenericRdPLC(PlcMemArea.DM, "4268", "4367", PlcSerialPort);//17000-17255
            //string StrRx6 = GenericRdPLC(PlcMemArea.DM, "4368", "4467", PlcSerialPort);//17256-17511
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string StrRx1 = GenericBitsRdPLC(PlcMemArea.WR_bit, (OmronFINsClass.StartingId_WR).ToString(),"00",
                (OmronFINsClass.StartingId_WR + 255).ToString(), "15", PlcSerialPort);//000000-025515
            stopwatch.Stop();
            string StrRx2 = GenericBitsRdPLC(PlcMemArea.WR_bit, (OmronFINsClass.StartingId_WR + 256).ToString(), "00",
                (OmronFINsClass.StartingId_WR + 511).ToString(), "15", PlcSerialPort);//025600-051115
            int debug1 = StrRx1.Length;
            int debug2 = StrRx2.Length;

            string StrRx3 = GenericBitsRdPLC(PlcMemArea.HR_bit, (OmronFINsClass.StartingId_HR).ToString(), "00",
                (OmronFINsClass.StartingId_HR + 255).ToString(), "15", PlcSerialPort);//000000-025515
            string StrRx4 = GenericBitsRdPLC(PlcMemArea.HR_bit, (OmronFINsClass.StartingId_HR + 256).ToString(), "00",
                (OmronFINsClass.StartingId_HR + 511).ToString(), "15", PlcSerialPort);//025600-051115

            //string StrRx3 = GenericRdPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR).ToString(),
            //    (OmronFINsClass.StartingId_HR + 255).ToString(), PlcSerialPort);//0000-0255
            //string StrRx4 = GenericRdPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR + 256).ToString(),
            //    (OmronFINsClass.StartingId_HR + 511).ToString(), PlcSerialPort);//0256-0511



            //TODO: verify Rx string is valid before processing
            //string StrRx = StrRx1 + StrRx2;
            //PlcData.AllW = ConvWordStrToWordArray(StrRx);
            //PlcData.GetRetValue();
            //return ConvWordStrToWordList(PlcMemArea.WR, StrRx);
            lock (PlcDataMapper._WordsListLock)
            {
                UpdateStringToBits(PlcMemArea.WR_bit, StrRx1 + StrRx2);
                UpdateStringToBits(PlcMemArea.HR_bit, StrRx3 + StrRx4);
            }

        }


        /// <summary>
        /// Write all static W,H,D to PLC
        /// </summary>
        /// <param name="PlcSerialPort"></param>
        public static void WriteAllWords(SerialPort PlcSerialPort)
        {
            string str_AllW = GenerateStrFromWords(PlcMemArea.WR);
            string strW1 = str_AllW.Substring(0, str_AllW.Length/2);
            string strW2 = str_AllW.Substring(str_AllW.Length/2, str_AllW.Length / 2);
            int debug1 = strW1.Length;
            int debug2 = strW2.Length;

            string str_AllH = GenerateStrFromWords(PlcMemArea.HR);
            string strH1 = str_AllH.Substring(0, str_AllH.Length / 2);
            string strH2 = str_AllH.Substring(str_AllH.Length / 2, str_AllH.Length / 2);

            string str_AllD = GenerateStrFromWords(PlcMemArea.DM);
            string strD1 = str_AllD.Substring(0, str_AllD.Length / 2);
            string strD2 = str_AllD.Substring(str_AllD.Length / 2, str_AllD.Length / 2);
            //Note: below write method parameters were not up to date
            //GenericWrtPLC(PlcMemArea.WR, OmronFINsClass.StartingId_WR.ToString(),
            //    (OmronFINsClass.StartingId_WR+255).ToString(), strW1, PlcSerialPort);//000-255
            //GenericWrtPLC(PlcMemArea.WR, (OmronFINsClass.StartingId_WR+256).ToString(),
            //    (OmronFINsClass.StartingId_WR + 511).ToString(), strW1, PlcSerialPort);//256-511

            //GenericWrtPLC(PlcMemArea.HR, OmronFINsClass.StartingId_HR.ToString(),
            //    (OmronFINsClass.StartingId_HR + 255).ToString(), strH1, PlcSerialPort);//000-255
            //GenericWrtPLC(PlcMemArea.HR, (OmronFINsClass.StartingId_HR + 256).ToString(),
            //    (OmronFINsClass.StartingId_HR + 511).ToString(), strH1, PlcSerialPort);//256-511

            //GenericWrtPLC(PlcMemArea.DM, OmronFINsClass.StartingId_DM.ToString(),
            //    (OmronFINsClass.StartingId_DM + 255).ToString(), strD1, PlcSerialPort);//000-255
            //GenericWrtPLC(PlcMemArea.DM, (OmronFINsClass.StartingId_DM + 256).ToString(),
            //    (OmronFINsClass.StartingId_DM + 511).ToString(), strD1, PlcSerialPort);//256-511
        }

        /// <summary>
        /// Send word one by one in Words list
        /// </summary>
        /// <param name="PlcSerialPort">Serial port</param>
        /// <param name="words">Words list</param>
        public static bool WriteWords(SerialPort PlcSerialPort,List<PlcWordModel> words, out string expMsg)
        {
            bool successed = false;
            string msg = null;
            foreach (var word in words)
            {
                successed = GenericWrtPLC(PlcMemArea.DM, word.WordAddress.ToString(), word.WordAddress.ToString(),
                    word.ValueStrHex, PlcSerialPort, out msg);
                if (!successed)
                {
                    expMsg = msg;
                    return false;
                }
            }
            expMsg = null;
            return successed;
        }

        /// <summary>
        /// Send bit one by one in Bits list
        /// </summary>
        /// <param name="PlcSerialPort">Serial port</param>
        /// <param name="bits">Bits list</param>
        public static bool WriteBits(SerialPort PlcSerialPort, List<PlcBitModel> bits, out string expMsg)
        {
            bool successed = false;
            string msg = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var bit in bits)
            {
                //stopwatch.Reset();
                //stopwatch.Start();
                if (bit.MemArea=="W")
                {
                    successed = BitWrtPLC(PlcMemArea.WR_bit, bit.WordAddressStr, bit.BitAddressStr, bit.Value, PlcSerialPort, out msg);
                    if (!successed)
                    {
                        expMsg = msg;
                        return false;
                    }
                }
                else if (bit.MemArea=="H")
                {
                    successed = BitWrtPLC(PlcMemArea.HR_bit, bit.WordAddressStr, bit.BitAddressStr, bit.Value, PlcSerialPort, out msg);
                    if (!successed)
                    {
                        expMsg = msg;
                        return false;
                    }
                }
                //stopwatch.Stop();
            }
            expMsg = null;
            stopwatch.Stop();
            return successed;
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
        /// Convert Hex string to Decimal string
        /// </summary>
        /// <param name="str16">Hex string</param>
        /// <returns>Decimal string</returns>
        public static string DecimalConvert16(string str16)
        {
            string str10 = String.Join(String.Empty,
             str16.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 10).PadLeft(4, '0')));

            return str10;
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
