namespace XF_OPI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnLogoff = new System.Windows.Forms.Button();
            this.btnLogon = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnService = new System.Windows.Forms.Button();
            this.btnEN = new System.Windows.Forms.Button();
            this.btnCN = new System.Windows.Forms.Button();
            this.lblSetting = new System.Windows.Forms.Label();
            this.lblLogOff = new System.Windows.Forms.Label();
            this.lblLogOn = new System.Windows.Forms.Label();
            this.lblUsers = new System.Windows.Forms.Label();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNewLOT = new System.Windows.Forms.Button();
            this.PLCPort = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.TestPLC = new System.Windows.Forms.Button();
            this.dataGridViewAll = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dataGridViewAllOutput = new System.Windows.Forms.DataGridView();
            this.tbDataName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSetValue = new System.Windows.Forms.Button();
            this.cbDataValue = new System.Windows.Forms.ComboBox();
            this.lblWAddress = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbUshortName = new System.Windows.Forms.TextBox();
            this.tbUshortValue = new System.Windows.Forms.TextBox();
            this.tbUintValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbUintName = new System.Windows.Forms.TextBox();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.sqLiteCommandBuilder1 = new System.Data.SQLite.SQLiteCommandBuilder();
            this.chbCylPlunger1 = new System.Windows.Forms.CheckBox();
            this.timerUpdateRlist = new System.Windows.Forms.Timer(this.components);
            this.progressBarDashboard = new System.Windows.Forms.ProgressBar();
            this.btnTestAsync = new System.Windows.Forms.Button();
            this.btnCancelAsync = new System.Windows.Forms.Button();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.lblLoginName = new System.Windows.Forms.Label();
            this.lblLoginGroup = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblStop = new System.Windows.Forms.Label();
            this.ExportAddress = new System.Windows.Forms.Button();
            this.tBoxTimeout = new System.Windows.Forms.TextBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.btnCylPlunger1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllOutput)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnSetting, "btnSetting");
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnLogoff
            // 
            this.btnLogoff.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnLogoff, "btnLogoff");
            this.btnLogoff.Name = "btnLogoff";
            this.btnLogoff.UseVisualStyleBackColor = false;
            this.btnLogoff.Click += new System.EventHandler(this.BtnLogoff_Click);
            // 
            // btnLogon
            // 
            this.btnLogon.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnLogon, "btnLogon");
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.UseVisualStyleBackColor = false;
            this.btnLogon.Click += new System.EventHandler(this.BtnLogon_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnUsers, "btnUsers");
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnStats
            // 
            this.btnStats.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnStats, "btnStats");
            this.btnStats.Name = "btnStats";
            this.btnStats.UseVisualStyleBackColor = false;
            this.btnStats.Click += new System.EventHandler(this.BtnStats_Click);
            // 
            // btnService
            // 
            this.btnService.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnService, "btnService");
            this.btnService.Name = "btnService";
            this.btnService.UseVisualStyleBackColor = false;
            this.btnService.Click += new System.EventHandler(this.BtnService_Click);
            // 
            // btnEN
            // 
            this.btnEN.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnEN, "btnEN");
            this.btnEN.Name = "btnEN";
            this.btnEN.UseVisualStyleBackColor = false;
            this.btnEN.Click += new System.EventHandler(this.ButtonEN_Click);
            // 
            // btnCN
            // 
            this.btnCN.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnCN, "btnCN");
            this.btnCN.Name = "btnCN";
            this.btnCN.UseVisualStyleBackColor = false;
            this.btnCN.Click += new System.EventHandler(this.ButtonCN_Click);
            // 
            // lblSetting
            // 
            resources.ApplyResources(this.lblSetting, "lblSetting");
            this.lblSetting.Name = "lblSetting";
            // 
            // lblLogOff
            // 
            resources.ApplyResources(this.lblLogOff, "lblLogOff");
            this.lblLogOff.Name = "lblLogOff";
            // 
            // lblLogOn
            // 
            resources.ApplyResources(this.lblLogOn, "lblLogOn");
            this.lblLogOn.Name = "lblLogOn";
            // 
            // lblUsers
            // 
            resources.ApplyResources(this.lblUsers, "lblUsers");
            this.lblUsers.Name = "lblUsers";
            // 
            // lblStats
            // 
            resources.ApplyResources(this.lblStats, "lblStats");
            this.lblStats.Name = "lblStats";
            // 
            // lblService
            // 
            resources.ApplyResources(this.lblService, "lblService");
            this.lblService.Name = "lblService";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNewLOT
            // 
            resources.ApplyResources(this.btnNewLOT, "btnNewLOT");
            this.btnNewLOT.Name = "btnNewLOT";
            this.btnNewLOT.UseVisualStyleBackColor = true;
            this.btnNewLOT.Click += new System.EventHandler(this.button2_Click);
            // 
            // PLCPort
            // 
            this.PLCPort.BaudRate = 115200;
            this.PLCPort.DataBits = 7;
            this.PLCPort.Parity = System.IO.Ports.Parity.Even;
            this.PLCPort.PortName = "COM4";
            this.PLCPort.StopBits = System.IO.Ports.StopBits.Two;
            // 
            // TestPLC
            // 
            resources.ApplyResources(this.TestPLC, "TestPLC");
            this.TestPLC.Name = "TestPLC";
            this.TestPLC.UseVisualStyleBackColor = true;
            this.TestPLC.Click += new System.EventHandler(this.TestPLC_Click);
            // 
            // dataGridViewAll
            // 
            this.dataGridViewAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewAll, "dataGridViewAll");
            this.dataGridViewAll.Name = "dataGridViewAll";
            this.dataGridViewAll.RowTemplate.Height = 28;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // dataGridViewAllOutput
            // 
            this.dataGridViewAllOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewAllOutput, "dataGridViewAllOutput");
            this.dataGridViewAllOutput.Name = "dataGridViewAllOutput";
            this.dataGridViewAllOutput.RowTemplate.Height = 28;
            // 
            // tbDataName
            // 
            resources.ApplyResources(this.tbDataName, "tbDataName");
            this.tbDataName.Name = "tbDataName";
            this.tbDataName.Click += new System.EventHandler(this.tbInput_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // btnSetValue
            // 
            resources.ApplyResources(this.btnSetValue, "btnSetValue");
            this.btnSetValue.Name = "btnSetValue";
            this.btnSetValue.UseVisualStyleBackColor = true;
            this.btnSetValue.Click += new System.EventHandler(this.btnSetValue_Click);
            // 
            // cbDataValue
            // 
            this.cbDataValue.AllowDrop = true;
            this.cbDataValue.FormattingEnabled = true;
            this.cbDataValue.Items.AddRange(new object[] {
            resources.GetString("cbDataValue.Items"),
            resources.GetString("cbDataValue.Items1")});
            resources.ApplyResources(this.cbDataValue, "cbDataValue");
            this.cbDataValue.Name = "cbDataValue";
            // 
            // lblWAddress
            // 
            resources.ApplyResources(this.lblWAddress, "lblWAddress");
            this.lblWAddress.BackColor = System.Drawing.Color.White;
            this.lblWAddress.Name = "lblWAddress";
            // 
            // lblValue
            // 
            resources.ApplyResources(this.lblValue, "lblValue");
            this.lblValue.BackColor = System.Drawing.Color.White;
            this.lblValue.Name = "lblValue";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tbUshortName
            // 
            resources.ApplyResources(this.tbUshortName, "tbUshortName");
            this.tbUshortName.Name = "tbUshortName";
            this.tbUshortName.Click += new System.EventHandler(this.tbInput_Click);
            // 
            // tbUshortValue
            // 
            resources.ApplyResources(this.tbUshortValue, "tbUshortValue");
            this.tbUshortValue.Name = "tbUshortValue";
            this.tbUshortValue.Click += new System.EventHandler(this.tbInput_Click);
            // 
            // tbUintValue
            // 
            resources.ApplyResources(this.tbUintValue, "tbUintValue");
            this.tbUintValue.Name = "tbUintValue";
            this.tbUintValue.Click += new System.EventHandler(this.tbInput_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // tbUintName
            // 
            resources.ApplyResources(this.tbUintName, "tbUintName");
            this.tbUintName.Name = "tbUintName";
            this.tbUintName.Click += new System.EventHandler(this.tbInput_Click);
            // 
            // rtbOutput
            // 
            resources.ApplyResources(this.rtbOutput, "rtbOutput");
            this.rtbOutput.Name = "rtbOutput";
            // 
            // sqLiteCommandBuilder1
            // 
            this.sqLiteCommandBuilder1.DataAdapter = null;
            this.sqLiteCommandBuilder1.QuoteSuffix = "]";
            // 
            // chbCylPlunger1
            // 
            resources.ApplyResources(this.chbCylPlunger1, "chbCylPlunger1");
            this.chbCylPlunger1.Name = "chbCylPlunger1";
            this.chbCylPlunger1.UseVisualStyleBackColor = true;
            this.chbCylPlunger1.CheckedChanged += new System.EventHandler(this.CylPlunger1_CheckedChanged);
            // 
            // timerUpdateRlist
            // 
            this.timerUpdateRlist.Tick += new System.EventHandler(this.timerUpdateRlist_Tick);
            // 
            // progressBarDashboard
            // 
            resources.ApplyResources(this.progressBarDashboard, "progressBarDashboard");
            this.progressBarDashboard.Name = "progressBarDashboard";
            // 
            // btnTestAsync
            // 
            resources.ApplyResources(this.btnTestAsync, "btnTestAsync");
            this.btnTestAsync.Name = "btnTestAsync";
            this.btnTestAsync.UseVisualStyleBackColor = true;
            this.btnTestAsync.Click += new System.EventHandler(this.btnTestAsync_Click);
            // 
            // btnCancelAsync
            // 
            resources.ApplyResources(this.btnCancelAsync, "btnCancelAsync");
            this.btnCancelAsync.Name = "btnCancelAsync";
            this.btnCancelAsync.UseVisualStyleBackColor = true;
            this.btnCancelAsync.Click += new System.EventHandler(this.btnCancelAsync_Click);
            // 
            // lblCurrentUser
            // 
            resources.ApplyResources(this.lblCurrentUser, "lblCurrentUser");
            this.lblCurrentUser.Name = "lblCurrentUser";
            // 
            // lblLoginName
            // 
            resources.ApplyResources(this.lblLoginName, "lblLoginName");
            this.lblLoginName.BackColor = System.Drawing.Color.White;
            this.lblLoginName.Name = "lblLoginName";
            // 
            // lblLoginGroup
            // 
            resources.ApplyResources(this.lblLoginGroup, "lblLoginGroup");
            this.lblLoginGroup.BackColor = System.Drawing.Color.White;
            this.lblLoginGroup.Name = "lblLoginGroup";
            this.lblLoginGroup.Click += new System.EventHandler(this.Label15_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = false;
            // 
            // lblStart
            // 
            resources.ApplyResources(this.lblStart, "lblStart");
            this.lblStart.Name = "lblStart";
            // 
            // lblStop
            // 
            resources.ApplyResources(this.lblStop, "lblStop");
            this.lblStop.Name = "lblStop";
            // 
            // ExportAddress
            // 
            resources.ApplyResources(this.ExportAddress, "ExportAddress");
            this.ExportAddress.Name = "ExportAddress";
            this.ExportAddress.UseVisualStyleBackColor = true;
            this.ExportAddress.Click += new System.EventHandler(this.ExportAddress_Click);
            // 
            // tBoxTimeout
            // 
            resources.ApplyResources(this.tBoxTimeout, "tBoxTimeout");
            this.tBoxTimeout.Name = "tBoxTimeout";
            // 
            // lblTimeout
            // 
            resources.ApplyResources(this.lblTimeout, "lblTimeout");
            this.lblTimeout.Name = "lblTimeout";
            // 
            // btnCylPlunger1
            // 
            resources.ApplyResources(this.btnCylPlunger1, "btnCylPlunger1");
            this.btnCylPlunger1.Name = "btnCylPlunger1";
            this.btnCylPlunger1.UseVisualStyleBackColor = true;
            this.btnCylPlunger1.Click += new System.EventHandler(this.BtnCylPlunger1_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCylPlunger1);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.tBoxTimeout);
            this.Controls.Add(this.ExportAddress);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblStop);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblLoginGroup);
            this.Controls.Add(this.lblLoginName);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.btnCancelAsync);
            this.Controls.Add(this.btnTestAsync);
            this.Controls.Add(this.progressBarDashboard);
            this.Controls.Add(this.chbCylPlunger1);
            this.Controls.Add(this.rtbOutput);
            this.Controls.Add(this.tbUintValue);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbUintName);
            this.Controls.Add(this.tbUshortValue);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbUshortName);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblWAddress);
            this.Controls.Add(this.cbDataValue);
            this.Controls.Add(this.btnSetValue);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDataName);
            this.Controls.Add(this.dataGridViewAllOutput);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.dataGridViewAll);
            this.Controls.Add(this.TestPLC);
            this.Controls.Add(this.btnNewLOT);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblService);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.lblUsers);
            this.Controls.Add(this.lblLogOn);
            this.Controls.Add(this.lblLogOff);
            this.Controls.Add(this.lblSetting);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnLogoff);
            this.Controls.Add(this.btnLogon);
            this.Controls.Add(this.btnUsers);
            this.Controls.Add(this.btnStats);
            this.Controls.Add(this.btnService);
            this.Controls.Add(this.btnEN);
            this.Controls.Add(this.btnCN);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnSetting;
        internal System.Windows.Forms.Button btnLogoff;
        internal System.Windows.Forms.Button btnLogon;
        internal System.Windows.Forms.Button btnUsers;
        internal System.Windows.Forms.Button btnStats;
        internal System.Windows.Forms.Button btnService;
        internal System.Windows.Forms.Button btnEN;
        internal System.Windows.Forms.Button btnCN;
        private System.Windows.Forms.Label lblSetting;
        private System.Windows.Forms.Label lblLogOff;
        private System.Windows.Forms.Label lblLogOn;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNewLOT;
        private System.IO.Ports.SerialPort PLCPort;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Button TestPLC;
        private System.Windows.Forms.DataGridView dataGridViewAll;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridViewAllOutput;
        private System.Windows.Forms.TextBox tbDataName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSetValue;
        private System.Windows.Forms.ComboBox cbDataValue;
        private System.Windows.Forms.Label lblWAddress;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbUshortName;
        private System.Windows.Forms.TextBox tbUshortValue;
        private System.Windows.Forms.TextBox tbUintValue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbUintName;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Data.SQLite.SQLiteCommandBuilder sqLiteCommandBuilder1;
        private System.Windows.Forms.CheckBox chbCylPlunger1;
        private System.Windows.Forms.Timer timerUpdateRlist;
        private System.Windows.Forms.ProgressBar progressBarDashboard;
        private System.Windows.Forms.Button btnTestAsync;
        private System.Windows.Forms.Button btnCancelAsync;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.Label lblLoginGroup;
        internal System.Windows.Forms.Button btnStart;
        internal System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblStop;
        private System.Windows.Forms.Button ExportAddress;
        private System.Windows.Forms.TextBox tBoxTimeout;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.Button btnCylPlunger1;
    }
}

