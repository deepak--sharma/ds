using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            new ViewModifyEmployees("detail").ShowDialog();
        }
    }
}
