using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class ProductBL
    {
        public string CreateProduct(Product theProduct)
        {
            string status = "START";
            try
            {
                int code = (new ProductDL()).InsertProduct(theProduct, UserBL.userID);
                if (code < 1)
                {
                    status = "ERROR";
                }
                else
                {
                    status = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public List<Product> GetAllProducts()
        {
            ProductDL objProductDL = new ProductDL();
            return objProductDL.GetAllProducts();
        }
    }
}
