using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using System.Reflection;
using System.Globalization;
using XFOPI_Library.Models.SG2000;
using XFOPI_Library;
using XFOPI_Library.Models;

//TODO - Password login. Lock user controls. Auto-timed log off
//TODO - add recipe management
//TODO - add production data model and export method

namespace XF_OPI
{
    public partial class MainForm : Form
    {
        public List<TypeUintModel> AllTypeUint = new List<TypeUintModel>();
        public List<TypeBoolModel> AllTypeBool = new List<TypeBoolModel>();
        public List<TypeUshortModel> AllTypeUshort = new List<TypeUshortModel>();
        public List<AlarmCodeModel> AllAlarms = new List<AlarmCodeModel>();
        public List<PlcDataAddressRecordModel> plcDataAddressRecords = new List<PlcDataAddressRecordModel>();
        public List<PlcDataAddressMappingModel> plcDataAddressMappings = new List<PlcDataAddressMappingModel>();
        public List<PlcWordModel> WR;
        public Dictionary<string, string> DataAddressMap = new Dictionary<string, string> { };
       

        public MainForm()
        {
            InitializeComponent();
        }

        private void GetRes(CultureInfo CI)
        {
            Thread.CurrentThread.CurrentCulture = CI;
            Thread.CurrentThread.CurrentUICulture = CI;
            ResourceManager rm = new ResourceManager("XF_OPI.MainForm", System.Reflection.Assembly.GetExecutingAssembly());
            label1.Text = rm.GetString("label1.Text", CI);
            label2.Text = rm.GetString("label2.Text", CI);
            label3.Text = rm.GetString("label3.Text", CI);
            label4.Text = rm.GetString("label4.Text", CI);
            label5.Text = rm.GetString("label5.Text", CI);
            label6.Text = rm.GetString("label6.Text", CI);
        }

        private void ButtonCN_Click(object sender, EventArgs e)
        {
            CultureInfo CI = new CultureInfo("zh-CN");
            GetRes(CI);
        }

        private void ButtonEN_Click(object sender, EventArgs e)
        {
            CultureInfo CI = new CultureInfo("en-US");
            GetRes(CI);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (DataExport DE = new DataExport())
            {
                if (DE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //this.textBox5.Text = np.value;
                    //T.Text = np.value;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (LOTform lt = new LOTform())
            {
                if (lt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //this.textBox5.Text = np.value;
                    //T.Text = np.value;
                }
            }
        }


        private void btnUsers_Click(object sender, EventArgs e)
        {
            using (UserMaintForm userMaint = new UserMaintForm())
            {
                if (userMaint.ShowDialog() == DialogResult.OK) { }
            }

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm frm = new SettingForm();
            frm.Show();
            this.Hide();
            frm.VisibleChanged += SubForm_VisibleChanged;

        }
        private void SubForm_VisibleChanged(object sender, EventArgs e)
        {  
            this.Show();
        }

        private void TestPLC_Click(object sender, EventArgs e)
        {
            PLCPort.Open();
            

            using (TestPLCConn TP = new TestPLCConn())
            {
                TP.setSerialPort(PLCPort);
                if (TP.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PLCPort.Close();
                }
            }
        }

        private void btnInitialModels_Click(object sender, EventArgs e)
        {
            AllTypeUint = GlobalConfig.DBConnection.GetTypeUint_All();
            AllAlarms = GlobalConfig.DBConnection.GetAlarmCodes_All();

            dataGridViewAllTypeUint.DataSource = null;
            dataGridViewAllTypeUint.DataSource = AllTypeUint.Select(u => new
            {
                Name = u.Name,
                WriteOnStop = u.WrtOnStop,
                WriteOnStart = u.WrtOnStart,
                WriteOnRun = u.WrtOnRun
            }).ToList();

            dataGridViewAllAlarms.DataSource = null;
            dataGridViewAllAlarms.DataSource = AllAlarms.Select(u => new
            {
                Name = u.AlarmCodeName,
                Description = u.AlarmDescription
            }).ToList();
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //Load addresses from DB
            plcDataAddressRecords = GlobalConfig.DBConnection.GetTypeBoolAddressRecord_All();

            //Create the complete address mappings
            plcDataAddressMappings = GlobalConfig.LoadPlcDataAddressMappings(plcDataAddressRecords, plcDataAddressMappings);

            //Create and load values into Words
            //PlcWordModel[] W = GlobalConfig.CreateArray<PlcWordModel>(plcDataAddressMappings.Count);
            WR = GlobalConfig.CreateList<PlcWordModel>(OmronFINsClass.Size_WR);

            int i = 0;
            foreach (var item in WR)
            {
                item.PlcWordAddress.WordAddress = i;
                item.SetMemoryArea("W");
                i++;
            }
            //for (int i = 0; i < 15; i++)
            //{
            //    W[i].SetWordAddress(i);
            //}



        }

        private void btnSetValue_Click(object sender, EventArgs e)
        {
            //TODO: Consolidate below repeatable process into methods and work Thread

            string DataName = tbDataName.Text;
            int DataValue = int.Parse(cbDataValue.Text);

            //1.Take the UI DataName and find the entry inside the Mapping list
            PlcDataAddressMappingModel entry = plcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == DataName);

            //2.Write the DataValue to the corresponding bit inside the WR Word list WR
            //WR.Find(x => x.MemoryArea == entry.PlcAddress.MemoryArea 
            //&& x.WordAddress == entry.PlcAddress.WordAddress).SetBitValue((int)entry.PlcAddress.BitAddress, DataValue);
            PlcWordModel WR_entry = new PlcWordModel();
            WR_entry = WR.Find(x => x.PlcWordAddress.MemoryArea == entry.PlcAddress.MemoryArea
            && x.PlcWordAddress.WordAddress == entry.PlcAddress.WordAddress);              
            WR_entry.SetBitValue((int)entry.PlcAddress.BitAddress, DataValue);

            //3.Display Word address and its value
            lblWAddress.Text = entry.PlcDataAddressRecord.PlcAddress;
            lblValue.Text = WR_entry.GetValueStr();


        }
    }
}
