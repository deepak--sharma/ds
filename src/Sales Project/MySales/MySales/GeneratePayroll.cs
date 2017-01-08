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
using System.IO;
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
        private List<Employee> GetDataList(bool filter)
        {
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
            return lstEmp;
        }
        private void BindGrid(bool filter)
        {
            if (cbMonth.SelectedItem == null || cbYear.SelectedItem == null) { return; }
            lvPayroll.Items.Clear();
            var lstEmp = GetDataList(filter);
            if (lstEmp.Count == 0)
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
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.SalDetails.MonthlyGross.ToString() });
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.Attendance.TotalDays.ToString() });
                lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = emp.Attendance.LeaveDays.ToString() });
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
            VerifyInput();
            btnGenPayroll.Enabled = false;
            lblCounter.Text = "Started calculating salary....";
            var dataToProcessExists = false;
            foreach (ListViewItem item in lvPayroll.Items)
            {
                if (item.SubItems[8].Text == Utility.PayrollStatus.TOBECALCULATED.ToString())
                {
                    dataToProcessExists = true;
                    item.ImageIndex = 2;
                    Application.DoEvents();
                    var empBeingProcessed = new Employee
                    {
                        Id = Convert.ToInt64(item.ToolTipText)
                    };
                    var status = new PayrollBl().PayrollCalculator(
                        empBeingProcessed,
                        Convert.ToInt32(cbMonth.SelectedIndex) + 1,
                        Convert.ToInt32(cbYear.SelectedItem.ToString())
                        );
                    if (status == Utility.ActionStatus.SUCCESS)
                    {
                        item.SubItems[8].Text = Utility.PayrollStatus.CALCULATED.ToString();
                        item.SubItems[7].Text = empBeingProcessed.PayrollDetails.NetPayable.ToString();
                        item.Tag = empBeingProcessed.PayrollDetails.Id.ToString();
                        item.ImageIndex = 1;
                        item.Font = new Font(item.Font, FontStyle.Bold);
                        Application.DoEvents();
                    }
                    lblCounter.Text = "Finished calculating salary....";
                }
            }
            if (!dataToProcessExists)
            {
                MessageBox.Show("Monthly payroll for all employess has been processed, there is no new data for calculation");
                btnGenPayroll.Enabled = true;
                return;
            }
            btnGenPayroll.Enabled = true;
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            BindGrid(true);
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(false);
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(false);
        }
        private void VerifyInput()
        {
            lblCounter.Text = "Verifying employee data before calculating salary....";
            foreach (ListViewItem item in lvPayroll.Items)
            {
                var sal = decimal.Zero;
                var days = 0;
                var absent = 0;
                if (
                    (!decimal.TryParse(item.SubItems[2].Text, out sal) || sal == decimal.Zero) ||
                     ((!Int32.TryParse(item.SubItems[3].Text, out days) || days == 0) &&
                       (!Int32.TryParse(item.SubItems[4].Text, out absent) || absent == 0))
                    )
                {
                    item.SubItems[8].Text = Utility.PayrollStatus.REJECTED.ToString();
                    item.ImageIndex = 3;
                    item.Font = new Font(item.Font, FontStyle.Bold);
                    Application.DoEvents();
                }
                lblCounter.Text = "Finished verifying employee data before calculating salary....";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvPayroll.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var dialogResult = MessageBox.Show("Are you sure you want to delete the selected data?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                var status = new PayrollBl().DeletePayroll(Convert.ToInt64(lvPayroll.SelectedItems[0].ToolTipText),
                                                           Convert.ToInt32(cbMonth.SelectedIndex) + 1,
                                                           Convert.ToInt32(cbYear.SelectedItem.ToString())
                                                          );
                BindGrid(false);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var lstPayroll = GetDataList(false);

            var htmlTableRows = Utility.List2HtmlTable(lstPayroll,
                        x => x.EmployeeFullName(),
                        x => x.SalDetails.MonthlyGross,
                        x => x.Attendance.TotalDays,
                        x => x.Attendance.LeaveDays,
                        x => x.AdvanceDetails.TotalAdvance,
                        x => x.Attendance.Overtime,
                        x => x.PayrollDetails.NetPayable);
            var style = @"<style>
                        table {
                            border-collapse: collapse;
                        }

                        table, th, td {
                            border: 1px solid black;
	                        padding: 5px;
                        }
                        </style>";
            var html = string.Format("{0}<table><th>Name</th><th>Salary</th><th>Days</th><th>Absent</th><th>Advance</th><th>OverTime(hrs)</th><th>NetPayable</th>{1}</table>", style, htmlTableRows);
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Directory.GetParent(dir).Root.ToString();
            var fileName = path + @"report.html";            
            File.WriteAllText(fileName, html);
            var frmPrint = new PrintPreview { Url = @"file://" + fileName };
            frmPrint.ShowDialog();
        }
    }
}
