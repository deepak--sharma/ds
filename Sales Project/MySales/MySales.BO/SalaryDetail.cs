using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class SalaryDetail
    {
        public long ID
        { get; set; }
        public long EmpID
        { get; set; }
        public decimal MonthlyGross
        { get; set; }
        public DateTime CreateDate
        { get; set; }
        public DateTime ModifiedDate
        { get; set; }
    }
}
