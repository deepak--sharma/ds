using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using System.Data;
using System.Data.OleDb;

namespace MySales.DL
{
    public class ProductDL
    {
        private const string ProductInsertQuery = "Insert into Product (ID,Item,Client_ID,Dealer_ID,[Purchase Date],Warranty,Amount,Balance,CreationDate,CreatedBy) values (@id,@item,@Client_ID,@Dealer_ID,@PurchaseDate,@Warranty,@Amount,@Balance,@CreationDate,@CreatedBy)";
        private const string SelectMaxProduct = "Select MAX(ID) FROM Product";
        //private const string SelectAllProducts = "Select p.ID as ProductID,p.Item,c.ID as ClientID,c.Name as Client,d.ID as DealerID,d.Name as Dealer,p.[Purchase Date],p.Warranty,p.Amount from Product p left outer join Client c on p.Client_ID=c.ID left outer join Dealer d on p.Dealer_ID=d.ID";
        private const string SelectAllProducts = @"SELECT Product.ID AS Product_ID, Product.Item, Product.[Purchase Date], Product.Warranty, Product.Amount, Client.ID AS Client_ID, Client.Name AS Client_Name, Dealer.ID AS Dealer_ID, Dealer.Name AS Dealer_Name
FROM Dealer INNER JOIN (Client INNER JOIN Product ON Client.ID = Product.Client_ID) ON Dealer.ID = Product.Dealer_ID;";
        public int InsertProduct(Product theProduct, long createdByID)
        {
            int statusCode = -1;
            try
            {
                theProduct.ID = GetNextProductID();
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(ProductInsertQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theProduct.ID));
                        cmd.Parameters.Add(new OleDbParameter("@item", theProduct.Item));
                        cmd.Parameters.Add(new OleDbParameter("@Client_ID", theProduct.Client.ID));
                        cmd.Parameters.Add(new OleDbParameter("@Dealer_ID", theProduct.Dealer.ID));
                        cmd.Parameters.Add(new OleDbParameter("@PurchaseDate", theProduct.PurchaseDate.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@Warranty", theProduct.Warranty));
                        cmd.Parameters.Add(new OleDbParameter("@Amount", theProduct.Amount));
                        cmd.Parameters.Add(new OleDbParameter("@Balance", theProduct.Balance));
                        cmd.Parameters.Add(new OleDbParameter("@CreationDate", DateTime.Now.ToString()));
                        cmd.Parameters.Add(new OleDbParameter("@CreatedBy", createdByID));
                        statusCode = cmd.ExecuteNonQuery();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return statusCode;

        }

        private long GetNextProductID()
        {
            long _productID = 1;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectMaxProduct, con))
                    {
                        Object statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_productID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out _productID);
                            _productID += 1;
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _productID;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> lstProducts = new List<Product>();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectAllProducts, con))
                    {
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Product theProduct = new Product
                                {
                                    ID = null != dr["Product_ID"] && string.Empty != dr["Product_ID"].ToString().Trim() ? long.Parse(dr["Product_ID"].ToString().Trim()) : 0,
                                    Client = new Client
                                    {
                                        ID = null != dr["Client_ID"] && string.Empty != dr["Client_ID"].ToString().Trim() ? long.Parse(dr["Client_ID"].ToString().Trim()) : 0,
                                        Name = null != dr["Client_Name"] ? dr["Client_Name"].ToString().Trim() : string.Empty
                                    },
                                    Dealer = new Dealer
                                    {
                                        ID = null != dr["Dealer_ID"] && string.Empty != dr["Dealer_ID"].ToString().Trim() ? long.Parse(dr["Dealer_ID"].ToString().Trim()) : 0,
                                        Name = null != dr["Dealer_Name"] ? dr["Dealer_Name"].ToString().Trim() : string.Empty
                                    },
                                    Item = null != dr["Item"] ? dr["Item"].ToString() : string.Empty,
                                    Warranty = null != dr["Warranty"] && string.Empty != dr["Warranty"].ToString().Trim() ? int.Parse(dr["Warranty"].ToString().Trim()) : 0,
                                    Amount = null != dr["Amount"] && string.Empty != dr["Amount"].ToString().Trim() ? float.Parse(dr["Amount"].ToString().Trim()) : 0,
                                    PurchaseDate = null != dr["Purchase Date"] && string.Empty != dr["Purchase Date"].ToString().Trim() ? DateTime.Parse(dr["Purchase Date"].ToString().Trim()) : DateTime.MinValue
                                };
                                lstProducts.Add(theProduct);
                            }
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstProducts;
        }
    }
}
