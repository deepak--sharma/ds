using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySales.BL;
using MySales.BO;

namespace MySales
{
    public partial class PayrollCalculator : Form
    {
        List<Employee> _empList = new List<Employee>();
        public PayrollCalculator()
        {
            InitializeComponent();
        }

        private void PayrollCalculator_Load(object sender, EventArgs e)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var yearCount = 0;
            if (month == 1)
            {
                yearCount = year - 1;
            }
            else
            {
                yearCount = year;
            }
            for (var i = yearCount; i <= year; ++i)
            {
                cbYear.Items.Add(i);
            }
            if (month == 1)
            {
                cbMonth.SelectedIndex = 11;
            }
            else
            {
                cbMonth.SelectedIndex = month - 2;
            }
            cbYear.SelectedIndex = 0;
            SetupEmployeesList();
        }

        private void SetupEmployeesList()
        {
            //var empList = new EmployeeBL().GetAllEmployees();
            _empList = new EmployeeBL().GetAllEmployees();
            _empList = _empList.OrderBy(e => e.FirstName).ToList();
            dgvEmp.AutoGenerateColumns = false;

            if (dgvEmp.ColumnCount == 0)
            {
                dgvEmp.Columns.Add("ID", "ID");
                dgvEmp.Columns.Add("Code", "Code");
                dgvEmp.Columns.Add("Name", "Name");
                dgvEmp.Columns.Add("Designation", "Designation");
                dgvEmp.Columns.Add(new DataGridViewButtonColumn { Text = "Modify", UseColumnTextForButtonValue = true });
                dgvEmp.Columns.Add(new DataGridViewButtonColumn { Text = "Delete", UseColumnTextForButtonValue = true });

            }
            dgvEmp.Columns["ID"].DataPropertyName = "ID";
            dgvEmp.Columns["ID"].Visible = false;
            dgvEmp.Columns["Code"].DataPropertyName = "EmpCode";
            dgvEmp.Columns["Code"].Visible = false;
            dgvEmp.Columns["Name"].DataPropertyName = "FullName";
            dgvEmp.Columns["Designation"].DataPropertyName = "Designation.Desc";

            dgvEmp.DataSource = _empList;
            /*if (_empList != null)
            {
                lblRcdCount.Text = _empList.Count().ToString() + " record(s) found";
            }
            else
            {
                lblRcdCount.Text = "0 record(s) found";
            }*/

        }

        private void btnGenPayroll_Click(object sender, EventArgs e)
        {
            /*
             * 1. Check for Eligible ones for calculating payroll
             * 2. Show result for Ineligible employees.
             * 3. 
             */
        }
    }
}
