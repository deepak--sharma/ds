using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class Product
    {
        public Product()
        { 
            
        }
        public long ID { get; set; }
        public string Item { get; set; }
        public Client Client { get; set; }
        public Dealer Dealer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Warranty { get; set; }
        public float Amount { get; set; }
        public float Balance { get; set; }
    }
}
