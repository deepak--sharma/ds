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
        public Utility.ActionStatus AddUpdateEmployee(Employee employee, Int64 userId)
        {
            /* 
             * 1. Add Employee Current and Permanent Addresses.
             * 2. Add Employee.
             * 3. Add Employee Salary Details (if any).
             * 4. Add Employee Advance Details (if any).             
             */
            var addressBl = new AddressBL();
            var result = Utility.ActionStatus.SUCCESS;
            var cStatus = addressBl.AddUpdateAddress(employee.AddressC);
            var pStatus = addressBl.AddUpdateAddress(employee.AddressP);
            if (cStatus == Utility.ActionStatus.FAILURE || pStatus == Utility.ActionStatus.FAILURE)
            {
                //Call DeleteAddress for both types
                addressBl.DeleteAddress(employee.AddressC.Id);
                addressBl.DeleteAddress(employee.AddressP.Id);
                return Utility.ActionStatus.FAILURE;
            }
            result = _empDl.AddUpdateEmployee(employee, userId);
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
        public List<Employee> GetEmpAttendance(int month, int year)
        {
            /*
             * 1. Get all the employees 
             * 2. First of all check whether attendance records exists if yes go to 4 if no go to 3.
             * 3. insert blank records in attendance table for current payroll month and year.
             * 4. Get blank inserted attendance details
             * 5. Join the above lists and return single Joined list.             * 
             */
            var empList = _empDl.GetAllEmployees();
            var empIds = new StringBuilder();
            var ctr = 0;
            foreach (var employee in empList)
            {
                ctr++;
                empIds.Append(employee.Id.ToString());
                if (ctr < empList.Count)
                    empIds.Append(",");
            }
            var attList = new EmpAttendanceDL().GetAllEmpAttendance(empIds.ToString(), month, year);
            if (attList.Count == 0)
            {
                //Insert blank attendance records for each emp
                foreach (var employee in empList)
                {

                }
            }
            var empAttList = from a in empList
                             join b in attList on a.Id equals b.EmpID
                             select new Employee()
                                        {
                                            Attendance = b,
                                            Id = a.Id,
                                            EmpCode = a.EmpCode,
                                            FirstName = a.FirstName,
                                            MiddleName = a.MiddleName,
                                            LastName = a.LastName,
                                            FullName = a.FullName
                                        };
            return empAttList.ToList();
        }
        public List<Employee> GetEmpAdvance()
        {
            return _empDl.GetAllEmployees();
        }
    }
}
