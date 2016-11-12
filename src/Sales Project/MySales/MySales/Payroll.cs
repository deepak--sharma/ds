using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySales.Utils;

namespace MySales
{
    public partial class frmPayroll : Form
    {
        public frmPayroll()
        {
            InitializeComponent();
        }

        private void frmPayroll_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["frmMain"].Show();
        }



        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmEmployee().ShowDialog();
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmGeneratePayroll().ShowDialog();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ViewModifyEmployees().ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new ViewModifyEmployees("att").ShowDialog();
        }

        private void payrollDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PayrollReport().ShowDialog();
        }

        private void startPayrollGenMenuItem_Click(object sender, EventArgs e)
        {
            var nav = new Navigator("string");
            nav.Launch();
        }
    }
}
