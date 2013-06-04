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
            //if (IsUserValid())            
            if (new UserBL().IsUserValid(this.txtUsername.Text.Trim(), this.txtPassword.Text.Trim(), out UserBL.userID))
            {
                this.Hide();
                Home theHome = new Home();
                theHome.Show();
            }
            else
            {
                MessageBox.Show("Please enter correct Username or Password.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUsername.Text = WindowsIdentity.GetCurrent().Name;
            txtUsername.ReadOnly = true;
            txtPassword.Focus();
        }

        //private bool IsUserValid()
        //{
        //    bool _isUserValid = false;

        //    using (OleDbConnection con = new OleDbConnection(connStr))
        //    {
        //        string cmdStr = "select * from [User Account] where Username=@un";
        //        con.Open();
        //        using (OleDbCommand cmd = new OleDbCommand())
        //        {
        //            cmd.CommandText = cmdStr;
        //            cmd.Connection = con;
        //            cmd.Parameters.Add(new OleDbParameter("@un", this.txtUsername.Text.Trim()));
        //            //cmd.Parameters["@un"].Value = ;
        //            //cmd.Parameters.Add(new OleDbParameter("@pwd", this.txtPassword.Text.Trim()));
        //            //cmd.Parameters.Add("@pwd");
        //            //cmd.Parameters["@pwd"].Value = this.txtPassword.Text.Trim();
        //            using (OleDbDataAdapter adap = new OleDbDataAdapter(cmd))
        //            {
        //                DataSet ds = new DataSet();
        //                adap.Fill(ds);
        //                _isUserValid = ds.Tables[0].Rows[0]["Password"].ToString().Equals(this.txtPassword.Text);
        //            }
        //        }
        //    }
        //    return _isUserValid;
        //}
    }
}
