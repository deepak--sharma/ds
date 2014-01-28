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

namespace MySales
{
    public partial class ViewModifyEmployees : Form
    {
        List<Employee> _empList = new List<Employee>();
        private string formMode = string.Empty;
        public ViewModifyEmployees()
        {
            InitializeComponent();
        }
        public ViewModifyEmployees(string mode)
        {
            InitializeComponent();
            formMode = mode;
        }

        private void ViewModifyEmployees_Load(object sender, EventArgs e)
        {
            SetupEmployeesList();
            if (!string.IsNullOrEmpty(formMode) && formMode == "detail")
            {
                ChangeFormMode();
            }
        }
        private void ChangeFormMode()
        {
            Text = "Enter Employee's Monthly Data Before Calculating Salary";
            btnCancel.Visible = true;
            btnSave.Visible = true;
        }

        private void SetupEmployeesList()
        {
            _empList = new EmployeeBL().GetAllEmployees();
            _empList = _empList.OrderBy(e => e.FirstName).ToList();
            dgvEmp.AutoGenerateColumns = false;

            if (dgvEmp.ColumnCount == 0)
            {
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "ID",
                    HeaderText = "ID",
                    Visible = false,
                    DataPropertyName = "ID"
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "Code",
                    HeaderText = "Code",
                    Visible = false,
                    DataPropertyName = "EmpCode"
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "Name",
                    HeaderText = "Name",
                    DataPropertyName = "FullName",
                    ReadOnly = true
                });
                dgvEmp.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    Name = "Designation",
                    HeaderText = "Designation",
                    DataPropertyName = "Designation.Desc",
                    ReadOnly = true
                });
                if (string.IsNullOrEmpty(formMode))
                {
                    dgvEmp.Columns.Add(new DataGridViewButtonColumn
                                           {
                                               Text = "Modify Details",
                                               Name = "Modify",
                                               UseColumnTextForButtonValue = true,
                                           });
                    dgvEmp.Columns.Add(new DataGridViewButtonColumn
                                           {
                                               Text = "Delete",
                                               Name = "Delete",
                                               UseColumnTextForButtonValue = true,
                                           });
                }

                if (!string.IsNullOrEmpty(formMode) && formMode == "detail")
                {
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                            {
                                HeaderText = "Days Present",
                                Name = "Present",
                                MaxInputLength = 2,
                                ReadOnly = false,
                            });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Days Absent",
                        Name = "Absent",
                        MaxInputLength = 2,
                        ReadOnly = false,
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "OT Hours",
                        Name = "OT",
                        ReadOnly = false,
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Total Advance Amount",
                        Name = "AdvanceAmt",
                        ReadOnly = false,
                    });
                    dgvEmp.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        HeaderText = "Advance Deuction",
                        Name = "AdvanceDeduct",
                        ReadOnly = false,
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
            if (dgvEmp.Rows[e.RowIndex].DataBoundItem != null &&
                dgvEmp.Columns[e.ColumnIndex].DataPropertyName.Contains("."))
            {
                e.Value = BindProperty(dgvEmp.Rows[e.RowIndex].DataBoundItem, dgvEmp.Columns[e.ColumnIndex].DataPropertyName);
            }

        }
        private string BindProperty(object property, string propertyName)
        {
            string retValue = "";

            if (propertyName.Contains("."))
            {
                string leftPropertyName;
                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                var arrayProperties = property.GetType().GetProperties();

                foreach (var propertyInfo in arrayProperties.Where(propertyInfo => propertyInfo.Name == leftPropertyName))
                {
                    retValue = BindProperty(
                        propertyInfo.GetValue(property, null),
                        propertyName.Substring(propertyName.IndexOf(".") + 1));
                    break;
                }
            }
            else
            {
                var propertyType = property.GetType();
                var propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }
            return retValue;
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
            switch (e.ColumnIndex)
            {
                case 4:
                    {
                        long empId = -1;
                        if (e.RowIndex < 0) break;
                        long.TryParse(Convert.ToString(dgvEmp.Rows[e.RowIndex].Cells[0].Value), out empId);
                        var empFrm = new FrmEmployee(empId);
                        empFrm.ShowDialog();
                    }
                    break;
            }
        }
    }
}
