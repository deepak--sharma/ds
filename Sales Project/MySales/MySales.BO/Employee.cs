using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class Employee
    {

        #region Constructor
        public Employee()
        {
            Attendance = new EmpAttendance();
            AdvanceDetails = new AdvanceDetail();
            SalDetails = new SalaryDetail();
            PayrollDetails = new Payroll();
            Designation = new Designation();
        }
        #endregion

        #region Private members

        #endregion

        #region Public properties

        public EmpAttendance Attendance { get; set; }

        public AdvanceDetail AdvanceDetails { get; set; }

        public SalaryDetail SalDetails { get; set; }

        public Payroll PayrollDetails { get; set; }

        public long Id
        { get; set; }
        public String EmpCode
        { get; set; }
        public string FirstName
        { get; set; }
        public string MiddleName
        { get; set; }
        public string LastName
        { get; set; }
        public string FathersName { get; set; }
        public string Gender
        { get; set; }
        public string DateOfBirth
        { get; set; }
        public string AddressC
        { get; set; }
        public string AddressP
        { get; set; }
        public string DateOfJoining
        { get; set; }
        public bool IsActive
        { get; set; }

        public Designation Designation { get; set; }

        public string FullName
        {
            get;
            set;
        }
        #endregion
    }
}
