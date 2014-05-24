using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class ProductBl
    {
        public string CreateProduct(Product theProduct)
        {
            var status = "START";
            try
            {
                var code = (new ProductDl()).InsertProduct(theProduct, UserBl.UserId);
                status = code < 1 ? "ERROR" : "SUCCESS";
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }

        public List<Product> GetAllProducts()
        {
            var objProductDl = new ProductDl();
            return objProductDl.GetAllProducts();
        }
    }
}
