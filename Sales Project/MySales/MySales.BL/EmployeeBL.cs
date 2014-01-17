﻿using System;
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
            /* 1. Add Employee.
             * 2. Add Employee Current and Permanent Addresses.
             * 3. Add Employee Salary Details (if any).
             * 4. Add Employee Advance Details (if any).             
             */
            return new EmployeeDl().AddEmployee(employee, userId);
        }
        public Employee GetSingleEmployee(long empId)
        {
            return new EmployeeDl().GetSingleEmployee(empId);
        }
    }
}
