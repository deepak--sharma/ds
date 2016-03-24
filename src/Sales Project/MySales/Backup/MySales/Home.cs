using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySales.BL;
namespace MySales
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            EnterSalesData objEnterSalesData = new EnterSalesData();
            objEnterSalesData.Show();
        }

        private void lnkViewSalesData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmSearch objfrmSearch = new frmSearch();
            objfrmSearch.Show();
        }

        private void lnkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.OK) Application.Exit();
        }

        private void lnkResetPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UserBL.MODE = 2;
            this.Hide();
            Register regScreen = new Register();
            regScreen.Show();
        }
    }
}
