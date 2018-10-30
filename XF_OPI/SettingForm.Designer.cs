namespace XF_OPI
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.btnRtnMain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRtnMain
            // 
            this.btnRtnMain.Location = new System.Drawing.Point(1119, 2);
            this.btnRtnMain.Name = "btnRtnMain";
            this.btnRtnMain.Size = new System.Drawing.Size(113, 69);
            this.btnRtnMain.TabIndex = 0;
            this.btnRtnMain.Text = "MAIN";
            this.btnRtnMain.UseVisualStyleBackColor = true;
            this.btnRtnMain.Click += new System.EventHandler(this.btnRtnMain_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 653);
            this.Controls.Add(this.btnRtnMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRtnMain;

        
    }
}