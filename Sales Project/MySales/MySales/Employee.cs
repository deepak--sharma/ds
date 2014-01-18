using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using MySales.BL;
using MySales.BO;
using MySales.Utils;

namespace MySales
{
    public partial class FrmEmployee : Form
    {
        private long _empID = -1;
        public FrmEmployee()
        {
            InitializeComponent();
        }
        public FrmEmployee(long empID)
        {
            InitializeComponent();
            _empID = empID;
        }
        StateBL objStateBL = new StateBL();
        CityBL objCityBL = new CityBL();

        private void FillDesignation()
        {
            List<Designation> lstDesignation = new DesignationBL().GetDesignations();
            if (null != lstDesignation)
            {
                lstDesignation.Insert(0, new Designation { Desc = "-select-", ID = -1 });
                ddlDesig.DataSource = lstDesignation;
                ddlDesig.DisplayMember = "Desc";
                ddlDesig.ValueMember = "ID";
            }
        }

        private void FillState()
        {
            var lstStatesC = objStateBL.GetStateList();

            var lstStatesP = new List<State>();
            if (null != lstStatesC)
            {
                lstStatesC.Insert(0, new State { Name = "-select-", ID = -1 });
                ddlStateC.DataSource = lstStatesC;
                ddlStateC.DisplayMember = "Name";
                ddlStateC.ValueMember = "ID";
            }
            lstStatesP.AddRange(lstStatesC);

            ddlStateP.DataSource = lstStatesP;
            ddlStateP.ValueMember = "ID";
            ddlStateP.DisplayMember = "Name";
            //ddlStateP.Items.Insert(0, new State { Name = "-select-", ID = -1 });

        }
        void FillCity(long stateId, string strType)
        {
            var lstCity = objCityBL.GetCityList();
            var lstFilteredCity = from city in lstCity
                                  where city.StateID == stateId
                                  select city;
            var lstCity1 = lstFilteredCity.ToList<City>();
            lstCity1.Insert(0, new City { Name = "-select-", ID = -1 });

            if (strType == "C")
            {
                ddlCityC.DataSource = lstCity1;
                ddlCityC.DisplayMember = "Name";
                ddlCityC.ValueMember = "ID";
            }
            else if (strType == "P")
            {
                ddlCityP.DataSource = lstCity1;
                ddlCityP.DisplayMember = "Name";
                ddlCityP.ValueMember = "ID";
            }

        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "";
            rbM.Checked = true;
            FillDesignation();
            FillState();
            if (_empID != -1)
                SetupForm();
        }

        private void SetupForm()
        {
            var emp = new EmployeeBL().GetSingleEmployee(_empID);
            if (emp == null) return;
            txtFN.Text = emp.FirstName;
            txtMN.Text = emp.MiddleName;
            txtLN.Text = emp.LastName;
            rbM.Checked = emp.Gender == MALE;
            rbF.Checked = emp.Gender == FEMALE;
            txtFathersName.Text = emp.FathersName;
            dateTimePicker1.Value = DateTime.Parse(emp.DateOfBirth);
            ddlDesig.Text = emp.Designation.Desc;
            txtAddC.Text = emp.AddressC.Line1;
            ddlStateC.SelectedValue = emp.AddressC.StateId;
            ddlCityC.SelectedValue = emp.AddressC.CityId;
            txtPincodeC.Text = emp.AddressC.Pincode.ToString();
            txtAddP.Text = emp.AddressP.Line1;
            ddlStateP.SelectedValue = emp.AddressP.StateId;
            ddlCityP.SelectedValue = emp.AddressP.CityId;
            txtPincodeP.Text = emp.AddressP.Pincode.ToString();
            txtMobileNo.Text = emp.MobileNo;
            txtOtherNo.Text = emp.OtherNo;
        }

        private void ddlStateC_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCity(Convert.ToInt64(ddlStateC.SelectedIndex), "C");
        }

