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
        private readonly EmpAttendanceDl _empAttendanceDl = new EmpAttendanceDl();
        public EmpAttendance GetEmpAttendance(long empId, int month, int year)
        {
            var empAttDetails = new EmpAttendance();
            try
            {
                empAttDetails = _empAttendanceDl.GetEmpAttendance(empId, month, year);
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
