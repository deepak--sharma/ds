using System;
using System.Windows.Forms;
using MySales.BL;
using MySales.BO;
using System.Linq;
using System.Collections.Generic;
namespace MySales
{
    public partial class EnterSalesData : Form
    {
        long clientID = 0, dealerID = 0;
        List<Client> lstClient = new List<Client>();
        List<Dealer> lstDealer = new List<Dealer>();
        public EnterSalesData()
        {
            InitializeComponent();
        }

        private void EnterSalesData_Load(object sender, EventArgs e)
        {
            ClearForm();
            AutoCompleteClient();
            AutoCompleteDealer();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            Client theClient = new Client();
            Dealer theDealer = new Dealer();
            Product theProduct = new Product();
            /* 1.CreateClient
             * 2.CreateDealer
             * 3.CreateProduct
             */
            if (CreateClient(theClient))
            {
                if (CreateDealer(theDealer))
                {
                    theProduct.Client = theClient;
                    theProduct.Dealer = theDealer;
                    if (CreateProduct(theProduct))
                    {
                        DialogResult dlgResult = MessageBox.Show("Data entered successfully. Do you want to enter more data?", "SUCCESS", MessageBoxButtons.YesNo);
                        switch (dlgResult)
                        {
                            case DialogResult.Yes:
                                ClearForm();
                                break;
                            case DialogResult.No:
                                this.Hide();
                                Application.OpenForms["Home"].Show();
                                break;
                        }
                    }
                }
            }
        }

        private bool CreateClient(Client theClient)
        {
            bool clientCreated = false;
            if (txtItem.Text.Trim() == "")
            {
                errorProvider1.SetError(txtItem, "Cannot be left blank");

                //MessageBox.Show("Please enter all the mandatory fields.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtClient.Text.Trim() == "")
            {
                errorProvider1.SetError(txtClient, "Cannot be left blank");
                return false;
            }
            if (txtDealer.Text.Trim() == "")
            {
                errorProvider1.SetError(txtDealer, "Cannot be left blank");
                return false;
            }
            float tmpVar;
            if (!float.TryParse(txtAmount.Text.Trim(), out tmpVar))
            {
                errorProvider1.SetError(txtAmount, "Enter valid Amount data");
                return false;
            }
            if (!float.TryParse(txtBalance.Text.Trim(), out tmpVar))
            {
                errorProvider1.SetError(txtBalance, "Enter valid Balance data");
                return false;
            }


            ClientBl objClientBL = new ClientBl();
            try
            {
                if (clientID == 0)
                {
                    theClient.Id = objClientBL.GetNextClientId();
                    theClient.Name = this.txtClient.Text.Trim();
                    theClient.Address = "NA";
                    theClient.Description = this.txtClient.Text.Trim();
                    theClient.CreateDate = DateTime.Now;
                    string strStatus = objClientBL.CreateClient(theClient);
                    clientCreated = strStatus == "SUCCESS";
                }
                else
                {
                    theClient.Id = clientID;
                    clientCreated = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return clientCreated;
        }

        private bool CreateDealer(Dealer theDealer)
        {
            bool dealerCreated = false;
            try
            {
                DealerBl objDealerBL = new DealerBl();
                if (dealerID == 0)
                {
                    theDealer.Id = objDealerBL.GetNextDealerId();
                    theDealer.Name = this.txtDealer.Text.Trim();
                    theDealer.Address = "NA";
                    theDealer.Description = this.txtDealer.Text.Trim();
                    theDealer.CreateDate = DateTime.Now;
                    string strStatus = objDealerBL.CreateDealer(theDealer);
                    dealerCreated = strStatus == "SUCCESS";
                }
                else
                {
                    theDealer.Id = dealerID;
                    dealerCreated = true;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dealerCreated;
        }

        private bool CreateProduct(Product theProduct)
        {
            bool productCreated = false;
            try
            {
                theProduct.Item = this.txtItem.Text.Trim();
                DateTime PurchaseDate = new DateTime();
                DateTime.TryParse(this.txtDate.Text.Trim(), out PurchaseDate);
                theProduct.PurchaseDate = PurchaseDate;
                int warranty = 0;
                if (ddlWarranty.SelectedItem != null)
                {
                    Int32.TryParse(this.ddlWarranty.SelectedItem.ToString(), out warranty);
                }
                theProduct.Warranty = warranty;
                float amount = 0, balance = 0;
                float.TryParse(this.txtAmount.Text.Trim(), out amount);
                theProduct.Amount = amount;
                float.TryParse(this.txtBalance.Text.Trim(), out balance);
                theProduct.Balance = balance;
                string strStatus = new ProductBl().CreateProduct(theProduct);
                productCreated = strStatus == "SUCCESS";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return productCreated;
        }

        private void AutoCompleteClient()
        {
            try
            {
                AutoCompleteStringCollection clientNames = new AutoCompleteStringCollection();
                string strStatus = new ClientBl().GetClientNames(ref lstClient);
                switch (strStatus)
                {
                    case "SUCCESS":
                        foreach (Client theClient in lstClient)
                        {
                            clientNames.Add(theClient.Name);
                        }
                        break;
                }

                txtClient.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtClient.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtClient.AutoCompleteCustomSource = clientNames;
            }
            catch (Exception ex)
            {
                //log error message
            }

        }

        private void AutoCompleteDealer()
        {
            try
            {
                AutoCompleteStringCollection dealerNames = new AutoCompleteStringCollection();
                string strStatus = new DealerBl().GetDealerNames(ref lstDealer);
                switch (strStatus)
                {
                    case "SUCCESS":
                        foreach (Dealer theDealer in lstDealer)
                        {
                            dealerNames.Add(theDealer.Name);
                        }
                        break;
                }

                txtDealer.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtDealer.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDealer.AutoCompleteCustomSource = dealerNames;
            }
            catch (Exception ex)
            {
                //log error message
            }

        }

        private void txtClient_Leave(object sender, EventArgs e)
        {
            AutoCompleteStringCollection list = txtClient.AutoCompleteCustomSource;
            if (list.Contains(txtClient.Text.Trim()))
            {
                var selectedClient = from c in lstClient
                                     where c.Name == txtClient.Text.Trim()
                                     select c;
                clientID = selectedClient.First().Id;
            }
        }

        private void txtDealer_Leave(object sender, EventArgs e)
        {
            AutoCompleteStringCollection list = txtDealer.AutoCompleteCustomSource;
            if (list.Contains(txtDealer.Text.Trim()))
            {
                var selectedDealer = from c in lstDealer
                                     where c.Name == txtDealer.Text.Trim()
                                     select c;
                dealerID = selectedDealer.First().Id;
            }
        }

        private void lnkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Home objHome = new Home();
            //this.Hide();
            //objHome.Show();
            this.Close();
        }

        private void ClearForm()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox)
                {
                    ctl.Text = "";
                }
                if (ctl is ComboBox)
                {
                    ComboBox cBox = (ComboBox)ctl;
                    cBox.SelectedIndex = 0;
                }
                if (ctl is DateTimePicker)
                {
                    DateTimePicker dp = (DateTimePicker)ctl;
                    ctl.ResetText();
                }
            }
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearForm();
        }

        private void EnterSalesData_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["Home"].Show();
        }


    }
}
