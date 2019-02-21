using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XFOPI_Library.PLCConnection;

namespace XFOPI_Library.Models
{
    /// <summary>
    /// The model for one PLC word in Hex FFFF
    /// !!!Cautious, DO NOT instantiate!!!
    /// To address a single bits, use MemoryArea[WordNumber].Bits[BitNumber]
    /// </summary>
    public class PlcWordModel
    {
        //public string MemoryArea { get; private set; }
        //public int WordAddress { get { return this.WAddress; } }

        //private int WAddress;

        public PlcAddressModel PlcWordAddress = new PlcAddressModel();

        //private string MemArea;
        //private static int Wcounter;

        private string _hexValueStr { get; set; }
        public string _binaryValueStr { get; private set; }
        public List<int> Bits = new List<int>(OmronFINsClass.Width_Word);
        
        public PlcWordModel()
        {
            for (int i = 0; i < OmronFINsClass.Width_Word; i++)
            {
                Bits.Add(0);
            }
            //this.MemArea = MemoryArea;
            //this.WAddress = Wcounter++;
            //if (Wcounter == OmronFINsClass.Size_WR)
            //    throw new InvalidOperationException("Max number of Word address is reached!!");

            //PlcWordAddress.WordAddress = Wcounter++;
        }

        public void SetValueStr(string ValueStr)
        {
            _hexValueStr = ValueStr;
            _binaryValueStr = OmronFINsProcessor.BinaryConvert16(_hexValueStr);
            SetAllBitsValue();
        }        

        public void SetMemoryArea(string MemoryArea)
        {
            PlcWordAddress.MemoryArea = MemoryArea.ToUpper();
        }

        //public void SetWordAddress(int WordAddress)
        //{
        //    this.WordAddress = WordAddress;
        //}

        /// <summary>
        /// Set a value of a individual Bit inside a Word. Update Word value
        /// </summary>
        /// <param name="BitNumber">The address of individual Bit</param>
        /// <param name="BitValue">Value to be set 0 or 1</param>
        public void SetBitValue(int BitAddress, int BitValue)
        {
            if (BitValue==0||BitValue==1)
            {
                Bits[BitAddress] = BitValue;
                List<int> BitsReversed = new List<int>(Bits);
                BitsReversed.Reverse();
                _binaryValueStr = string.Join("", BitsReversed);
                _hexValueStr = OmronFINsProcessor.HexConvert2(_binaryValueStr); 
            }
        }

        /// <summary>
        /// Get the value of the Word in hex
        /// </summary>
        /// <returns>Word hex value string</returns>
        public string GetValueStr()
        {
            return _hexValueStr;
        }

        private void SetAllBitsValue()
        {   
            for (int i = 0; i < OmronFINsClass.Width_Word; i++)
            {
                Bits[i] = int.Parse(_binaryValueStr[i].ToString());
            }
        }

    }
}
