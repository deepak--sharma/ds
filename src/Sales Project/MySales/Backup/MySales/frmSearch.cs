using System;
using System.Configuration;
using System.Windows.Forms;
using MySales.DL;
using System.Data;
using MySales.BL;
using MySales.BO;
using System.Collections.Generic;
using System.Reflection;
namespace MySales
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        private void BindGrid()
        {
            dgSalesData.AutoGenerateColumns = false;
            if (dgSalesData.ColumnCount == 0)
            {
                dgSalesData.Columns.Add("ProductID", "ProductID");
                dgSalesData.Columns.Add("Item", "Item");
                dgSalesData.Columns.Add("Client", "Client");
                dgSalesData.Columns.Add("Dealer", "Dealer");
                dgSalesData.Columns.Add("Amount", "Amount");
                dgSalesData.Columns.Add("PurchaseDate", "PurchaseDate");
                dgSalesData.Columns.Add(new DataGridViewButtonColumn { Text = "Delete This", UseColumnTextForButtonValue = true });
            }

            dgSalesData.Columns["ProductID"].DataPropertyName = "ID";
            dgSalesData.Columns["ProductID"].Visible = false;
            dgSalesData.Columns["Item"].DataPropertyName = "Item";
            dgSalesData.Columns["Client"].DataPropertyName = "Client.Name";
            dgSalesData.Columns["Dealer"].DataPropertyName = "Dealer.Name";
            dgSalesData.Columns["Amount"].DataPropertyName = "Amount";
            dgSalesData.Columns["PurchaseDate"].DataPropertyName = "PurchaseDate";


            ProductBL objProductBL = new ProductBL();
            List<Product> lstProducts = new List<Product>();
            lstProducts = objProductBL.GetAllProducts();
            if (lstProducts.Count > 0)
            {
                dgSalesData.DataSource = lstProducts;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgSalesData.DataSource = null;
            BindGrid();
        }

        private void FetchData()
        {
            DBManager objDBManager = new DBManager();
        }

        private void dgSalesData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgSalesData.Rows[e.RowIndex].DataBoundItem != null &&
                dgSalesData.Columns[e.ColumnIndex].DataPropertyName.Contains("."))
            {
                e.Value = BindProperty(dgSalesData.Rows[e.RowIndex].DataBoundItem, dgSalesData.Columns[e.ColumnIndex].DataPropertyName);
            }

        }
        private string BindProperty(object property, string propertyName)
        {
            string retValue = "";

            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;
                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                        retValue = BindProperty(
                          propertyInfo.GetValue(property, null),
                          propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }
            return retValue;
        }

        private void lnkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Home objHome = new Home();
            this.Hide();
            objHome.Show();
        }

        private void dgSalesData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)  //Button Column
            {
                dgSalesData.Rows[e.RowIndex].Selected = true;                
                MessageBox.Show("Are you sure you want delete the selected Item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

    }
}

