using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XF_OPI
{
    public partial class FTPConfigForm : Form
    {
        public FTPConfigForm()
        {
            InitializeComponent();
        }

        public string FTPServerAddress;
        public string FTPUserName;
        public string FTPPassword;

        private void btn_save_Click(object sender, EventArgs e)
        {
            FTPServerAddress = txt_server.Text;
            FTPUserName = txt_username.Text;
            FTPPassword = txt_pw.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_KP_Click(object sender, EventArgs e)
        {
            TextBox T = (TextBox)sender;
            using (KeyPad kp = new KeyPad())
            {
                if (kp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //this.textBox5.Text = np.value;
                    if (T.Name == "txt_server")
                        T.Text = "ftp://" + kp.value;
                    else
                        T.Text = kp.value;
                }
            }
        }

        private void FTPConfigForm_Load(object sender, EventArgs e)
        {

        }
    }
}
