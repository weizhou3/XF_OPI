namespace XF_OPI
{
    partial class DataExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataExport));
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnToday = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnFrmHrPlus = new System.Windows.Forms.Button();
            this.BtnFrmHrMinus = new System.Windows.Forms.Button();
            this.BtnToHrMinus = new System.Windows.Forms.Button();
            this.BtnToHrPlus = new System.Windows.Forms.Button();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.BtnExportMUBA = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.BtnSaveRecipe = new System.Windows.Forms.Button();
            this.BtnLdRecipe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TBoxMUBA = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.SuspendLayout();
            // 
            // dateTimePickerStartTime
            // 
            resources.ApplyResources(this.dateTimePickerStartTime, "dateTimePickerStartTime");
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnToday
            // 
            resources.ApplyResources(this.BtnToday, "BtnToday");
            this.BtnToday.Name = "BtnToday";
            this.BtnToday.Click += new System.EventHandler(this.BtnToday_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // BtnFrmHrPlus
            // 
            resources.ApplyResources(this.BtnFrmHrPlus, "BtnFrmHrPlus");
            this.BtnFrmHrPlus.Name = "BtnFrmHrPlus";
            this.BtnFrmHrPlus.Click += new System.EventHandler(this.HrPlus_Click);
            // 
            // BtnFrmHrMinus
            // 
            resources.ApplyResources(this.BtnFrmHrMinus, "BtnFrmHrMinus");
            this.BtnFrmHrMinus.Name = "BtnFrmHrMinus";
            this.BtnFrmHrMinus.Click += new System.EventHandler(this.HrPlus_Click);
            // 
            // BtnToHrMinus
            // 
            resources.ApplyResources(this.BtnToHrMinus, "BtnToHrMinus");
            this.BtnToHrMinus.Name = "BtnToHrMinus";
            this.BtnToHrMinus.Click += new System.EventHandler(this.HrPlus_Click);
            // 
            // BtnToHrPlus
            // 
            resources.ApplyResources(this.BtnToHrPlus, "BtnToHrPlus");
            this.BtnToHrPlus.Name = "BtnToHrPlus";
            this.BtnToHrPlus.Click += new System.EventHandler(this.HrPlus_Click);
            // 
            // dateTimePickerEndTime
            // 
            resources.ApplyResources(this.dateTimePickerEndTime, "dateTimePickerEndTime");
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            // 
            // BtnExportMUBA
            // 
            resources.ApplyResources(this.BtnExportMUBA, "BtnExportMUBA");
            this.BtnExportMUBA.Name = "BtnExportMUBA";
            this.BtnExportMUBA.Click += new System.EventHandler(this.BtnExportMUBA_Click);
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // BtnSaveRecipe
            // 
            resources.ApplyResources(this.BtnSaveRecipe, "BtnSaveRecipe");
            this.BtnSaveRecipe.Name = "BtnSaveRecipe";
            // 
            // BtnLdRecipe
            // 
            resources.ApplyResources(this.BtnLdRecipe, "BtnLdRecipe");
            this.BtnLdRecipe.Name = "BtnLdRecipe";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // TBoxMUBA
            // 
            resources.ApplyResources(this.TBoxMUBA, "TBoxMUBA");
            this.TBoxMUBA.Name = "TBoxMUBA";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 57600;
            this.serialPort1.DataBits = 7;
            this.serialPort1.Parity = System.IO.Ports.Parity.Even;
            this.serialPort1.PortName = "COM4";
            this.serialPort1.StopBits = System.IO.Ports.StopBits.Two;
            // 
            // DataExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TBoxMUBA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnLdRecipe);
            this.Controls.Add(this.BtnSaveRecipe);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnExportMUBA);
            this.Controls.Add(this.dateTimePickerEndTime);
            this.Controls.Add(this.BtnToHrMinus);
            this.Controls.Add(this.BtnToHrPlus);
            this.Controls.Add(this.BtnFrmHrMinus);
            this.Controls.Add(this.BtnFrmHrPlus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnToday);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerStartTime);
            this.Name = "DataExport";
            this.Load += new System.EventHandler(this.DataExport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnToday;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnFrmHrPlus;
        private System.Windows.Forms.Button BtnFrmHrMinus;
        private System.Windows.Forms.Button BtnToHrMinus;
        private System.Windows.Forms.Button BtnToHrPlus;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.Button BtnExportMUBA;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button BtnSaveRecipe;
        private System.Windows.Forms.Button BtnLdRecipe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBoxMUBA;
        private System.IO.Ports.SerialPort serialPort1;
    }
}