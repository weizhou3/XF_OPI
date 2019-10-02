using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Resources;
using System.Threading;
using System.Globalization;
using XFOPI_Library.Models.SG2000;
using XFOPI_Library;
using XFOPI_Library.Models;
using XFOPI_Library.PLCConnection;
using System.IO.Pipelines;


//TODO - Password login. Lock user controls. Auto-timed log off
//TODO - add recipe management
//TODO - add production data model and export method

namespace XF_OPI
{
    public partial class MainForm : Form
    {
        //底层系统交换变量
        //public List<TypeUintModel> AllTypeUint = new List<TypeUintModel>();
        //public List<TypeBoolModel> AllTypeBool = new List<TypeBoolModel>();
        //public List<TypeUshortModel> AllTypeUshort = new List<TypeUshortModel>();

        //public List<PlcDataAddressRecordModel> plcDataAddressRecords = new List<PlcDataAddressRecordModel>();
        //public List<PlcDataAddressMappingModel> plcDataAddressMappings = new List<PlcDataAddressMappingModel>();
        //public List<PlcWordModel> PlcWordList;

        //public List<PlcPortSettingModel> plcPortSettings = new List<PlcPortSettingModel>();


        //public List<AlarmCodeModel> AllAlarms = new List<AlarmCodeModel>();

        //上层系统交换变量,置于 public MainForm() 之前
        //public static Dictionary<string, string> DataNameValue_Wlist = new Dictionary<string, string>();
        //public static Dictionary<string, string> DataNameValue_Rlist = new Dictionary<string, string>();

        CancellationTokenSource cts = new CancellationTokenSource();
        //Progress<ProgressReportModel> progress = new Progress<ProgressReportModel>();



        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            PlcDataMapper.InitializeData();
            WireupForm();
            CultureInfo CI = new CultureInfo(UserSettings.Default.Language);
            GetRes(CI);
            //Thread workerThread = new Thread(()=> GlobalConfig.testWorkerThread());
            //workerThread.Start();

            //OmronFINsPlcConnector dataConnector = new OmronFINsPlcConnector(PLCPort);//publisher
            //TestPrintResultService printService = new TestPrintResultService();//subscriber

            //suscribe events
            //dataConnector.PlcReadingCompleted += UpdateUIservice_read;
            //dataConnector.PlcWritingCompleted += UpdateUIservice_write;




            //start async operation
            //await dataConnector.PlcDataExchangeAllAsync(4000, cts.Token);
            //if (cts.IsCancellationRequested)
            //    rtbOutput.Text += "Operation was cancelled";
            //else
            //    rtbOutput.Text += "ALL DONE!";


            //progress.ProgressChanged += ReportProgress;


            //var result = await GlobalConfig.testWorkerThreadAsync(progress, cts.Token, 20);
            //PrintResult(result);

            //GlobalConfig.DataConnection.SetPort(PLCPort);
            //Progress<ProgressReportModel> progressDataExchange = new Progress<ProgressReportModel>();
            //progressDataExchange.ProgressChanged += ReportProgress;
            //cts = new CancellationTokenSource();
            //DataWorker dataWorker = new DataWorker();
            //dataWorker.RunDataExchange(10, 3000, cts.Token, progressDataExchange);


        }



        private void btnTestAsync_Click(object sender, EventArgs e)
        {
            //OmronFINsPlcConnector dataConnector = new OmronFINsPlcConnector(PLCPort);//initialize PLC data connector
            GlobalConfig.DataConnection.SetPort(PLCPort);

            Progress<PlcProgressReportModel> plcProgress = new Progress<PlcProgressReportModel>();
            plcProgress.ProgressChanged += ReportPlcProgress;

            cts.Dispose();
            cts = new CancellationTokenSource();
            DataWorker dataWorder = new DataWorker();
            dataWorder.RunDataExchange(int.Parse(tBoxTimeout.Text)*1000, 400, cts.Token, plcProgress);//publisher

            //TestPrintResultService printService = new TestPrintResultService();//subscriber

            //dataConnector.PlcReadingCompleted += UpdateUIservice_read;
            //dataConnector.PlcReadingProgressChanged += UpdateUIservice;

            //await dataConnector.TestReadAsync(cts.Token);

            //rtbOutput.Text += "ALL DONE!";




        }

