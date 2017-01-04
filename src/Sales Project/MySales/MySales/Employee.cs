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
        StateBl objStateBL = new StateBl();
        CityBl objCityBL = new CityBl();
        private long _currentAddressId;
        private long _permanentAddressId;
        private long _salaryId;

        private void FillDesignation()
        {
            List<Designation> lstDesignation = new DesignationBl().GetDesignations();
            if (null != lstDesignation)
            {
                lstDesignation.Insert(0, new Designation { Desc = "-select-", Id = -1 });
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
                lstStatesC.Insert(0, new State { Name = "-select-", Id = -1 });
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
                                  where city.StateId == stateId
                                  select city;
            var lstCity1 = lstFilteredCity.ToList<City>();
            lstCity1.Insert(0, new City { Name = "-select-", Id = -1 });

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
            ShowHideAdvGrid("Hide");
            if (_empID != -1)
            {
                btnClearForm.Visible = false;
                SetupForm();
            }
        }
        private Employee emp;
        private void SetupForm()
        {
            emp = new EmployeeBl().GetSingleEmployee(_empID);
            if (emp == null) return;
            txtFN.Text = emp.FirstName;
            txtMN.Text = emp.MiddleName;
            txtLN.Text = emp.LastName;
            rbM.Checked = emp.Gender == MALE;
            rbF.Checked = emp.Gender == FEMALE;
            txtFathersName.Text = emp.FathersName;
            dateTimePicker1.Value = emp.DateOfBirth ?? DateTime.Now;
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
            _currentAddressId = emp.AddressC.Id;
            _permanentAddressId = emp.AddressP.Id;
            _salaryId = emp.SalDetails.Id;
            txtMonthlyGross.Text = emp.SalDetails.MonthlyGross.ToString();
            if (emp.AdvanceDetails.TotalAdvance > 0)
            {
                ShowHideAdvGrid("Show");
                emp.AdvanceDetails.AdvAction = emp.AdvanceDetails.Id > 0 ? "U" : "I";
                lblTotalAdvValue.Text = emp.AdvanceDetails.TotalAdvance.ToString().Trim();
                lblDeductionValue.Text = emp.AdvanceDetails.AdvanceDeduction.ToString().Trim();
                lblBalanceValue.Text = emp.AdvanceDetails.Balance.ToString().Trim();
                emp.AdvanceHistory = new AdvanceDetailsBl().GetEmployeeAdvHistory(_empID, true);
                foreach (var item in emp.AdvanceHistory)
                {
                    var lvItem = new ListViewItem { Text = item.TotalAdvance.ToString(), ToolTipText = item.Id.ToString() };
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = item.AdvanceDeduction.ToString() });
                    lvItem.SubItems.Add(new ListViewItem.ListViewSubItem { Text = item.Balance.ToString() });
                    lvAdvance.Items.Add(lvItem);
                }
            }
        }

        private void ddlStateC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStateC.Items.Count == 0) { return; }
            var item = (State)ddlStateC.Items[ddlStateC.SelectedIndex];
            FillCity(Convert.ToInt64(item.Id), "C");
        }

        private void ddlStateP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStateP.Items.Count == 0) { return; }
            var item = (State)ddlStateP.Items[ddlStateP.SelectedIndex];
            FillCity(Convert.ToInt64(item.Id), "P");
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
                Id = _empID,
                EmpCode = _empID <= 0 ? GetEmployeeCode() : string.Empty,
                FirstName = this.txtFN.Text.Trim(),
                MiddleName = this.txtMN.Text.Trim(),
                LastName = this.txtLN.Text.Trim(),
                FathersName = txtFathersName.Text.Trim(),
                Gender = strGender,
                DateOfBirth = this.dateTimePicker1.Value,
                MobileNo = txtMobileNo.Text.Trim(),
                OtherNo = txtOtherNo.Text.Trim(),
                DateOfJoining = DateTime.Now,
                IsActive = true,
                Designation = new Designation { Id = Convert.ToInt64(this.ddlDesig.SelectedValue) },
                AddressC = new Address()
                               {
                                   Id = _currentAddressId,
                                   Line1 = txtAddC.Text.Trim(),
                                   CityId = (long)ddlCityC.SelectedValue,
                                   StateId = (long)ddlStateC.SelectedValue,
                                   Pincode = Convert.ToInt64(string.IsNullOrEmpty(txtPincodeC.Text.Trim()) ? "0" : txtPincodeC.Text.Trim())
                               },
                AddressP = new Address()
                {
                    Id = _permanentAddressId,
                    Line1 = txtAddP.Text.Trim(),
                    CityId = (long)ddlCityP.SelectedValue,
                    StateId = (long)ddlStateP.SelectedValue,
                    Pincode = Convert.ToInt64(string.IsNullOrEmpty(txtPincodeP.Text.Trim()) ? "0" : txtPincodeP.Text.Trim())
                }
               ,
                SalDetails = new SalaryDetail()
                                 {
                                     Id = _salaryId,
                                     CreateDate = DateTime.Now,
                                     MonthlyGross = !string.IsNullOrEmpty(txtMonthlyGross.Text.Trim()) ?
                                     Convert.ToDecimal(txtMonthlyGross.Text.Trim()) : 0
                                 },
                AdvanceDetails = new AdvanceDetail
                {
                    AdvanceDeduction = string.IsNullOrEmpty(lblDeductionValue.Text) ? decimal.MinValue :  Convert.ToDecimal(lblDeductionValue.Text),
                    TotalAdvance = string.IsNullOrEmpty(lblTotalAdvValue.Text) ? decimal.MinValue : Convert.ToDecimal(lblTotalAdvValue.Text),
                    Balance = string.IsNullOrEmpty(lblBalanceValue.Text) ? decimal.MinValue : Convert.ToDecimal(lblBalanceValue.Text),
                    IsActive = true                    
                }                
            };
            if (this.emp != null)
            {
                emp.AdvanceHistory = this.emp.AdvanceHistory;
                emp.AdvanceDetails.Id = this.emp.AdvanceDetails.Id;
            }
            
            MessageBox.Show(new EmployeeBl().AddUpdateEmployee(emp, 1) == Utility.ActionStatus.SUCCESS
                                ? "Data saved successfully."
                                : "Error: Please contact product support.");
            this.Close();
        }
        private void FillAdvanceDetails(Employee e)
        {
            foreach (ListViewItem item in lvAdvance.Items)
            {
                var adv = new AdvanceDetail {
                    EmpId = e.Id,
                    AdvanceDeduction = Convert.ToDecimal(item.SubItems[2].Text),
                    TotalAdvance = Convert.ToDecimal(item.SubItems[1].Text),
                    Balance = Convert.ToDecimal(item.SubItems[3].Text),
                };
                e.AdvanceDetails = adv;
            }
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

     

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gbAddAdv.Visible = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvAdvance.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.");
                return;
            }
            var dialogResult = MessageBox.Show("You are about to delete an Active advance record. Are you sure you want to continue as this action can't be undone?", "Confirm action", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            foreach (ListViewItem item in lvAdvance.SelectedItems)
            {
                var advItem = emp.AdvanceHistory.FirstOrDefault(x => x.Id == Convert.ToInt64(item.ToolTipText));
                advItem.IsActive = false;
                lvAdvance.Items.Remove(item);
                UpdateAdvanceTotals();
            }

            if (lvAdvance.Items.Count == 0)
            {
                ShowHideAdvGrid("Hide");
            }

        }

        private void txtAdvAmt_Leave(object sender, EventArgs e)
        {
            txtBalAmt.Text = txtAdvAmt.Text;
        }
        private bool ValidateAdvanceDetails()
        {
            var allGood = true;
            var advAmt = txtAdvAmt.Text.Trim();
            var dedAmt = txtDeduction.Text.Trim();
            decimal adv, ded;
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(advAmt))
            {
                errorProvider1.SetError(txtAdvAmt, "Value cannot be blank.");
                allGood = false;
            }
            if (string.IsNullOrEmpty(advAmt))
            {
                errorProvider1.SetError(txtDeduction, "Value cannot be blank.");
                allGood = false;
            }

            if(!decimal.TryParse(advAmt,out adv))
            {
                errorProvider1.SetError(txtAdvAmt, "Invalid amount value");
                allGood = false;
            }
            if (!decimal.TryParse(dedAmt, out ded))
            {
                errorProvider1.SetError(txtDeduction, "Invalid amount value");
                allGood = false;
            }
            if (adv <= 0)
            {
                errorProvider1.SetError(txtAdvAmt, "Amount must be greater than zero");
                allGood = false;
            }
            if (ded <= 0)
            {
                errorProvider1.SetError(txtDeduction, "Amount must be greater than zero");
                allGood = false;
            }
            if (ded > adv)
            {
                errorProvider1.SetError(txtDeduction, "Deduction amount cannot be greater than Advance amount");
                allGood = false;
            }
            return allGood;

        }

        private void btnSubmitAdv_Click(object sender, EventArgs e)
        {
            if (!ValidateAdvanceDetails())
            {
                return;
            }
            //var itemCount = lvAdvance.Items.Count;
            var lvItem = new ListViewItem { Text = txtAdvAmt.Text.Trim() };         
            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem
            {
                Text = txtDeduction.Text.Trim()
            });
            lvItem.SubItems.Add(new ListViewItem.ListViewSubItem
            {
                Text = txtBalAmt.Text.Trim()
            });
            lvAdvance.Items.Add(lvItem);
            if (emp != null)
            {
                emp.AdvanceHistory.Add(new AdvanceDetail
                {
                    AdvanceDeduction = Convert.ToDecimal(txtDeduction.Text.Trim()),
                    Balance = Convert.ToDecimal(txtBalAmt.Text.Trim()),
                    EmpId = emp.Id,
                    TotalAdvance = Convert.ToDecimal(txtAdvAmt.Text.Trim()),
                    CreateDate = DateTime.Now,
                    IsActive = true
                });
            }
            txtAdvAmt.Text = txtDeduction.Text = txtBalAmt.Text = "0.0";
            gbAddAdv.Visible = false;
            ShowHideAdvGrid("Show");
            UpdateAdvanceTotals();
        }

        private void btnCancelAdv_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            gbAddAdv.Visible = false;
        }
        private void ShowHideAdvGrid(string action)
        {
            switch (action)
            {
                case "Show":
                    lvAdvance.Visible = true;
                    btnRemove.Visible = true;
                    lnkAdvHistory.Visible = true;
                    gbTotal.Visible = true;
                    lblNoAdvData.Visible = false;
                    break;
                case "Hide":
                    lvAdvance.Visible = false;
                    btnRemove.Visible = false;
                    lnkAdvHistory.Visible = false;
                    gbTotal.Visible = false;
                    lblNoAdvData.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void lnkAdvHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var historyForm = new AdvanceHistory
            {
                EmpId = _empID
            };
            historyForm.ShowDialog();
        }
        private void UpdateAdvanceTotals()
        {
            lblTotalAdvValue.Text = string.Empty;
            lblDeductionValue.Text = string.Empty;
            lblBalanceValue.Text = string.Empty;
            decimal adv=0, ded=0, bal=0;
            foreach (ListViewItem item in lvAdvance.Items)
            {
                adv += Convert.ToDecimal(item.Text); 
                bal += Convert.ToDecimal(item.SubItems[0].Text);
                ded += Convert.ToDecimal(item.SubItems[1].Text);
            }
            lblTotalAdvValue.Text = adv.ToString();
            lblDeductionValue.Text = ded.ToString();
            lblBalanceValue.Text = bal.ToString();
        }
    }
}
