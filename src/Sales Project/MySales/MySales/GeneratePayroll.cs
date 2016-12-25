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
            var objEmployeeBl = new PayrollBl();
            var lstEmp = objEmployeeBl.GetPayrollGridData(Convert.ToInt32(cbMonth.SelectedIndex) + 1, Convert.ToInt32(cbYear.SelectedItem.ToString()));
            if(lstEmp.Count == 0)
            {
                lblCounter.Text = string.Format("No data found for selected Month/Year");
                return;
            }
            lblCounter.Text = string.Format("{0} records found", lstEmp.Count.ToString());
            lstEmp = lstEmp.OrderBy(x => x.FirstName).ToList();
            foreach (var emp in lstEmp)
            {
                var lvItem = new ListViewItem
                {
                    ToolTipText = emp.Id.ToString(),
                    ImageIndex = 0
                };
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.EmployeeFullName() });
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.AdvanceDetails.TotalAdvance.ToString() });
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.Attendance.Overtime.ToString() });
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.PayrollDetails.Status.ToString() });
                lvPayroll.Items.Add(lvItem);
            }
        }

        private void btnGenPayroll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvPayroll.Items)
            {
                var status = new PayrollBl().PayrollCalculator(
                    new Employee
                    {
                        Id = Convert.ToInt64(item.ToolTipText)
                    },
                    Convert.ToInt32(cbMonth.SelectedIndex) + 1,
                    Convert.ToInt32(cbYear.SelectedItem.ToString())
                    );
                if (status == Utility.ActionStatus.SUCCESS)
                {
                    item.ImageIndex = 1;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                }
            }
            //BindGrid();
            
            //var lstEmp = lbSource.Items.Cast<Employee>().ToList();
            //foreach (var emp in from emp in lstEmp let status = new PayrollBl().PayrollCalculator(emp, Convert.ToInt32(cbMonth.SelectedIndex) + 1, Convert.ToInt32(cbYear.SelectedItem.ToString())) where status == Utils.Utility.ActionStatus.SUCCESS select emp)
            //{
            //    _lstEmployees.Remove(emp);
            //    Application.DoEvents();
            //    lblProcessing.Text = "Processing payroll for " + emp.FirstName;
            //    lblProcessing.Refresh();
            //    lbTarget.Items.Add(emp);
            //    lbTarget.DisplayMember = "FirstName";
            //    lbTarget.ValueMember = "ID";
            //    Application.DoEvents();
            //}
            //if (lbSource.Items.Count == 0)
            //    lblProcessing.Text = "Process finished.";
        }

        
    }
}
