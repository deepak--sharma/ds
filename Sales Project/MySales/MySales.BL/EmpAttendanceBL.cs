using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class EmpAttendanceBL
    {
        public EmpAttendance GetEmpAttendance(long empID, int month, int year)
        {
            EmpAttendance empAttDetails = new EmpAttendance();
            try
            {
                empAttDetails = new EmpAttendanceDL().GetEmpAttendance(empID, month, year);
            }
            catch (Exception)
            {


            }
            finally
            {

            }
            return empAttDetails;
        }
    }
}
