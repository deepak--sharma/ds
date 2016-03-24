using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class SalaryDetail
    {
        public long Id
        { get; set; }
        public long EmpId
        { get; set; }
        public decimal MonthlyGross
        { get; set; }
        public DateTime CreateDate
        { get; set; }
        public DateTime ModifiedDate
        { get; set; }
    }
}
