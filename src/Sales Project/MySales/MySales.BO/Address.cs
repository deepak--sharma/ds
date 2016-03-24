using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class Address
    {
        public long Id { get; set; }
        public String Line1 { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long Pincode { get; set; }
    }
}
