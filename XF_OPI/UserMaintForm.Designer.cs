namespace XF_OPI
{
    partial class UserMaintForm
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
            this.cBoxUserGroup = new System.Windows.Forms.ComboBox();
            this.lblAllUsers = new System.Windows.Forms.Label();
            this.btnDeleteSelectedUser = new System.Windows.Forms.Button();
            this.gBoxCreateUser = new System.Windows.Forms.GroupBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.tBoxLN = new System.Windows.Forms.TextBox();
            this.tBoxID = new System.Windows.Forms.TextBox();
            this.tBoxEmail = new System.Windows.Forms.TextBox();
            this.tBoxPhone = new System.Windows.Forms.TextBox();
            this.tBoxFN = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.dataGridViewAllUsers = new System.Windows.Forms.DataGridView();
            this.lblCautious = new System.Windows.Forms.Label();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gBoxCreateUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // cBoxUserGroup
            // 
            this.cBoxUserGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cBoxUserGroup.FormattingEnabled = true;
            this.cBoxUserGroup.Location = new System.Drawing.Point(177, 436);
            this.cBoxUserGroup.Name = "cBoxUserGroup";
            this.cBoxUserGroup.Size = new System.Drawing.Size(257, 37);
            this.cBoxUserGroup.TabIndex = 9;
            this.cBoxUserGroup.SelectedIndexChanged += new System.EventHandler(this.cBoxUserGroup_SelectedIndexChanged);
            // 
            // lblAllUsers
            // 
            this.lblAllUsers.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.lblAllUsers.AutoSize = true;
            this.lblAllUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllUsers.Location = new System.Drawing.Point(594, 30);
            this.lblAllUsers.Name = "lblAllUsers";
            this.lblAllUsers.Size = new System.Drawing.Size(128, 32);
            this.lblAllUsers.TabIndex = 12;
            this.lblAllUsers.Text = "All Users";
            // 
            // btnDeleteSelectedUser
            // 
            this.btnDeleteSelectedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSelectedUser.Location = new System.Drawing.Point(704, 555);
            this.btnDeleteSelectedUser.Name = "btnDeleteSelectedUser";
            this.btnDeleteSelectedUser.Size = new System.Drawing.Size(292, 74);
            this.btnDeleteSelectedUser.TabIndex = 13;
            this.btnDeleteSelectedUser.Text = "Delete Selected User";
            this.btnDeleteSelectedUser.UseVisualStyleBackColor = true;
            this.btnDeleteSelectedUser.Click += new System.EventHandler(this.btnDeleteSelectedUser_Click);
            // 
            // gBoxCreateUser
            // 
            this.gBoxCreateUser.Controls.Add(this.label3);
            this.gBoxCreateUser.Controls.Add(this.label2);
            this.gBoxCreateUser.Controls.Add(this.label1);
            this.gBoxCreateUser.Controls.Add(this.lblGroup);
            this.gBoxCreateUser.Controls.Add(this.btnCreateUser);
            this.gBoxCreateUser.Controls.Add(this.tBoxLN);
            this.gBoxCreateUser.Controls.Add(this.tBoxID);
            this.gBoxCreateUser.Controls.Add(this.tBoxEmail);
            this.gBoxCreateUser.Controls.Add(this.tBoxPhone);
            this.gBoxCreateUser.Controls.Add(this.tBoxFN);
            this.gBoxCreateUser.Controls.Add(this.lblId);
            this.gBoxCreateUser.Controls.Add(this.cBoxUserGroup);
            this.gBoxCreateUser.Controls.Add(this.lblPhone);
            this.gBoxCreateUser.Controls.Add(this.lblEmail);
            this.gBoxCreateUser.Controls.Add(this.lblLastName);
            this.gBoxCreateUser.Controls.Add(this.lblFirstName);
            this.gBoxCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gBoxCreateUser.Location = new System.Drawing.Point(33, 29);
            this.gBoxCreateUser.Name = "gBoxCreateUser";
            this.gBoxCreateUser.Size = new System.Drawing.Size(477, 609);
            this.gBoxCreateUser.TabIndex = 19;
            this.gBoxCreateUser.TabStop = false;
            this.gBoxCreateUser.Text = "Create New User";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup.Location = new System.Drawing.Point(0, 436);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(80, 29);
            this.lblGroup.TabIndex = 36;
            this.lblGroup.Text = "Group";
            this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateUser.Location = new System.Drawing.Point(91, 537);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(181, 52);
            this.btnCreateUser.TabIndex = 35;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // tBoxLN
            // 
            this.tBoxLN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxLN.Location = new System.Drawing.Point(177, 131);
            this.tBoxLN.Name = "tBoxLN";
            this.tBoxLN.Size = new System.Drawing.Size(257, 35);
            this.tBoxLN.TabIndex = 34;
            this.tBoxLN.Click += new System.EventHandler(this.TBoxKP_Click);
            // 
            // tBoxID
            // 
            this.tBoxID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxID.Location = new System.Drawing.Point(177, 208);
            this.tBoxID.Name = "tBoxID";
            this.tBoxID.Size = new System.Drawing.Size(257, 35);
            this.tBoxID.TabIndex = 33;
            this.tBoxID.Click += new System.EventHandler(this.TBoxKP_Click);
            // 
            // tBoxEmail
            // 
            this.tBoxEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxEmail.Location = new System.Drawing.Point(177, 285);
            this.tBoxEmail.Name = "tBoxEmail";
            this.tBoxEmail.Size = new System.Drawing.Size(257, 35);
            this.tBoxEmail.TabIndex = 32;
            this.tBoxEmail.Click += new System.EventHandler(this.TBoxKP_Click);
            // 
            // tBoxPhone
            // 
            this.tBoxPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxPhone.Location = new System.Drawing.Point(177, 361);
            this.tBoxPhone.Name = "tBoxPhone";
            this.tBoxPhone.Size = new System.Drawing.Size(257, 35);
            this.tBoxPhone.TabIndex = 31;
            this.tBoxPhone.Click += new System.EventHandler(this.TBoxKP_Click);
            // 
            // tBoxFN
            // 
            this.tBoxFN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tBoxFN.Location = new System.Drawing.Point(177, 55);
            this.tBoxFN.Name = "tBoxFN";
            this.tBoxFN.Size = new System.Drawing.Size(257, 35);
            this.tBoxFN.TabIndex = 30;
            this.tBoxFN.Click += new System.EventHandler(this.TBoxKP_Click);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblId.Location = new System.Drawing.Point(6, 211);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(36, 29);
            this.lblId.TabIndex = 29;
            this.lblId.Text = "ID";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(6, 352);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(83, 29);
            this.lblPhone.TabIndex = 28;
            this.lblPhone.Text = "Phone";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(6, 285);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(74, 29);
            this.lblEmail.TabIndex = 27;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(6, 134);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(128, 29);
            this.lblLastName.TabIndex = 26;
            this.lblLastName.Text = "Last Name";
            this.lblLastName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFirstName
            // 
            this.lblFirstName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(6, 55);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(131, 29);
            this.lblFirstName.TabIndex = 25;
            this.lblFirstName.Text = "First Name";
            this.lblFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridViewAllUsers
            // 
            this.dataGridViewAllUsers.AllowUserToAddRows = false;
            this.dataGridViewAllUsers.AllowUserToDeleteRows = false;
            this.dataGridViewAllUsers.AllowUserToOrderColumns = true;
            this.dataGridViewAllUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAllUsers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewAllUsers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewAllUsers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewAllUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllUsers.Location = new System.Drawing.Point(600, 65);
            this.dataGridViewAllUsers.Name = "dataGridViewAllUsers";
            this.dataGridViewAllUsers.ReadOnly = true;
            this.dataGridViewAllUsers.RowHeadersWidth = 62;
            this.dataGridViewAllUsers.RowTemplate.Height = 28;
            this.dataGridViewAllUsers.Size = new System.Drawing.Size(396, 477);
            this.dataGridViewAllUsers.TabIndex = 38;
            this.dataGridViewAllUsers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAllUsers_CellContentClick);
            // 
            // lblCautious
            // 
            this.lblCautious.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.lblCautious.AutoSize = true;
            this.lblCautious.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCautious.Location = new System.Drawing.Point(531, 665);
            this.lblCautious.Name = "lblCautious";
            this.lblCautious.Size = new System.Drawing.Size(582, 26);
            this.lblCautious.TabIndex = 39;
            this.lblCautious.Text = "Cautious! Selected User will be deleted permanently!!";
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.BackColor = System.Drawing.Color.SteelBlue;
            this.btnCloseForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseForm.ForeColor = System.Drawing.Color.White;
            this.btnCloseForm.Location = new System.Drawing.Point(964, 7);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(158, 52);
            this.btnCloseForm.TabIndex = 37;
            this.btnCloseForm.Text = "CLOSE";
            this.btnCloseForm.UseVisualStyleBackColor = false;
            this.btnCloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(132, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 29);
            this.label1.TabIndex = 37;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(132, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 29);
            this.label2.TabIndex = 38;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(132, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 29);
            this.label3.TabIndex = 39;
            this.label3.Text = "*";
            // 
            // UserMaintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 700);
            this.ControlBox = false;
            this.Controls.Add(this.btnCloseForm);
            this.Controls.Add(this.lblCautious);
            this.Controls.Add(this.dataGridViewAllUsers);
            this.Controls.Add(this.gBoxCreateUser);
            this.Controls.Add(this.btnDeleteSelectedUser);
            this.Controls.Add(this.lblAllUsers);
            this.Name = "UserMaintForm";
            this.Text = "Users Maintenance";
            this.Load += new System.EventHandler(this.UserMaintForm_Load);
            this.gBoxCreateUser.ResumeLayout(false);
            this.gBoxCreateUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cBoxUserGroup;
        private System.Windows.Forms.Label lblAllUsers;
        private System.Windows.Forms.Button btnDeleteSelectedUser;
        private System.Windows.Forms.GroupBox gBoxCreateUser;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.TextBox tBoxLN;
        private System.Windows.Forms.TextBox tBoxID;
        private System.Windows.Forms.TextBox tBoxEmail;
        private System.Windows.Forms.TextBox tBoxPhone;
        private System.Windows.Forms.TextBox tBoxFN;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.DataGridView dataGridViewAllUsers;
        private System.Windows.Forms.Label lblCautious;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}