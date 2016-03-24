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
using MySales.DL;
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
            var objEmployeeBl = new EmployeeBl();
            var lstEmp = objEmployeeBl.GetAllEmployees();
            //var lstPayroll = new PayrollDl().FetchPayrollData(Convert.ToInt32(cbMonth.SelectedIndex) + 1 , Convert.ToInt32(cbYear.SelectedItem.ToString()), Utility.PayrollStatus.CALCULATED, true);
            //filter out employess that have payroll already generated for the current month/yr
            //var payrollData = from a in lstEmp
            //                  join b in lstPayroll on a.Id equals b.EmpId
            //                  select new Employee()
            //                  {
            //                      PayrollDetails = b,
            //                      Id = a.Id,
            //                      EmpCode = a.EmpCode,
            //                      FirstName = a.FirstName,
            //                      MiddleName = a.MiddleName,
            //                      LastName = a.LastName,
            //                      FullName = a.FullName

            //                  };

            
            lstEmp = lstEmp.OrderBy(x => x.FirstName).ToList();
            foreach (var emp in lstEmp)
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

            foreach (var emp in from emp in lstEmp let status = new PayrollBl().PayrollCalculator(emp, Convert.ToInt32(cbMonth.SelectedIndex) + 1, Convert.ToInt32(cbYear.SelectedItem.ToString())) where status == Utils.Utility.ActionStatus.SUCCESS select emp)
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
