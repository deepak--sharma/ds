using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using MySales.BL;
using System.Security.Principal;
namespace MySales
{
    public partial class Login : Form
    {
        string connStr = string.Empty;
        public Login()
        {
            InitializeComponent();
            connStr = ConfigurationManager.ConnectionStrings["LocalAccessDB"].ConnectionString.Trim();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (new UserBl().IsUserValid(this.txtUsername.Text.Trim(), this.txtPassword.Text.Trim(), out UserBl.UserId))
            {
                this.Hide();
                new frmMain().Show();
            }
            else
            {
                MessageBox.Show("Please enter correct Username or Password.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsername.Text = WindowsIdentity.GetCurrent().Name;
            txtPassword.Focus();
        }

        private void lnkDefaultUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtUsername.Text = WindowsIdentity.GetCurrent().Name;
        }
    }
}
