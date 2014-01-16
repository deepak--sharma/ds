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
        public ViewModifyEmployees()
        {
            InitializeComponent();
        }

        private void ViewModifyEmployees_Load(object sender, EventArgs e)
        {
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
                        long.TryParse(Convert.ToString(dgvEmp.Rows[e.RowIndex].Cells[0].Value), out empId);
                        var empFrm = new FrmEmployee(empId);
                        empFrm.ShowDialog();
                    }
                    break;
            }
        }
    }
}
