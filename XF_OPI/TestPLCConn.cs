using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XFOPI_Library;
using XFOPI_Library.PLCConnection;
using XFOPI_Library.Models;

namespace XF_OPI
{
    public partial class TestPLCConn : Form
    {
        SerialPort PlcSerialPort;
        PLCCommDataModel PlcData = new PLCCommDataModel();
        //int loopCounter;

        public TestPLCConn()
        {
            InitializeComponent();
        }

        private void TestPLCConn_Load(object sender, EventArgs e)
        {

        }

        private void btnRdAll_Click(object sender, EventArgs e)
        {
            RdAllW();
        }

        private void btnWrtAll_Click(object sender, EventArgs e)
        {
            WrtAllW("0001");
        }

        public void setSerialPort(SerialPort port)
        {
            PlcSerialPort = port;
        }

        private void RdAllW()
        {
            //string StrRx1 = OmronFINsProcessor.GenericRdPLC("WR", "000000", "012B00", PlcSerialPort);
            string StrRx1 = OmronFINsProcessor.GenericRdPLC("WR", "0000", "0049", PlcSerialPort);
            //string StrRx2 = OmronFINsProcessor.GenericRdPLC("WR", "012C00", "01FF00", PlcSerialPort);
            string StrRx2 = OmronFINsProcessor.GenericRdPLC("WR", "0050", "0099", PlcSerialPort);
            //TODO: verify Rx string is valid before processing
            string StrRx = StrRx1 + StrRx2;
            PlcData.AllW = OmronFINsProcessor.ConvWordStrToWordArray(StrRx);
        }

        private void WrtAllW(string StringToWrite)
        {
            string WordStrToWrite1 = null;
            string WordStrToWrite2 = null;
            //for (int i = 0; i < 300; i++)
            //{
            //    WordStrToWrite1 = WordStrToWrite1 + "0011";
            //}
            //for (int i = 0; i < 212; i++)
            //{
            //    WordStrToWrite2 = WordStrToWrite2 + "0011";
            //}
            //WordStrToWrite1 = StringToWrite;
            //WordStrToWrite2 = (int.Parse(StringToWrite)+1).ToString().PadLeft(4,'0');

            //OmronFINsProcessor.GenericWrtPLC("WR", "000000", "012B00", WordStrToWrite1, PlcSerialPort);
            OmronFINsProcessor.GenericWrtPLC("WR", "0000", "0099", StringToWrite, PlcSerialPort);
            //OmronFINsProcessor.GenericWrtPLC("WR", "012C00", "01FF00", WordStrToWrite2, PlcSerialPort);
            //OmronFINsProcessor.GenericWrtPLC("WR", "0050", "0099", WordStrToWrite2, PlcSerialPort);
        }

        private void btnLoopRW_Click(object sender, EventArgs e)
        {
            PLCRWtimer.Start();
        }

        private void PLCRWtimer_Tick(object sender, EventArgs e)
        {
            string StringToWrite=null;
            for (int i = 0; i < int.Parse(LoopCount.Text); i++)
            {
                
                for (int j = 0; j < 100; j++)
                {
                    StringToWrite = StringToWrite + i.ToString().PadLeft(4, '0');
                }
                RdAllW();
                WrtAllW(StringToWrite);
            }

            foreach (var item in PlcData.AllW)
            {
                Console.WriteLine(item);
            }

            PLCRWtimer.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.BackColor = Color.Orange;
        }
    }
}
