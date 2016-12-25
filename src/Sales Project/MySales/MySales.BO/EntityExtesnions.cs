using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public static class EntityExtesnions
    {
        public static string EmployeeFullName(this Employee emp)
        {
            if(!String.IsNullOrEmpty(emp.MiddleName.ToString().Trim()))
            {
                return emp.FirstName.Trim() + " " + emp.MiddleName.Trim() + " " + emp.LastName.Trim();
            }
            return emp.FirstName.Trim() + " " + emp.LastName.Trim();
        }
    }
}
