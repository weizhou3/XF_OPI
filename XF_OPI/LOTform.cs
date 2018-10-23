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
    public partial class LOTform : Form
    {
        public LOTform()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            using (KeyPad kp = new KeyPad())
            {
                if (kp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtLOTid.Text = kp.value;
                    //T.Text = np.value;
                }
            }
        }
    }
    
}
