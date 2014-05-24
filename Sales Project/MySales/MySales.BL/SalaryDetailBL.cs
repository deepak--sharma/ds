using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class SalaryDetailBl
    {
        public SalaryDetail GetMonthlyGross(Int64 empId)
        {
            return new SalaryDetailDl().GetMonthlyGross(empId);
        }
        public int AddUpdateSalaryDetails(Employee emp)
        {
            return new SalaryDetailDl().AddUpdateSalaryDetails(emp);
        }
    }
}
