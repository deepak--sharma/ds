using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;
using MySales.Utils;

namespace MySales.BL
{
    public class EmpAttendanceBl
    {
        private EmpAttendanceDl _empAttendanceDl;
        public EmpAttendanceBl(int month,int year)
        {
            Month = month;
            Year = year;
            _empAttendanceDl = new EmpAttendanceDl(Month, Year);            
        }
        public int Month { get; set; }
        public int Year { get; set; }
        
        public EmpAttendance GetEmpAttendance(long empId)
        {
            var empAttDetails = new EmpAttendance();
            try
            {
                empAttDetails = _empAttendanceDl.GetEmpAttendance(empId);
            }
            catch (Exception)
            {


            }
            finally
            {

            }
            return empAttDetails;
        }

        public Utility.ActionStatus AddAttendanceDetails(EmpAttendance att)
        {
            return _empAttendanceDl.AddAttendanceDetails(att);
        }


        public Utility.ActionStatus UpdateAttendanceDetails(EmpAttendance empAttendance)
        {
            //var state = Utility.ActionStatus.SUCCESS;

            return _empAttendanceDl.UpdateAttendanceDetails(empAttendance);
        }
    }
}
