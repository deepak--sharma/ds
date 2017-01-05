using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySales.BO;
using MySales.BL;
using System.Security.Principal;
namespace MySales
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUN.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Username field cannot be left blank.");
                return;
            }
            if (txtUN.Text.Trim().Length > 100)
            {
                MessageBox.Show("Username length cannot be greater than 100.");
                return;
            }
            if (txtPwd.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Password field cannot be blank.");
                return;
            }
            if (txtCPWD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Confirm Password field cannot be blank.");
                return;
            }
            if (txtPwd.Text.Trim().Length < 5 || txtPwd.Text.Trim().Length > 20)
            {
                MessageBox.Show("Password length should be between (5-20)");
                return;
            }
            if (txtPwd.Text.Trim() != txtCPWD.Text.Trim())
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }
            User theUser = new User();
            theUser.Username = txtUN.Text.Trim();
            theUser.Password = UtilityClass.Encrypt(txtPwd.Text.Trim());
            string status = "";
            switch (UserBl.Mode)
            {
                case 1:
                    status = new UserBl().CreateUser(theUser);
                    break;
                case 2:
                    status = new UserBl().ChangePassword(theUser);
                    break;
            }
            
            if (status == "SUCCESS")
            {
                MessageBox.Show("User created successfully.");
                this.Hide();
                new Login().Show();
            }
            else
            {
                MessageBox.Show("Some error occured.");
            }

        }

        private void Register_Load(object sender, EventArgs e)
        {
            switch (UserBl.Mode)
            {
                case 1:
                    this.Text = "Register";
                    break;
                case 2:
                    this.Text = "Change Password";
                    break;
            }

            txtUN.Text = WindowsIdentity.GetCurrent().Name;
            txtUN.ReadOnly = true;
            txtPwd.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
