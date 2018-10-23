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

namespace XF_OPI
{
    public partial class MainForm : Form
    {

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
    }
}
