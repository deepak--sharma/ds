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
        public List<Employee> GetAllEmployees()
        {
            var objEmployeeDl = new EmployeeDl();
            return objEmployeeDl.GetAllEmployees();
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
            result = new EmployeeDl().AddEmployee(employee, userId);
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
            return new EmployeeDl().GetSingleEmployee(empId);
        }
    }
}
