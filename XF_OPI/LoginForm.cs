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
    public partial class LoginForm : Form
    {
        private List<UserModel> allUsers = new List<UserModel>();//GlobalConfig.DBConnection.GetUsers_All();
        private List<GroupModel> allGroups = new List<GroupModel>();//GlobalConfig.DBConnection.GetGroups_All();
        private UserModel SelectedUser = new UserModel();
        public string LoginName { get; set; }
        public string LoginGroup { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        private void WireUpLists()
        {
            allUsers = GlobalConfig.DBConnection.GetUsers_All();
            allGroups = GlobalConfig.DBConnection.GetGroups_All();

            cBoxSelectUser.DataSource = null;
            cBoxSelectUser.DataSource = allUsers.Select(u=> u.FullName ).ToList();

            //dataGridViewAllUsers.DataSource = null;
            //dataGridViewAllUsers.DataSource = allUsers.Select(u => new { Name = u.FullName, UserGroup = u.Group }).ToList();

            //cBoxUserGroup.DataSource = null;
            //cBoxUserGroup.DataSource = allGroups;
            //cBoxUserGroup.DisplayMember = "GroupName";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            SelectedUser = allUsers.FirstOrDefault(u => u.FullName == cBoxSelectUser.Text);
            string pw = "";
            switch (SelectedUser.Group)
            {
                case "Admin":
                    pw = UserSettings.Default.PW_Admin;
                    break;
                case "Maint":
                    pw = UserSettings.Default.PW_Maint;
                    break;
                default:
                    pw = UserSettings.Default.PW_Op;
                    break;
            }
            if (tBoxPassword.Text==pw)
            {
                DialogResult = DialogResult.OK;
                LoginName = SelectedUser.FullName;
                LoginGroup = SelectedUser.Group;
                UserSettings.Default.CurrentUserGroup = SelectedUser.Group;
                Close();
            }
            else
            {
                MessageBox.Show("Log on failed! Incorrect User Password!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }


        }

        private void TBoxPassword_Click(object sender, EventArgs e)
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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //private bool ValidateForm()
        //{
        //    bool output = true;

        //    if (tBoxPassword.Text.Length == 0)
        //        output = false;

        //    return output;
        //}
    }
}
