using MySales.BL;
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
    public partial class AdvanceHistory : Form
    {
        public AdvanceHistory()
        {
            InitializeComponent();
        }
        public Int64 EmpId { get; set; }
        private void AdvanceHistory_Load(object sender, EventArgs e)
        {
            var advHistoryBl = new AdvanceDetailsBl();
            var advHistory = advHistoryBl.GetEmployeeAdvHistory(EmpId);
            var ctr = 0;
            foreach (var history in advHistory)
            {
                ctr++;
                var lstItem = new ListViewItem
                {
                    Text = ctr.ToString()
                };
                lstItem.SubItems.Add(new ListViewItem.ListViewSubItem
                {
                    Text = history.TotalAdvance.ToString()
                });
                lstItem.SubItems.Add(new ListViewItem.ListViewSubItem
                {
                    Text = history.AdvanceDeduction.ToString()
                });
                lstItem.SubItems.Add(new ListViewItem.ListViewSubItem
                {
                    Text = history.CreateDate.ToString()
                });
                lstItem.SubItems.Add(new ListViewItem.ListViewSubItem
                {
                    Text = history.IsActive ? "Yes" : "No"
                });
                lstAdvHistory.Items.Add(lstItem);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
