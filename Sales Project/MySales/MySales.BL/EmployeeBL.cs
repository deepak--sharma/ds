using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class EmployeeBL
    {
        public List<Employee> GetAllEmployees()
        {
            var objEmployeeDl = new EmployeeDl();
            return objEmployeeDl.GetAllEmployees();
        }
        public int AddEmployee(Employee employee, Int64 userId)
        {
            return new EmployeeDl().AddEmployee(employee, userId);
        }
        public Employee GetSingleEmployee(long empId)
        {
            return new EmployeeDl().GetSingleEmployee(empId);
        }
    }
}
