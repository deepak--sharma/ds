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
            BindGrid(false);
        }

        private void BindGrid(bool filter)
        {
            lvPayroll.Items.Clear();
            var objEmployeeBl = new PayrollBl();
            var lstAllEmp = objEmployeeBl.GetPayrollGridData(Convert.ToInt32(cbMonth.SelectedIndex) + 1, Convert.ToInt32(cbYear.SelectedItem.ToString()));
            List<Employee> lstEmp;
            if (filter)
            {
                var filterExpression = txtFilter.Text.Trim();
                var lst = from a in lstAllEmp
                          where a.FirstName.StartsWith(filterExpression, true, null)
                          select a;
                lstEmp = lst as List<Employee> ?? lst.ToList();
            }
            else
            {
                lstEmp = lstAllEmp;
            }
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
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.PayrollDetails.NetPayable.ToString() });
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.PayrollDetails.Status.ToString() });
                switch (emp.PayrollDetails.Status)
                {
                    case Utility.PayrollStatus.CALCULATED:
                        lvItem.ImageIndex = 1;
                        lvItem.Font = new Font(lvItem.Font, FontStyle.Bold);
                        break;
                    default:
                        break;
                }
                lvPayroll.Items.Add(lvItem);
            }
        }

        private void btnGenPayroll_Click(object sender, EventArgs e)
        {
            btnGenPayroll.Enabled = false;
            var dataToProcessExists = false;
            foreach (ListViewItem item in lvPayroll.Items)
            {
                if (item.SubItems[5].Text == Utility.PayrollStatus.TOBECALCULATED.ToString())
                {
                    dataToProcessExists = true;
                    item.ImageIndex = 2;
                    Application.DoEvents();
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
                        Application.DoEvents();

                    }
                }                
            }
            if (!dataToProcessExists)
            {
                MessageBox.Show("Monthly payroll for all employess has been processed, there is no new data for calculation");
                return;
            }
            btnGenPayroll.Enabled = true;
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

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            BindGrid(true);
        }
    }
}
