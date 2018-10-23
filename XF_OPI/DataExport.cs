using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace XF_OPI
{
    public partial class DataExport : Form
    {
        public DataExport()
        {
            InitializeComponent();
        }

        public class MUBAdataStructure
        {
            public string[] MUBAdataS { get; set; } //DM 5801~5836
            public int AddrB { get; }
            public int AddrE { get; }

            public MUBAdataStructure()
            {
                MUBAdataS = new string[36];
                AddrB = 5801;
                AddrE = 5836;
            }
        }

        OmronFINsClass.PLCcommand PLCcmd = new OmronFINsClass.PLCcommand();
        OmronFINsClass Omron = new OmronFINsClass();
        MUBAdataStructure MUBAdata = new MUBAdataStructure();

        private bool DistMUBAdata(string S)
        {
            if (S.Length == 144)
            {
                for (int i = 0; i < 36; i++)
                {
                    if(i%2==0)
                        MUBAdata.MUBAdataS[i+1] = S.Substring(4 * i, 4);
                    else
                        MUBAdata.MUBAdataS[i-1] = S.Substring(4 * i, 4);
                }


                return true;
            }
            else
                return false;
        }

        private void DataExport_Load(object sender, EventArgs e)
        {
            this.dateTimePickerStartTime.Value = DateTime.Now;
            this.dateTimePickerEndTime.Value = DateTime.Now;
            // string sCommandFileName = simCmd.sCommandFileName;
            //StreamReader srCommand = new StreamReader(sCommandFileName);
        }

        private void WriteToFile(string sOutputFileName, string sToWrite)
        {
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(sOutputFileName, true))
            {
                file.WriteLine(sToWrite);
            }
        }

        private int GetMUBA()
        {
            int MUBA = 0;
            int[] MUBAarray = new int[18];
            int TotalUnits = 0;
            int TotalJams = 0;

            for(int i = 0; i < 18; i++)
            {
                MUBAarray[i] = int.Parse(MUBAdata.MUBAdataS[i]);
            }

            TotalUnits = MUBAarray[0];
            for(int i = 1; i < 18; i++)
            {
                if (i < 6 && i > 9)//6~9 are test related, not count to jams
                    TotalJams = TotalJams + MUBAarray[i];
            }
            return MUBA;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HrPlus_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string PickerName = "";
            switch (bt.Name)
            {
                case "BtnFrmHrPlus":
                    PickerName = "dateTimePickerStartTime";
                    break;
                case "BtnFrmHrMinus":
                    PickerName = "dateTimePickerStartTime";
                    break;
                case "BtnToHrPlus":
                    PickerName = "dateTimePickerEndTime";
                    break;
                case "BtnToHrMinus":
                    PickerName = "dateTimePickerEndTime";
                    break;
            }
            var pickers = Controls.Find(PickerName, true);
            if (pickers.Length > 0)
            {
                var picker = (DateTimePicker)pickers[0];
                //  if //(LabelSOT1.Visible||LabelSOT2.Visible|| LabelSOT3.Visible || LabelSOT4.Visible)
                //   (LB.Text == "0000")
                //  label.Visible = false;
                //else
                switch (bt.Name)
                {
                    case "BtnFrmHrPlus":
                        picker.Value = picker.Value.AddHours(1);
                        break;
                    case "BtnFrmHrMinus":
                        picker.Value = picker.Value.AddHours(-1);
                        break;
                    case "BtnToHrPlus":
                        picker.Value = picker.Value.AddHours(1);
                        break;
                    case "BtnToHrMinus":
                        picker.Value = picker.Value.AddHours(-1);
                        break;
                }
            }
        }

        private void BtnToday_Click(object sender, EventArgs e)
        {
            dateTimePickerStartTime.Value = DateTime.Today;
            dateTimePickerEndTime.Value = DateTime.Now;
        }

        private void BtnExportMUBA_Click(object sender, EventArgs e)
        {
            try
            {
                string xAddrB = "";
                string xAddrE = "";
                string RdValue = "";
                int MUBA = 0;
                xAddrB = Omron.HexConvert10(MUBAdata.AddrB.ToString());
                xAddrE = Omron.HexConvert10(MUBAdata.AddrE.ToString());
                 //MainForm.PlcPort.Open();
                //ReadFromPLC();
                //serialPort1.Open();

               // RdValue = Omron.GenericRdPLC(PLCcmd.DM, xAddrB, xAddrE, MainForm.PlcPort);
                //RdValue = Omron.GenericRdPLC(PLCcmd.DM, xAddrB, xAddrE, serialPort1);
                DistMUBAdata(RdValue);
                MUBA = GetMUBA();
                TBoxMUBA.Text = MUBA.ToString();
            }catch(Exception exp) { }
            
            
        }
        /*private bool ReadFromPLC()
        {
            Omron.SendPLC(PLCcmd.getPLCCW, MainForm.PlcPort);
            Thread.Sleep(10);
            string PlcRxStr = Omron.ReadPLC(MainForm.PlcPort);
            if (PlcRxStr != null)
            {
                //DistPlcCWdata(PlcRxStr);
                return true;
            }
            else
                return false;
        }*/
    }
}
