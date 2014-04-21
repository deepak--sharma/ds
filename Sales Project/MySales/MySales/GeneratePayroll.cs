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
using MySales.Utils;

namespace MySales
{
    public partial class frmGeneratePayroll : Form
    {
        public frmGeneratePayroll()
        {
            InitializeComponent();
        }

        readonly BindingList<Employee> _lstEmployees = new BindingList<Employee>();

        private void FrmGeneratePayrollLoad(object sender, EventArgs e)
        {
            Utility.SetPayrollMonthYearDropdownList(cbMonth, cbYear);
            BindGrid();
        }

        private void BindGrid()
        {
            var objEmployeeBl = new EmployeeBL();
            var lst = objEmployeeBl.GetAllEmployees();
            lst = lst.OrderBy(x => x.FirstName).ToList();
            foreach (var emp in lst)
            {
                _lstEmployees.Add(emp);
            }
            if (_lstEmployees.Count <= 0) return;
            lbSource.DataSource = _lstEmployees;
            lbSource.DisplayMember = "FirstName";
            lbSource.ValueMember = "ID";
        }

        private void btnGenPayroll_Click(object sender, EventArgs e)
        {
            var lstEmp = lbSource.Items.Cast<Employee>().ToList();

            foreach (var emp in from emp in lstEmp let status = new PayrollBL().PayrollCalculator(emp, Convert.ToInt32(cbMonth.SelectedIndex) + 1, Convert.ToInt32(cbYear.SelectedItem.ToString())) where status == Utils.Utility.ActionStatus.SUCCESS select emp)
            {
                _lstEmployees.Remove(emp);
                Application.DoEvents();
                lblProcessing.Text = "Processing payroll for " + emp.FirstName;
                lblProcessing.Refresh();
                lbTarget.Items.Add(emp);
                lbTarget.DisplayMember = "FirstName";
                lbTarget.ValueMember = "ID";
                Application.DoEvents();
            }
            if (lbSource.Items.Count == 0)
                lblProcessing.Text = "Process finished.";
        }
    }
}
