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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNewLOT = new System.Windows.Forms.Button();
            this.PLCPort = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.TestPLC = new System.Windows.Forms.Button();
            this.btnInitialModels = new System.Windows.Forms.Button();
            this.dataGridViewAllTypeUint = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dataGridViewAllAlarms = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.tbDataName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSetValue = new System.Windows.Forms.Button();
            this.cbDataValue = new System.Windows.Forms.ComboBox();
            this.lblWAddress = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllTypeUint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllAlarms)).BeginInit();
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
            // 
            // btnLogon
            // 
            this.btnLogon.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnLogon, "btnLogon");
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.UseVisualStyleBackColor = false;
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
            // 
            // btnService
            // 
            this.btnService.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnService, "btnService");
            this.btnService.Name = "btnService";
            this.btnService.UseVisualStyleBackColor = false;
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
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
            // btnInitialModels
            // 
            resources.ApplyResources(this.btnInitialModels, "btnInitialModels");
            this.btnInitialModels.Name = "btnInitialModels";
            this.btnInitialModels.UseVisualStyleBackColor = true;
            this.btnInitialModels.Click += new System.EventHandler(this.btnInitialModels_Click);
            // 
            // dataGridViewAllTypeUint
            // 
            this.dataGridViewAllTypeUint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewAllTypeUint, "dataGridViewAllTypeUint");
            this.dataGridViewAllTypeUint.Name = "dataGridViewAllTypeUint";
            this.dataGridViewAllTypeUint.RowTemplate.Height = 28;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // dataGridViewAllAlarms
            // 
            this.dataGridViewAllAlarms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewAllAlarms, "dataGridViewAllAlarms");
            this.dataGridViewAllAlarms.Name = "dataGridViewAllAlarms";
            this.dataGridViewAllAlarms.RowTemplate.Height = 28;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // tbDataName
            // 
            resources.ApplyResources(this.tbDataName, "tbDataName");
            this.tbDataName.Name = "tbDataName";
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
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.lblWAddress);
            this.Controls.Add(this.cbDataValue);
            this.Controls.Add(this.btnSetValue);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDataName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridViewAllAlarms);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.dataGridViewAllTypeUint);
            this.Controls.Add(this.btnInitialModels);
            this.Controls.Add(this.TestPLC);
            this.Controls.Add(this.btnNewLOT);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllTypeUint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllAlarms)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNewLOT;
        private System.IO.Ports.SerialPort PLCPort;
        private System.IO.Ports.SerialPort serialPort2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button TestPLC;
        private System.Windows.Forms.Button btnInitialModels;
        private System.Windows.Forms.DataGridView dataGridViewAllTypeUint;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridViewAllAlarms;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbDataName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSetValue;
        private System.Windows.Forms.ComboBox cbDataValue;
        private System.Windows.Forms.Label lblWAddress;
        private System.Windows.Forms.Label lblValue;
    }
}

