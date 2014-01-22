using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using System.Data.OleDb;
using MySales.Utils;
using System.Data;

namespace MySales.DL
{
    public class EmpAttendanceDL
    {
        private const string SELECT_ATT = "Select [ID],[TotalDays],[WorkDays],[LeaveDays],[Overtime],[CreateDate],[ModifiedDate] from [Emp_Attendance] where [EmpID] = @empID And [PayrollMonth]=@mnt And [PayrollYear]=@yr";
        public EmpAttendance GetEmpAttendance(long empID, int month, int year)
        {
            EmpAttendance empAttDetails = new EmpAttendance();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SELECT_ATT, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empID));
                        cmd.Parameters.Add(new OleDbParameter("@mnt", month));
                        cmd.Parameters.Add(new OleDbParameter("@yr", year));
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                empAttDetails.EmpID = empID;
                                empAttDetails.ID = Convert.ToInt64(dr["ID"]);
                                empAttDetails.TotalDays = Convert.ToInt64(dr["TotalDays"]);
                                empAttDetails.Month = month;
                                empAttDetails.Year = year;
                                empAttDetails.WorkDays = Convert.ToInt64(dr["WorkDays"]);
                                empAttDetails.LeaveDays = Convert.ToInt64(dr["LeaveDays"]);
                                empAttDetails.Overtime = Convert.ToInt64(dr["Overtime"]);
                                empAttDetails.CreateDate = dr["CreateDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["CreateDate"]);
                                empAttDetails.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModifiedDate"]);

                            }
                        }
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }

                }
            }
            catch (Exception)
            {

            }

            return empAttDetails;
        }
    }
}