        private void ReportPlcProgress(object sender, PlcProgressReportModel e)
        {
            
            rtbOutput.Text += e.Msg + Environment.NewLine;

            if (e.PercetageComplete==50)
            {
                //publish R list to display
                dataGridViewAll.DataSource = null;
                dataGridViewAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dataGridViewAll.DataSource = PlcDataMapper.DataNameValue_Rlist.Select(u => new
                {
                    Name = u.Key,
                    Value = u.Value.Value
                }).ToList();

                foreach (var cbox in this.Controls.OfType<CheckBox>())
                {
                    if (PlcDataMapper.DataNameValue_Rlist.ContainsKey(cbox.Name.Substring(3, (cbox.Name.Length - 3))))
                    {
                        if (PlcDataMapper.DataNameValue_Rlist[cbox.Name.Substring(3, (cbox.Name.Length - 3))].Value == "1")
                        {
                            cbox.BackColor = Color.Gold;
                            cbox.Enabled = true;
                        }
                        else if (PlcDataMapper.DataNameValue_Rlist[cbox.Name.Substring(3, (cbox.Name.Length - 3))].Value == "0")
                        {
                            cbox.BackColor = Color.LightGray;
                            cbox.Enabled = true;
                        }
                    }
                }
            }
            else if (e.PercetageComplete==100)
            {
                //publish 
                dataGridViewAllOutput.DataSource = null;
                dataGridViewAllOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //dataGridViewAllOutput.DataSource = PlcDataMapper.DataNameValue_Wlist.Select
                dataGridViewAllOutput.DataSource = e.Wlist.Select(u => new
                {
                    Name = u.Key,
                    Value = u.Value

                }).ToList();

                rtbOutput.Clear();

            }
            //refresh button color if set PLC value succeeded
            foreach (var button in this.Controls.OfType<Button>())
            {
                if (PlcDataMapper.DataNameValue_Rlist.ContainsKey(button.Name.Substring(3, (button.Name.Length - 3))))
                {
                    if (PlcDataMapper.DataNameValue_Rlist[button.Name.Substring(3, (button.Name.Length - 3))].Value == "1")
                    {
                        button.BackColor = Color.Gold;
                    }else if (PlcDataMapper.DataNameValue_Rlist[button.Name.Substring(3, (button.Name.Length - 3))].Value == "0")
                    {
                        button.BackColor = Color.LightGray;
                    }
                }                
            }





        }

        //private bool startPlcPort(out string expMsg)
        //{
        //    try
        //    {
        //        if (!PLCPort.IsOpen)
        //            PLCPort.Open();
                
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void wireupLists(bool RdList, bool WrtList, Dictionary<string,string> wlist)
        {
            if (RdList)
            {
                dataGridViewAll.DataSource = null;
                dataGridViewAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dataGridViewAll.DataSource = PlcDataMapper.DataNameValue_Rlist.Select(u => new
                {
                    Name = u.Key,
                    Value = u.Value
                }).ToList(); 
            }

            if (WrtList)
            {
                dataGridViewAllOutput.DataSource = null;
                dataGridViewAllOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                dataGridViewAllOutput.DataSource = wlist.Select(u => new
                {
                    Name = u.Key,
                    Value = u.Value
                }).ToList(); 
            }

        }

        private void UpdateUIservice_read(object sender, PlcFinishReadEventArgs e)
        {
            dataGridViewAll.DataSource = null;
            dataGridViewAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridViewAll.DataSource = PlcDataMapper.DataNameValue_Rlist.Select(u => new
            {
                Name = u.Key,
                Value = u.Value
            }).ToList();
            rtbOutput.Text += "read 255words: ticks" + e.ticks +"  ms time" + e._ms1 + Environment.NewLine;
            rtbOutput.Text += "total ms" + e._ms + Environment.NewLine;
        }

