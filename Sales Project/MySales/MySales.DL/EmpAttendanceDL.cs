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
        private const string INSERT_ATT = "Insert into Emp_Attendance (EmpID,PayrollMonth,PayrollYear,TotalDays,WorkDays,LeaveDays,Overtime,CreateDate,ModifiedDate) values (@empid,@mon,@yr,@td,@wd,@ld,@ot,@cd,@md);";
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

        public Utility.ActionStatus AddAttendanceDetails(EmpAttendance att)
        {
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(INSERT_ATT, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@empid",
                            Value = att.EmpID
                        });

                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@mon",
                            Value = att.Month
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@yr",
                            Value = att.Year
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@td",
                            Value = att.TotalDays
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@wd",
                            Value = att.WorkDays
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@ld",
                            Value = att.LeaveDays
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@ot",
                            Value = att.Overtime
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.DBDate,
                            ParameterName = "@cd",
                            Value = att.CreateDate
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.DBDate,
                            ParameterName = "@md",
                            Value = DBNull.Value
                        });
                        var rowsEffected = cmd.ExecuteNonQuery();
                        code = rowsEffected > 0 ? Utility.ActionStatus.SUCCESS : Utility.ActionStatus.FAILURE;
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }

            }
            catch
            {
                code = Utility.ActionStatus.FAILURE;
            }
            return code;
        }
    }
}
