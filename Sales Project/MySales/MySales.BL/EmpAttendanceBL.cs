using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;
using MySales.Utils;

namespace MySales.BL
{
    public class EmpAttendanceBL
    {
        private EmpAttendanceDL _empAttendanceDl = new EmpAttendanceDL();
        public EmpAttendance GetEmpAttendance(long empID, int month, int year)
        {
            EmpAttendance empAttDetails = new EmpAttendance();
            try
            {
                empAttDetails = _empAttendanceDl.GetEmpAttendance(empID, month, year);
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
    }
}
