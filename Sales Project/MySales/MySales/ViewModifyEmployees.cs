using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MySales.BL;
using MySales.BO;
using MySales.Utils;

namespace MySales
{
    public partial class ViewModifyEmployees : Form
    {
        List<Employee> _empList = new List<Employee>();
        private readonly string _formMode = string.Empty;
        private const string EmpId = "ID";
        private const string Code = "Code";
        private const string EmpName = "Name";
        private const string Designation = "Designation";
        private const string Modify = "Modify";
        private const string Delete = "Delete";
        private const string Present = "Present";
        private const string Absent = "Absent";
        private const string OtHrs = "OT";
        private const string AdvanceAmt = "AdvanceAmt";
        private const string AdvanceDeduct = "AdvanceDeduct";
        private const string AdvanceBal = "AdvanceBal";
        private const string AttId = "AttId";
        public ViewModifyEmployees()
        {
            InitializeComponent();
        }
        public ViewModifyEmployees(string mode)
        {
            InitializeComponent();
            _formMode = mode;
        }

        private int year, month;
        private void ViewModifyEmployees_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_formMode) && _formMode == "att")
            {
                ChangeFormMode();

            }
            SetupEmployeesGrid();
        }
        private void ChangeFormMode()
        {
            Text = "Enter Employee's Monthly Data Before Calculating Salary";
            btnCancel.Visible = true;
            btnSave.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            if (month == 1)
            {
                month = 12;
            }
            else
            {
                month = month - 1;
            }
            label3.Text = Utility.MonthNameFromInt(month) + " / " + year;
        }

        private void SetupEmployeesList()
        {
            var empBl = new EmployeeBl();
            _empList = string.IsNullOrEmpty(_formMode)
                           ? empBl.GetAllEmployees()
                           : empBl.GetAdvanceEmplDetails(month, year);
            _empList = _empList.OrderBy(e => e.FirstName).ToList();
            dgvEmp.AutoGenerateColumns = false;

            if (dgvEmp.ColumnCount == 0)
            {
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = EmpId,
                    HeaderText = "ID",
                    Visible = false,
                    DataPropertyName = "ID"
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = Code,
                    HeaderText = "Code",
                    Visible = false,
                    DataPropertyName = "EmpCode"
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = EmpName,
                    HeaderText = "Name",
                    DataPropertyName = "FullName",
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                if (string.IsNullOrEmpty(_formMode))
                {
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        Name = Designation,
                        HeaderText = "Designation",
                        DataPropertyName = "Designation.Desc",
                        ReadOnly = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    });
                    dgvEmp.Columns.Add(new DataGridViewButtonColumn
                                           {
                                               Text = "Modify Details",
                                               Name = Modify,
                                               UseColumnTextForButtonValue = true,
                                           });
                    dgvEmp.Columns.Add(new DataGridViewButtonColumn
                                           {
                                               Text = "Delete",
                                               Name = Delete,
                                               UseColumnTextForButtonValue = true,
                                           });
                }

                if (!string.IsNullOrEmpty(_formMode) && _formMode == "att")
                {

                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                            {
                                HeaderText = "Days Present",
                                Name = Present,
                                MaxInputLength = 2,
                                ReadOnly = false,
                                DataPropertyName = "Attendance.WorkDays"
                            });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Days Absent",
                        Name = Absent,
                        MaxInputLength = 2,
                        ReadOnly = false,
                        DataPropertyName = "Attendance.LeaveDays"
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "OT Hours",
                        Name = OtHrs,
                        ReadOnly = false,
                        DataPropertyName = "Attendance.Overtime"
                    });
                    /*dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Advance Amount",
                        Name = AdvanceAmt,
                        ReadOnly = false,
                        DataPropertyName = "AdvanceDetails.TotalAdvance"
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Advance Deuction",
                        Name = AdvanceDeduct,
                        ReadOnly = false,
                        DataPropertyName = "AdvanceDetails.AdvanceDeduction"
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Advance Balance",
                        Name = AdvanceBal,
                        ReadOnly = true,
                        DataPropertyName = "AdvanceDetails.Balance"
                    });*/
                }
            }
            dgvEmp.DataSource = _empList;
            if (_empList != null)
            {
                lblRcdCount.Text = _empList.Count().ToString() + " record(s) found";
            }
            else
            {
                lblRcdCount.Text = "0 record(s) found";
            }

        }

        private void SetupEmployeesGrid()
        {
            var empBl = new EmployeeBl();
            /* _empList = string.IsNullOrEmpty(_formMode)
                            ? empBl.GetAllEmployees()
                            : empBl.GetAdvanceEmplDetails(month, year);*/
            switch (_formMode)
            {
                case "":
                    _empList = empBl.GetAllEmployees();
                    break;
                case "att":
                    //get employees + atendance details
                    _empList = empBl.GetEmpAttendance(month, year);
                    break;
                case "adv":
                    //get employees + advance details
                    break;
            }
            if (_empList.Count == 0)
            {
                MessageBox.Show("Something went wrong! Please try again later or contact application support");
                return;
            }
            _empList = _empList.OrderBy(e => e.FirstName).ToList();
            dgvEmp.AutoGenerateColumns = false;

            if (dgvEmp.ColumnCount == 0)
            {
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = EmpId,
                    HeaderText = "ID",
                    Visible = false,
                    DataPropertyName = "ID"
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = Code,
                    HeaderText = "Code",
                    Visible = false,
                    DataPropertyName = "EmpCode"
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = EmpName,
                    HeaderText = "Name",
                    DataPropertyName = "FullName",
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });

                if (string.IsNullOrEmpty(_formMode))
                {
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                    {
                        Name = Designation,
                        HeaderText = "Designation",
                        DataPropertyName = "Designation.Desc",
                        ReadOnly = true,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    });
                    dgvEmp.Columns.Add(new DataGridViewButtonColumn
                    {
                        Text = "Modify Details",
                        Name = Modify,
                        UseColumnTextForButtonValue = true,
                    });
                    dgvEmp.Columns.Add(new DataGridViewButtonColumn
                    {
                        Text = "Delete",
                        Name = Delete,
                        UseColumnTextForButtonValue = true,
                    });
                }
                else if (!string.IsNullOrEmpty(_formMode) && _formMode == "att")
                {
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = AttId,
                        HeaderText = AttId,
                        Visible = false,
                        ReadOnly = false,
                        DataPropertyName = "Attendance.ID"
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                                           {
                                               HeaderText = "Days Present",
                                               Name = Present,
                                               MaxInputLength = 2,
                                               ReadOnly = false,
                                               DataPropertyName = "Attendance.WorkDays"
                                           });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                                           {
                                               HeaderText = "Days Absent",
                                               Name = Absent,
                                               MaxInputLength = 2,
                                               ReadOnly = false,
                                               DataPropertyName = "Attendance.LeaveDays"
                                           });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                                           {
                                               HeaderText = "OT Hours",
                                               Name = OtHrs,
                                               ReadOnly = false,
                                               DataPropertyName = "Attendance.Overtime"
                                           });
                }
                else if (!string.IsNullOrEmpty(_formMode) && _formMode == "adv")
                {
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Advance Amount",
                        Name = AdvanceAmt,
                        ReadOnly = false,
                        DataPropertyName = "AdvanceDetails.TotalAdvance"
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Advance Deuction",
                        Name = AdvanceDeduct,
                        ReadOnly = false,
                        DataPropertyName = "AdvanceDetails.AdvanceDeduction"
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Advance Balance",
                        Name = AdvanceBal,
                        ReadOnly = true,
                        DataPropertyName = "AdvanceDetails.Balance"
                    });
                }
            }
            dgvEmp.DataSource = _empList;
            if (_empList != null)
            {
                lblRcdCount.Text = _empList.Count().ToString() + " record(s) found";
            }
            else
            {
                lblRcdCount.Text = "0 record(s) found";
            }

        }

        private void dgvEmp_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Utility.dgvEmp_CellFormatting(dgvEmp, e);

        }


        private void txtSearchFN_KeyUp(object sender, KeyEventArgs e)
        {
            var txt = ((TextBox)sender).Text;
            var lst = from a in _empList
                      where a.FirstName.StartsWith(txt, true, null)
                      select a;
            var enumerable = lst as List<Employee> ?? lst.ToList();
            dgvEmp.DataSource = enumerable.ToList();
            lblRcdCount.Text = enumerable.Count().ToString() + " record(s) found";
        }

        private void dgvEmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(_formMode) && _formMode == "att")
            {
                return;
            }
            switch (e.ColumnIndex)
            {
                case 4:
                    {
                        long empId = -1;
                        if (e.RowIndex < 0) break;
                        long.TryParse(Convert.ToString(dgvEmp.Rows[e.RowIndex].Cells[0].Value), out empId);
                        var empFrm = new FrmEmployee(empId);
                        empFrm.ShowDialog();
                        //Refresh main form
                        ViewModifyEmployees_Load(sender, e);
                        break;
                    }
                case 5:
                    {
                        var result = MessageBox.Show("Are you sure you want to delete this employee?", caption: "Please confirm", buttons: MessageBoxButtons.YesNo);
                        switch (result)
                        {
                            case DialogResult.Yes:

                                break;
                            case DialogResult.No:
                                return;
                        }
                        break;
                    }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*Steps
             * 1. Save Attendance details.
             * 2.
             * 3. If anything goes wrong thaen Rollback both.
             */
            if (string.IsNullOrEmpty(_formMode)) return;
            switch (_formMode)
            {
                case "att":
                    //Update Attendance details here
                    UpdateAttendance();
                    break;
                case "adv":
                    //Update Advance details here
                    break;
            }
        }

        private void UpdateAttendance()
        {
            var successCtr = 0;
            foreach (DataGridViewRow dr in dgvEmp.Rows)
            {
                /*var advanceDetails = new AdvanceDetail()
                                         {
                                             PMonth = month,
                                             PYear = year,
                                             TotalAdvance = Convert.ToDecimal(dr.Cells[AdvanceAmt].Value),
                                             Balance = Convert.ToDecimal(dr.Cells[AdvanceAmt].Value),
                                             AdvanceDeduction = Convert.ToDecimal(dr.Cells[AdvanceDeduct].Value),
                                             EmpID = Convert.ToInt64(dr.Cells[EmpId].Value),
                                             CreateDate = DateTime.Now
                                         };
                var advanceDetailsAdded = new AdvanceDetailsBL().AddAdvanceDetails(advanceDetails) ==
                                          Utility.ActionStatus.SUCCESS;*/

                var attendanceDetail = new EmpAttendance()
                {
                    Id = Convert.ToInt64(dr.Cells[AttId].Value ?? dr.Cells[AttId].EditedFormattedValue),
                    EmpId = Convert.ToInt64(dr.Cells[EmpId].Value ?? dr.Cells[EmpId].EditedFormattedValue),
                    WorkDays = Convert.ToInt64(dr.Cells[Present].Value ?? dr.Cells[Present].EditedFormattedValue),
                    LeaveDays = Convert.ToInt64(dr.Cells[Absent].Value ?? dr.Cells[Absent].EditedFormattedValue),
                    Overtime = Convert.ToDecimal(dr.Cells[OtHrs].Value ?? dr.Cells[OtHrs].EditedFormattedValue),
                    Month = month,
                    Year = year,
                    TotalDays = DateTime.DaysInMonth(year, month),
                    ModifiedDate = DateTime.Now
                };
                var attendanceDetailsAdded = new EmpAttendanceBl().UpdateAttendanceDetails(attendanceDetail) ==
                                             Utility.ActionStatus.SUCCESS;
                if (attendanceDetailsAdded)
                {
                    successCtr += 1;
                }

            }
            if (successCtr == dgvEmp.Rows.Count)
            {
                // MessageBox.Show("Some technical error occured please contact sys admin or try again later.");
                MessageBox.Show("Attendance details updated successfully.");
            }

        }

        private void dgvEmp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(_formMode) && _formMode != "att")
            {
                return;
            }
            var newVal = dgvEmp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value ?? string.Empty;
            long tmpVal = 0;
            if (!Int64.TryParse(newVal.ToString(), out tmpVal))
            {
                MessageBox.Show("Please enter a valid number");
                dgvEmp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tmpVal;
            }
            if (tmpVal<0)
            {
                MessageBox.Show("Only positive numbers are allowed");
                dgvEmp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }
        }

    }
}
