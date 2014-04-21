using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class SalaryDetailBL
    {
        public SalaryDetail GetMonthlyGross(Int64 empID)
        {
            return new SalaryDetailDL().GetMonthlyGross(empID);
        }
        public int AddUpdateSalaryDetails(Employee emp)
        {
            return new SalaryDetailDL().AddUpdateSalaryDetails(emp);
        }
    }
}
