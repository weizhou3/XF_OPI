namespace XF_OPI
{
    partial class TestPLCConn
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
            this.btnRdAll = new System.Windows.Forms.Button();
            this.btnWrtAll = new System.Windows.Forms.Button();
            this.btnLoopRW = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoopCount = new System.Windows.Forms.TextBox();
            this.PLCRWtimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRdAll
            // 
            this.btnRdAll.Location = new System.Drawing.Point(67, 58);
            this.btnRdAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnRdAll.Name = "btnRdAll";
            this.btnRdAll.Size = new System.Drawing.Size(172, 80);
            this.btnRdAll.TabIndex = 0;
            this.btnRdAll.Text = "Read ALL";
            this.btnRdAll.UseVisualStyleBackColor = true;
            this.btnRdAll.Click += new System.EventHandler(this.btnRdAll_Click);
            // 
            // btnWrtAll
            // 
            this.btnWrtAll.Location = new System.Drawing.Point(67, 170);
            this.btnWrtAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnWrtAll.Name = "btnWrtAll";
            this.btnWrtAll.Size = new System.Drawing.Size(172, 80);
            this.btnWrtAll.TabIndex = 1;
            this.btnWrtAll.Text = "Write ALL";
            this.btnWrtAll.UseVisualStyleBackColor = true;
            this.btnWrtAll.Click += new System.EventHandler(this.btnWrtAll_Click);
            // 
            // btnLoopRW
            // 
            this.btnLoopRW.Location = new System.Drawing.Point(67, 292);
            this.btnLoopRW.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoopRW.Name = "btnLoopRW";
            this.btnLoopRW.Size = new System.Drawing.Size(172, 80);
            this.btnLoopRW.TabIndex = 2;
            this.btnLoopRW.Text = "Loop R/W";
            this.btnLoopRW.UseVisualStyleBackColor = true;
            this.btnLoopRW.Click += new System.EventHandler(this.btnLoopRW_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(291, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Read all W and H 0-511";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Write 0011 to all W and H 0-511";
            // 
            // LoopCount
            // 
            this.LoopCount.Location = new System.Drawing.Point(297, 314);
            this.LoopCount.Name = "LoopCount";
            this.LoopCount.Size = new System.Drawing.Size(100, 30);
            this.LoopCount.TabIndex = 5;
            this.LoopCount.Text = "10";
            // 
            // PLCRWtimer
            // 
            this.PLCRWtimer.Tick += new System.EventHandler(this.PLCRWtimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(860, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 85);
            this.button1.TabIndex = 6;
            this.button1.Text = "Change Lable color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestPLCConn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 710);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LoopCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoopRW);
            this.Controls.Add(this.btnWrtAll);
            this.Controls.Add(this.btnRdAll);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TestPLCConn";
            this.Text = "Test PLC connection";
            this.Load += new System.EventHandler(this.TestPLCConn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRdAll;
        private System.Windows.Forms.Button btnWrtAll;
        private System.Windows.Forms.Button btnLoopRW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox LoopCount;
        private System.Windows.Forms.Timer PLCRWtimer;
        private System.Windows.Forms.Button button1;
    }
}