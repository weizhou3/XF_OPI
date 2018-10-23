using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMgmtLibrary;
using UserMgmtLibrary.Models;

namespace XF_OPI
{
    public partial class UserMaintForm : Form
    {
        private List<UserModel> allUsers = GlobalConfig.Connection.GetUsers_All();
        private List<UserModel> groupUsers = new List<UserModel>();
        private List<UserModel> selectedUsers = new List<UserModel>();
        private List<GroupModel> selectedGroup = new List<GroupModel>();

        public UserMaintForm()
        {
            InitializeComponent();
            CreateSampleData();
            WireUpLists();
        }

        private void CreateSampleData()
        {
            UserModel dz = new UserModel { FirstName = "David", LastName = "Zhou" };
            UserModel mz = new UserModel { FirstName = "Mingyao", LastName = "Zhang" };
            groupUsers.Add(dz);
            groupUsers.Add(mz);
            selectedGroup.Add(new GroupModel { GroupMembers = groupUsers, GroupName = "Admin" });
        }

        private void WireUpLists()
        {
            listBoxUsersInGroup.DataSource = null;
            listBoxUsersInGroup.DataSource = selectedUsers;
            listBoxUsersInGroup.DisplayMember = "FullName";

            cBoxSelectedUser.DataSource = null;
            cBoxSelectedUser.DataSource = allUsers;
            cBoxSelectedUser.DisplayMember = "FullName";
        }

        

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            using (EditUserForm userEdit = new EditUserForm())
            {
                if (userEdit.ShowDialog() == DialogResult.OK) { }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cBoxUserGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            UserModel u = (UserModel)cBoxSelectedUser.SelectedItem;
            allUsers.Remove(u);
            selectedUsers.Add(u);
            WireUpLists();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                UserModel model = new UserModel(tBoxFN.Text, tBoxLN.Text, tBoxEmail.Text, tBoxPhone.Text, tBoxID.Text);

                GlobalConfig.Connection.CreateUser(model);

                tBoxFN.Text = "";
                tBoxLN.Text = "";
                tBoxEmail.Text = "";
                tBoxPhone.Text = "";
                tBoxID.Text = "";
                this.DialogResult = DialogResult.OK;
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

        private void tBoxKP_Click(object sender, EventArgs e)
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
    }
}