        private void UpdateUIservice_write(object sender, PlcFinishWriteEventArgs e)
        {
            dataGridViewAllOutput.DataSource = null;
            dataGridViewAllOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridViewAllOutput.DataSource = e.BitsSent.Select(u => new
            {
                MemArea = u.MemArea,
                Word = u.WordAddress,
                Bit = u.BitAddress,
                Value = u.Value
            }).ToList();
            rtbOutput.Text += "sent process done ticks:" + e.ticks + "  ms time" + e._ms + Environment.NewLine;
            //rtbOutput.Text += "total ms" + e._ms + Environment.NewLine;
        }

        private void printService(object sender, PlcFinishReadEventArgs args)
        {

            //if (args.TestResult>0)
            //{
            //    rtbOutput.Text += "Reading Completed" + args.TestResult.ToString() + Environment.NewLine;
            //    progressBarDashboard.Value = args.TestResult;
            //}
            //else
            //{
            //    rtbOutput.Text += "Reading in progress" + args.CurrentValue.ToString() + Environment.NewLine;
            //    progressBarDashboard.Value = args.CurrentValue;
            //}
            
        }


        private void ReportProgress(object sender, PlcProgressReportModel e)
        {
            rtbOutput.Text += e.Msg + Environment.NewLine;
            progressBarDashboard.Value = e.PercetageComplete;
            wireupLists(e.ReadCompleted,e.WrtCompleted, e.Wlist);
        }

        private void GetRes(CultureInfo CI)
        {
            Thread.CurrentThread.CurrentCulture = CI;
            Thread.CurrentThread.CurrentUICulture = CI;
            ResourceManager rm = new ResourceManager("XF_OPI.MainForm", System.Reflection.Assembly.GetExecutingAssembly());
            lblSetting.Text = rm.GetString("Setting", CI);
            lblLogOff.Text = rm.GetString("LogOff", CI);
            lblLogOn.Text = rm.GetString("LogOn", CI);
            lblUsers.Text = rm.GetString("Users", CI);
            lblStats.Text = rm.GetString("Statistics", CI);
            lblService.Text = rm.GetString("Service", CI);
            lblCurrentUser.Text = rm.GetString("CurrentUser", CI);
            lblStart.Text = rm.GetString("Start", CI);
            lblStop.Text = rm.GetString("Stop", CI);
        }

        private void ButtonCN_Click(object sender, EventArgs e)
        {
            UserSettings.Default.Language = "zh-CN";
            CultureInfo CI = new CultureInfo(UserSettings.Default.Language);
            GetRes(CI);
        }

