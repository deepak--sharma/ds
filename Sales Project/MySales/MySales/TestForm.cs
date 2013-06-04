using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace MySales
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList arrlist = new ArrayList();
            foreach (var item in listBox1.Items)
            {
                arrlist.Add(item);
            }

            foreach (var item in arrlist)
            {
                listBox1.Items.Remove(item);
                label1.Text = "";
                label1.Text = "Processing payroll for :" + label1.Text + " " + item.ToString();
                label1.Refresh();
                Application.DoEvents();
                Thread.Sleep(1000);
                Application.DoEvents();
                listBox2.Items.Add(item);
            }
        }
    }
}
