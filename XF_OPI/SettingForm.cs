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
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void btnRtnMain_Click(object sender, EventArgs e)
        {

            //this.VisibleChanged += SettingForm_VisibleChanged;
            this.Hide();


        }

        //private void SettingForm_VisibleChanged(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