        private void ButtonEN_Click(object sender, EventArgs e)
        {
            UserSettings.Default.Language = "en-US";
            CultureInfo CI = new CultureInfo(UserSettings.Default.Language);
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

            bool access = GlobalConfig.VerifyButtonAccess(((Control)sender).Name, this.Name,UserSettings.Default.CurrentUserGroup);

            if (!access)
            {
                MessageBox.Show("Insufficient user rights, current group: " + UserSettings.Default.CurrentUserGroup,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (UserMaintForm userMaint = new UserMaintForm())
                {
                    if (userMaint.ShowDialog() == DialogResult.OK) { }
                }
            }


        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            bool access = GlobalConfig.VerifyButtonAccess(((Control)sender).Name, this.Name, UserSettings.Default.CurrentUserGroup);
            if (!access)
            {
                MessageBox.Show("Insufficient user rights, current group: " + UserSettings.Default.CurrentUserGroup,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SettingForm frm = new SettingForm();
                frm.Owner = this;
                frm.Show();
                Hide();
                frm.VisibleChanged += SubForm_VisibleChanged;
            }


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
               
        private void button2_Click_1(object sender, EventArgs e)
        {

            //WR = GlobalConfig.CreateList<PlcWordModel>(OmronFINsClass.Size_WR);

            //int i = OmronFINsClass.StartingId_WR;
            //foreach (var item in WR)
            //{
            //    item.PlcWordAddress.WordAddress = i;
            //    item.SetMemoryArea("W");
            //    i++;
            //}
            //for (int i = 0; i < 15; i++)
            //{
            //    W[i].SetWordAddress(i);
            //}



        }
        //TODO: Add this button work flow (writing down to PLC) to a worker thread, need to lock input List<DataList>. Need Writable check!
        private void btnSetValue_Click(object sender, EventArgs e)
        {
            //TODO: Consolidate below repeatable process into methods and work Thread
            List<DataNameValueModel> DataList = new List<DataNameValueModel>();
            DataList.Add(new DataNameValueModel() { DataName = tbDataName.Text, DataValue = cbDataValue.Text });
            DataList.Add(new DataNameValueModel() { DataName = tbUshortName.Text, DataValue = tbUshortValue.Text });
            DataList.Add(new DataNameValueModel() { DataName = tbUintName.Text, DataValue = tbUintValue.Text });
            //string DataName = tbDataName.Text;
            //string DataValueStr = cbDataValue.Text;
            PlcDataType DataType;

            //TODO: Part of the Reading from PLC procedure. When set a list of string DataNames, use List<DataNames> and ForEach below sequence
            //1.determine the type of data and set its Value in the corresponding DataType list. 更新DataList的值

            foreach (var DataEntry in DataList)
            {
                if (PlcDataMapper.TypeBools.Any(x => x.Name == DataEntry.DataName))
                {
                    DataType = PlcDataType.TypeBool;
                    TypeBoolModel temp = PlcDataMapper.TypeBools.Find(x => x.Name == DataEntry.DataName);
                    temp.SetValue(DataEntry.DataValue);
                }
                else if (PlcDataMapper.TypeUshorts.Any(x => x.Name == DataEntry.DataName))
                {
                    DataType = PlcDataType.TypeUshort;
                    TypeUshortModel temp = PlcDataMapper.TypeUshorts.Find(x => x.Name == DataEntry.DataName);
                    temp.SetValue(DataEntry.DataValue);
                }
                else if (PlcDataMapper.TypeUints.Any(x => x.Name == DataEntry.DataName))
                {
                    DataType = PlcDataType.TypeUint;
                    TypeUintModel temp = PlcDataMapper.TypeUints.Find(x => x.Name == DataEntry.DataName);
                    temp.SetValue(DataEntry.DataValue);
                }
                else
                {
                    DataType = PlcDataType.Unassigned;
                }
            }           
            


            //Write All data to TypeBool Word List
            //foreach (var TypeBoolEntry in PlcDataMapper.TypeBools)
            //{
            //    PlcWordAddressModel tempWordAddress = plcDataAddressMappings.Find(
            //        x => x.PlcDataAddressRecord.DataName == TypeBoolEntry.Name).PlcWordAddress;
            //    PlcWordModel tempWord = PlcWordList.Find(x => x.MemoryArea == tempWordAddress.MemoryArea
            //    && x.WordAddress == tempWordAddress.WordAddress);
            //    if ( tempWordAddress.BitAddress!=null)
            //    {
            //        tempWord.SetBitValue((int)tempWordAddress.BitAddress, TypeBoolEntry.Value);
            //    }
            //}

            //foreach (var TypeUshortEntry in AllTypeUshort)
            //{
            //    PlcWordAddressModel tempWordAddress = plcDataAddressMappings.Find(
            //        x => x.PlcDataAddressRecord.DataName == TypeUshortEntry.Name).PlcWordAddress;
            //    PlcWordModel tempWord = PlcWordList.Find(x => x.MemoryArea == tempWordAddress.MemoryArea
            //    && x.WordAddress == tempWordAddress.WordAddress);

            //    tempWord.SetValue(TypeUshortEntry.ValueHexStr);
                
            //}

            //foreach (var TypeUintEntry in AllTypeUint)
            //{
            //    List<PlcDataAddressMappingModel> tempWordAddressMappingList = plcDataAddressMappings.FindAll(
            //        x => x.PlcDataAddressRecord.DataName == TypeUintEntry.Name);
            //    List<PlcWordModel> tempWordList = new List<PlcWordModel>();
            //    foreach (var tempWordAddressMappingEntry in tempWordAddressMappingList)
            //    {
            //        PlcWordModel tempWord = PlcWordList.Find(x => x.MemoryArea == tempWordAddressMappingEntry.PlcWordAddress.MemoryArea
            //        && x.WordAddress == tempWordAddressMappingEntry.PlcWordAddress.WordAddress);
            //        tempWordList.Add(tempWord);
            //    }
            //    List<PlcWordModel> SortedTempWordList = tempWordList.OrderBy(x => x.WordAddress).ToList();
            //    if (SortedTempWordList.Count==2)
            //    {
            //        string ValueStr = TypeUintEntry.ValueHexStr;
            //        SortedTempWordList[0].SetValue(ValueStr.Substring(4, 4));
            //        SortedTempWordList[1].SetValue(ValueStr.Substring(0, 4));
            //    }


            //}

            //dataGridViewAllOutput.DataSource = null;
            //dataGridViewAllOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //dataGridViewAllOutput.DataSource = PlcWordList.Select(u => new
            //{
            //    MemArea = u.MemoryArea,
            //    Address = u.WordAddress,
            //    WordValue = u.ValueStrHex

            //}).ToList();

            /*
            //2.1 Take the DataName and find all the entries inside the Mapping list
            List<PlcDataAddressMappingModel> MappingEntries = new List<PlcDataAddressMappingModel>();
            MappingEntries.AddRange(plcDataAddressMappings.FindAll(x => x.PlcDataAddressRecord.DataName == DataName));

            //2.2  Take the MappingEntries list and find all Word entries to be updated in Word List
            //List<PlcWordModel> PlcWordEntries = new List<PlcWordModel>();
            //PlcWordEntries.AddRange(PlcWordList.FindAll(x => x.PlcWordAddress.MemoryArea == entry.PlcWordAddress.MemoryArea
            //    && x.PlcWordAddress.WordAddress == entry.PlcWordAddress.WordAddress))


            List<PlcWordModel> PlcWordEntries = new List<PlcWordModel>();//将要update的Word清单
            foreach (var entry in MappingEntries)
            {   
                //PlcWordModel PlcWord_entry = new PlcWordModel();
                PlcWordEntries.Add(PlcWordList.Find(x => x.PlcWordAddress.MemoryArea == entry.PlcWordAddress.MemoryArea
                && x.PlcWordAddress.WordAddress == entry.PlcWordAddress.WordAddress));

                //PlcWord_entry = PlcWordList.Find(x => x.PlcWordAddress.MemoryArea == entry.PlcWordAddress.MemoryArea
                //&& x.PlcWordAddress.WordAddress == entry.PlcWordAddress.WordAddress);
                //PlcWord_entry.SetBitValue((int)entry.PlcWordAddress.BitAddress, DataValue);
            }

            //3. Update the DataValue to Word list

            List<PlcWordModel> PlcWordToUpdate = PlcWordList.Intersect(PlcWordEntries);

            foreach (var TypeBoolEntry in AllTypeBool)
            {

            }
            switch (DataType)
            {
                case PlcDataType.TypeBool:
                    PlcWordEntries[0].SetBitValue((int)PlcWordEntries[0].PlcWordAddress.BitAddress, DataValue);
                    break;
                case PlcDataType.TypeUshort:
                    break;
                case PlcDataType.TypeUint:
                    break;
                case PlcDataType.Unassigned:
                    break;
                default:
                    break;
            }*/


            //PlcDataAddressMappingModel entry = plcDataAddressMappings.Find(x => x.PlcDataAddressRecord.DataName == DataName);

            //2.Write the DataValue to the corresponding bit inside the WR Word list WR
            //WR.Find(x => x.MemoryArea == entry.PlcAddress.MemoryArea 
            //&& x.WordAddress == entry.PlcAddress.WordAddress).SetBitValue((int)entry.PlcAddress.BitAddress, DataValue);


            //TODO: Uint takes 2 words, need re-organize Load. 

            //3.Display Word address and its value
            //lblWAddress.Text = entry.PlcDataAddressRecord.PlcAddress;
            //lblValue.Text = PlcWord_entry.ValueStrHex;
        }

        /// <summary>
        /// OLD
        /// </summary>
        private void InitializeData()
        {
            //Load all Data Names with Writable attributes
            //AllTypeUint = GlobalConfig.DBConnection.GetTypeUint_All();
            //AllTypeUshort = GlobalConfig.DBConnection.GetTypeUshort_All();
            //AllTypeBool = GlobalConfig.DBConnection.GetTypeBool_All();

            ////Load Alarms
            //AllAlarms = GlobalConfig.DBConnection.GetAlarmCodes_All();

            ////Load addresses from DB
            //plcDataAddressRecords = GlobalConfig.DBConnection.GetDataAddressRecord_All();

            ////Load PLC serial port setting
            //plcPortSettings = GlobalConfig.DBConnection.GetPlcPortSetting_All();



            ////Populate the address mappings
            //plcDataAddressMappings = GlobalConfig.PopulatePlcDataAddressMappings(plcDataAddressRecords, plcDataAddressMappings);

            ////Complete the address mappings with full Uint address (2 words)
            //plcDataAddressMappings = GlobalConfig.AppendFullUintAddressMappings(AllTypeUint, plcDataAddressMappings);

            ////Populate default Words list
            //PlcWordList = GlobalConfig.PopulatePlcWordList_All(PlcWordList);

            

            ////Populate DataNameValue R,W list
            //foreach (var item in AllTypeBool)
            //{
            //    lock (PlcDataMapper._RWlistLock)
            //    {
            //        PlcDataMapper.DataNameValue_RWlist.Add(item.Name, item.ValueHexStr);
            //        //DataNameValue_Wlist.Add(item.Name, item.ValueHexStr);
            //    }

            //}
            //foreach (var item in AllTypeUshort)
            //{
            //    lock (PlcDataMapper._RWlistLock)
            //    {
            //        PlcDataMapper.DataNameValue_RWlist.Add(item.Name, item.ValueHexStr);
            //        //DataNameValue_Wlist.Add(item.Name, item.ValueHexStr); 
            //    }
            //}
            //foreach (var item in AllTypeUint)
            //{
            //    lock (PlcDataMapper._RWlistLock)
            //    {
            //        PlcDataMapper.DataNameValue_RWlist.Add(item.Name, item.ValueHexStr);
            //        //DataNameValue_Wlist.Add(item.Name, item.ValueHexStr); 
            //    }
            //}
        }

        private void PrintResult(int number)
        {
            rtbOutput.Text += $"{number.ToString()} counted" + Environment.NewLine;
        }

        private void WireupForm()
        {
            lblLoginGroup.Text = UserSettings.Default.CurrentUserGroup;
            //rtbOutput.Text += $"{number} counted"+Environment.NewLine;
            
            //dataGridViewAll.DataSource = null;
            //dataGridViewAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            
            //dataGridViewAll.DataSource = plcDataAddressMappings.Select(u => new
            //{
            //    ID = u.PlcDataAddressRecord.id,
            //    Name = u.PlcDataAddressRecord.DataName,
            //    Address = u.PlcDataAddressRecord.PlcAddress

            //}).ToList();
            //dataGridViewAll.Sort(dataGridViewAll.Columns["Address"], ListSortDirection.Ascending);
        }

        private void tbInput_Click(object sender, EventArgs e)
        {
            TextBox T = (TextBox)sender;
            using (KeyPad kp = new KeyPad())
            {
                if (kp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    T.Text = kp.value;
                }
            }
        }

        

        private void CylPlunger1_CheckedChanged(object sender, EventArgs e)
        {            
            CheckBox cb = (CheckBox)sender;
            cb.BackColor = Color.DimGray;
            cb.Enabled = false;
            
            //int l = cb.Name.Length;
            string Name = cb.Name.Substring(3, (cb.Name.Length-3));
            if (cb.Checked)
            {
                //if (PlcDataMapper.DataNameValue_Rlist[Name].Value=="1")
                //{
                //    cb.BackColor = Color.Gold;
                //}
                

                PlcDataMapper.DataNameValue_Wlist[Name] = "1";
                rtbOutput.Text += $"Data name is {Name} and Value is {PlcDataMapper.DataNameValue_Wlist[Name]}";
            }
            else
            {
                //cb.BackColor = Color.LightGray;
                PlcDataMapper.DataNameValue_Wlist[Name] = "0";
                rtbOutput.Text += $"Data name is {Name} and Value is {PlcDataMapper.DataNameValue_Wlist[Name]}";
            }

            dataGridViewAllOutput.DataSource = null;
            dataGridViewAllOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridViewAllOutput.DataSource = PlcDataMapper.DataNameValue_Wlist.Select(u => new
            {
                Name = u.Key,
                Value = u.Value
                

            }).ToList();

        }


        private void timerUpdateRlist_Tick(object sender, EventArgs e)
        {
            foreach (var item in PlcDataMapper.DataNameValue_Wlist)
            {
                PlcDataMapper.DataNameValue_Rlist[item.Key].Value = item.Value;
            }
        }

        private void btnCancelAsync_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void BtnLogon_Click(object sender, EventArgs e)
        {
            using (LoginForm lf = new LoginForm())
            {
                if (lf.ShowDialog()==DialogResult.OK)
                {
                    lblLoginGroup.Text = lf.LoginGroup;
                    lblLoginName.Text = lf.LoginName;
                }
                else
                {
                    lblLoginGroup.Text = "Operator";
                    lblLoginName.Text = "No User";
                }

            }
        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void BtnLogoff_Click(object sender, EventArgs e)
        {
            UserSettings.Default.CurrentUserGroup = "Operator";
            lblLoginGroup.Text = UserSettings.Default.CurrentUserGroup;
            lblLoginName.Text = "";

        }

        private void BtnStats_Click(object sender, EventArgs e)
        {

        }

        private void BtnService_Click(object sender, EventArgs e)
        {
            bool access = GlobalConfig.VerifyButtonAccess(((Control)sender).Name, this.Name, UserSettings.Default.CurrentUserGroup);
            if (!access)
            {
                MessageBox.Show("Insufficient user rights, current group: " + UserSettings.Default.CurrentUserGroup,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ServiceForm frm = new ServiceForm();
                frm.Owner = this;
                frm.Show();
                Hide();
                frm.VisibleChanged += SubForm_VisibleChanged;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {

        }

        private void ExportAddress_Click(object sender, EventArgs e)
        {
            PlcDataMapper.ExportDataAddresses();
        }

        private void BtnCylPlunger1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            PlcDataMapper.DataNameValue_Wlist[btn.Name.Substring(3, (btn.Name.Length - 3))] = "1";
            rtbOutput.Text += $"Data name is {btn.Name} and Value is {PlcDataMapper.DataNameValue_Wlist[btn.Name.Substring(3, (btn.Name.Length - 3))]}";
            //Console.WriteLine($"Data name is {DataNameValue[btn.Name]} and Value is {DataNameValue[btn.Name]}");
            dataGridViewAllOutput.DataSource = null;
            dataGridViewAllOutput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGridViewAllOutput.DataSource = PlcDataMapper.DataNameValue_Wlist.Select(u => new
            {
                Name = u.Key,
                Value = u.Value


            }).ToList();
        }
    }
}
