using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;
using MySales.Utils;

namespace MySales.BL
{
    public class EmployeeBL
    {
        private readonly EmployeeDl _empDl = new EmployeeDl();
        public List<Employee> GetAllEmployees()
        {
            return _empDl.GetAllEmployees();
        }
        public List<Employee> GetAdvanceEmplDetails(int payrollMonth, int payrollYear)
        {
            return _empDl.GetEmpAdvanceDetails(payrollMonth, payrollYear);
        }
        private Utility.ActionStatus InsertAdvanceEmplDetails(int payrollMonth, int payrollYear)
        {
            var result = Utility.ActionStatus.SUCCESS;
            return result;
        }
        public Utility.ActionStatus AddEmployee(Employee employee, Int64 userId)
        {
            /* 
             * 1. Add Employee Current and Permanent Addresses.
             * 2. Add Employee.
             * 3. Add Employee Salary Details (if any).
             * 4. Add Employee Advance Details (if any).             
             */
            var addressBl = new AddressBL();
            var result = Utility.ActionStatus.SUCCESS;
            var cStatus = addressBl.AddAddress(employee.AddressC);
            var pStatus = addressBl.AddAddress(employee.AddressP);
            if (cStatus == Utility.ActionStatus.FAILURE || pStatus == Utility.ActionStatus.FAILURE)
            {
                //Call DeleteAddress for both types
                addressBl.DeleteAddress(employee.AddressC.Id);
                addressBl.DeleteAddress(employee.AddressP.Id);
                return Utility.ActionStatus.FAILURE;
            }
            result = _empDl.AddEmployee(employee, userId);
            if (result == Utility.ActionStatus.FAILURE)
            {
                //Call DeleteAddress for both types
                addressBl.DeleteAddress(employee.AddressC.Id);
                addressBl.DeleteAddress(employee.AddressP.Id);
                return Utility.ActionStatus.FAILURE;
            }
            return result;
        }
        public Employee GetSingleEmployee(long empId)
        {
            return _empDl.GetSingleEmployee(empId);
        }
    }
}
