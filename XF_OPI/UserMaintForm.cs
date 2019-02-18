using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XFOPI_Library;
using XFOPI_Library.Models;

namespace XF_OPI
{
    public partial class UserMaintForm : Form
    {
        private List<UserModel> allUsers = new List<UserModel>();//GlobalConfig.DBConnection.GetUsers_All();
        private List<GroupModel> allGroups = new List<GroupModel>();//GlobalConfig.DBConnection.GetGroups_All();
        private UserModel SelectedUser = new UserModel();

        public UserMaintForm()
        {
            InitializeComponent();
            //CreateSampleData();
            WireUpLists();
        }

        private void CreateSampleData()
        {
            UserModel dz = new UserModel { FirstName = "David", LastName = "Zhou" };
            UserModel mz = new UserModel { FirstName = "Mingy", LastName = "Zhang" };
            allUsers.Add(dz);
            allUsers.Add(mz);
        }

        private void WireUpLists()
        {
            allUsers = GlobalConfig.DBConnection.GetUsers_All();
            allGroups= GlobalConfig.DBConnection.GetGroups_All();

            dataGridViewAllUsers.DataSource = null;
            dataGridViewAllUsers.DataSource = allUsers.Select(u=> new { Name=u.FullName,UserGroup=u.Group}).ToList();

            cBoxUserGroup.DataSource = null;
            cBoxUserGroup.DataSource = allGroups;
            cBoxUserGroup.DisplayMember = "GroupName";
        }

        private void cBoxUserGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                UserModel model = new UserModel(tBoxFN.Text, tBoxLN.Text, tBoxEmail.Text, tBoxPhone.Text, tBoxID.Text, cBoxUserGroup.Text);
                UserAccessGroup uag;
                switch (cBoxUserGroup.Text)
                {
                    case "Admin":
                        uag = UserAccessGroup.Admin;
                        break;
                    case "Maint":
                        uag = UserAccessGroup.Maint;
                        break;
                    case "Operator":
                        uag = UserAccessGroup.Operator;
                        break;
                    default:
                        uag = UserAccessGroup.Operator;
                        break;
                }

                GlobalConfig.DBConnection.CreateUser(model,uag);

                tBoxFN.Text = "";
                tBoxLN.Text = "";
                tBoxEmail.Text = "";
                tBoxPhone.Text = "";
                tBoxID.Text = "";
                WireUpLists();
                //this.DialogResult = DialogResult.OK;
                //this.Close();
            }
            else
            {
                MessageBox.Show("Missing or invalid input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            bool output = true;

            if (tBoxFN.Text.Length == 0)
                output = false;
            if (tBoxLN.Text.Length == 0)
                output = false;
            if (cBoxUserGroup.Text != "Admin" && cBoxUserGroup.Text != "Maint" && cBoxUserGroup.Text != "Operator")
                output = false;

            return output;
        }

        private void TBoxKP_Click(object sender, EventArgs e)
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

        private void btnDeleteSelectedUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllUsers.SelectedCells.Count>0)
            {
                //Get selected Full Name
                int rowIndex = dataGridViewAllUsers.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewAllUsers.Rows[rowIndex];
                string fullName = selectedRow.Cells[0].Value.ToString();

                //Find the UserModel to delete
                UserModel user = allUsers.Where(x => x.FullName == fullName).First();

                //delete user
                GlobalConfig.DBConnection.DeleteUser(user);
            }
            WireUpLists();
            
        }

        private void UserMaintForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewAllUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
