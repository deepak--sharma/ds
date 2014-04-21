using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySales.BL;
using MySales.Utils;

namespace MySales
{
    public partial class PayrollReport : Form
    {
        public PayrollReport()
        {
            InitializeComponent();
        }
        private void BindPayrollData()
        {
            var lst = new PayrollBL().FetchPayrollData(Convert.ToInt32(cbMonth.SelectedIndex) + 1,
                Convert.ToInt32(cbYear.SelectedItem.ToString()), Utility.PayrollStatus.CALCULATED, true);
            dataGridView1.AutoGenerateColumns = false;
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "PayrollID",
                    HeaderText = "ID",
                    Visible = false,
                    DataPropertyName = "ID"
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "EmpID",
                    HeaderText = "EmpID",
                    Visible = false,
                    DataPropertyName = "EmpID"
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "EmpName",
                    HeaderText = "Name",
                    Visible = true,
                    DataPropertyName = "FullName"
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "Salary",
                    HeaderText = "Salary",
                    Visible = true,
                    DataPropertyName = "PayrollDetails.NetPayable"
                });
            }

            dataGridView1.DataSource = lst;
        }

        private void PayrollReport_Load(object sender, EventArgs e)
        {
            Utility.SetPayrollMonthYearDropdownList(cbMonth, cbYear, true);

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Utility.dgvEmp_CellFormatting(dataGridView1, e);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            BindPayrollData();
        }
    }
}