        private void ddlStateP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCity(Convert.ToInt64(ddlStateP.SelectedIndex), "P");
        }

        private void chkCopyAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSame.Checked)
            {
                this.txtAddP.Text = this.txtAddC.Text;
                this.txtPincodeP.Text = this.txtPincodeC.Text;
                this.ddlStateP.SelectedIndex = this.ddlStateC.SelectedIndex;
                this.ddlCityP.SelectedIndex = this.ddlCityC.SelectedIndex;
            }
            else
            {
                this.txtAddP.Text = "";
                this.txtPincodeP.Text = "";
                this.ddlStateP.SelectedIndex = 0;
                this.ddlCityP.SelectedIndex = 0;
            }


        }
        const string MALE = "Male";
        const string FEMALE = "Female";

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                this.toolStripStatusLabel1.Text = "Please correct errors on all the pages of this form and try again.";
                return;
            }
            string strGender = string.Empty;
            if (this.rbM.Checked)
            {
                strGender = MALE;
            }
            else if (this.rbF.Checked)
            {
                strGender = FEMALE;
            }
            var emp = new Employee
            {
                EmpCode = GetEmployeeCode(),
                FirstName = this.txtFN.Text.Trim(),
                MiddleName = this.txtMN.Text.Trim(),
                LastName = this.txtLN.Text.Trim(),
                FathersName = txtFathersName.Text.Trim(),
                Gender = strGender,
                DateOfBirth = this.dateTimePicker1.Value.ToString(),
                MobileNo = txtMobileNo.Text.Trim(),
                OtherNo = txtOtherNo.Text.Trim(),
                DateOfJoining = DateTime.Now.ToString(),
                IsActive = true,
                Designation = new Designation { ID = Convert.ToInt64(this.ddlDesig.SelectedValue) },
                AddressC = new Address()
                               {
                                   Line1 = txtAddC.Text.Trim(),
                                   CityId = (long)ddlCityC.SelectedValue,
                                   StateId = (long)ddlStateC.SelectedValue,
                                   Pincode = Convert.ToInt64(string.IsNullOrEmpty(txtPincodeC.Text.Trim()) ? "0" : txtPincodeC.Text.Trim())
                               },
                AddressP = new Address()
                {
                    Line1 = txtAddP.Text.Trim(),
                    CityId = (long)ddlCityP.SelectedValue,
                    StateId = (long)ddlStateP.SelectedValue,
                    Pincode = Convert.ToInt64(string.IsNullOrEmpty(txtPincodeP.Text.Trim()) ? "0" : txtPincodeP.Text.Trim())
                }
               ,
                SalDetails = new SalaryDetail()
                                 {
                                     CreateDate = DateTime.Today,
                                     MonthlyGross = !string.IsNullOrEmpty(txtMonthlyGross.Text.Trim()) ?
                                     Convert.ToDecimal(txtMonthlyGross.Text.Trim()) : 0
                                 }
            };
            MessageBox.Show(new EmployeeBL().AddEmployee(emp, 1) == Utility.ActionStatus.SUCCESS
                                ? "Data saved successfully."
                                : "Error: Please contact product support.");
        }

        private void btnClearForm_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            this.toolStripStatusLabel1.Text = "";
            this.dateTimePicker1.Value = DateTime.Today;
            ResetForm();
        }

        private string GetEmployeeCode()
        {
            return "AN/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString() + "/" + new Random().Next(1000).ToString();
        }

        private bool IsFormValid()
        {
            bool _isFormValid = true;
            errorProvider1.Clear();
            if (txtFN.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtFN, "Please enter first name.");
                _isFormValid = false;
            }
            if (txtFathersName.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtFathersName, "Please enter father's name.");
                _isFormValid = false;
            }
            if (Convert.ToInt32(ddlDesig.SelectedValue) == -1)
            {
                errorProvider1.SetError(ddlDesig, "Please select designation.");
                _isFormValid = false;
            }
            if (!IsAgeValid(this.dateTimePicker1.Value))
            {
                errorProvider1.SetError(dateTimePicker1, "Age cannot be less than 15 years.");
                _isFormValid = false;
            }
            if (!IsAddressValid())
            {
                _isFormValid = false;
            }

            return _isFormValid;
        }
        private bool IsAgeValid(DateTime dtDob)
        {
            bool isAgeValid = !(DateTime.Today.Year - dtDob.Year < 15);
            return isAgeValid;
        }

        private bool IsAddressValid()
        {
            bool isAddressValid = true;
            if (this.txtAddC.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtAddC, "Please provide complete address line 1.");
                isAddressValid = false;
            }
            if (this.ddlStateC.SelectedIndex == 0)
            {
                errorProvider1.SetError(ddlStateC, "Please select state.");
                isAddressValid = false;
            }
            if (this.txtAddP.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtAddP, "Please provide complete address line 1.");
                isAddressValid = false;
            }
            if (this.ddlStateP.SelectedIndex == 0)
            {
                errorProvider1.SetError(ddlStateP, "Please select state.");
                isAddressValid = false;
            }
            if (this.txtMobileNo.Text.Trim() == string.Empty && this.txtOtherNo.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(this.gbContact, "Please enter either mobile no or other no.");
                isAddressValid = false;
            }

            return isAddressValid;
        }

        private void txtMobileNo_Leave(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtMobileNo.Text.Trim() == string.Empty)
                return;
            Int64 no = 0;
            if (!Int64.TryParse(txtMobileNo.Text.Trim(), out no))
            {
                errorProvider1.SetError(txtMobileNo, "Please enter a vild mobile no.");
            }
        }

        private void ResetForm()
        {
            foreach (var ctl2 in from ctl in this.Controls.OfType<TabControl>() from Control ctl1 in ctl.Controls from Control ctl2 in ctl1.Controls select ctl2)
            {
                var textBox = ctl2 as TextBox;
                if (textBox != null)
                {
                    textBox.Text = "";
                }
                if (!(ctl2 is GroupBox)) continue;
                foreach (Control ctl3 in ctl2.Controls)
                {
                    var box = ctl3 as TextBox;
                    if (box != null)
                    {
                        box.Text = "";
                    }
                    var comboBox = ctl3 as ComboBox;
                    if (comboBox != null)
                    {
                        comboBox.SelectedIndex = 0;
                    }
                    var checkBox = ctl3 as CheckBox;
                    if (checkBox != null)
                    {
                        checkBox.Checked = false;
                    }
                    if (!(ctl3 is GroupBox)) continue;
                    foreach (Control ctl4 in ctl3.Controls)
                    {
                        var ctl5 = ctl4 as TextBox;
                        if (ctl5 != null)
                        {
                            ctl5.Text = "";
                        }
                        var comboBox1 = ctl4 as ComboBox;
                        if (comboBox1 != null)
                        {
                            comboBox1.SelectedIndex = 0;
                        }
                        var checkBox1 = ctl4 as CheckBox;
                        if (checkBox1 != null)
                        {
                            checkBox1.Checked = false;
                        }

                    }
                }
            }
        }
    }
}
