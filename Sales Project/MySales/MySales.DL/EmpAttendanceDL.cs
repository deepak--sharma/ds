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
    public class EmpAttendanceDl
    {
        private const string SelectAtt = "Select [ID],[TotalDays],[WorkDays],[LeaveDays],[Overtime],[CreateDate],[ModifiedDate] from [Emp_Attendance] where [EmpID] = @empID And [PayrollMonth]=@mnt And [PayrollYear]=@yr";
        private const string SelectAllAtt = "Select [ID],[TotalDays],[WorkDays],[LeaveDays],[Overtime],[CreateDate],[ModifiedDate] from [Emp_Attendance] where [EmpID] In (@empID) And [PayrollMonth]=@mnt And [PayrollYear]=@yr";
        private const string InsertAtt = "Insert into Emp_Attendance (EmpID,PayrollMonth,PayrollYear,TotalDays,WorkDays,LeaveDays,Overtime,CreateDate,ModifiedDate) values (@empid,@mon,@yr,@td,@wd,@ld,@ot,@cd,@md);";
        private const string UpdateAttendance = "Update Emp_Attendance set TotalDays=@td,WorkDays=@wd,LeaveDays=@ld,Overtime=@ot,ModifiedDate=@md where [ID] = @attId";
        private const string InsertBlankAttendanceRecordsForCurrentPayroll = "Insert into Emp_Attendance (EmpID,PayrollMonth,PayrollYear,CreateDate) values (@eid,@pm,@py,@cd)";
        public EmpAttendance GetEmpAttendance(long empId, int month, int year)
        {
            var empAttDetails = new EmpAttendance();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectAtt, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empId));
                        cmd.Parameters.Add(new OleDbParameter("@mnt", month));
                        cmd.Parameters.Add(new OleDbParameter("@yr", year));
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                empAttDetails.EmpId = empId;
                                empAttDetails.Id = Convert.ToInt64(dr["ID"]);
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
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(InsertAtt, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@empid",
                            Value = att.EmpId
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

        public List<EmpAttendance> GetAllEmpAttendance(string empIds, int month, int year)
        {
            var lstAtt = new List<EmpAttendance>();
            try
            {
                var idParams = empIds.Split(',');
                var parameters = new string[idParams.Length];



                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Clear();
                        for (var i = 0; i < idParams.Length; i++)
                        {
                            parameters[i] = "@p" + i;
                            cmd.Parameters.AddWithValue(parameters[i], idParams[i]);
                        }
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@mnt", Value = month });
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@yr", Value = year });
                        cmd.CommandText = "Select [ID],[EmpID],[TotalDays],[WorkDays],[LeaveDays],[Overtime],[CreateDate],[ModifiedDate] from [Emp_Attendance] where [EmpID] In (" + string.Join(",", parameters) + ") And [PayrollMonth]=@mnt And [PayrollYear]=@yr";
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            //var dt = new DataTable();
                            //dt.Load(dr);
                            //if (idParams.Length != dt.Rows.Count)
                            //{
                            //    //Write code here to extract those Ids from idParams which do not exist in db
                            //}

                            while (dr.Read())
                            {
                                var empAttDetails = new EmpAttendance()
                                                        {
                                                            EmpId = dr["EmpID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["EmpID"]),
                                                            Id = dr["ID"] == DBNull.Value ? 0 : Convert.ToInt64(dr["ID"]),
                                                            TotalDays = dr["TotalDays"] == DBNull.Value ? 0 : Convert.ToInt64(dr["TotalDays"]),
                                                            Month = month,
                                                            Year = year,
                                                            WorkDays = dr["WorkDays"] == DBNull.Value ? 0 : Convert.ToInt64(dr["WorkDays"]),
                                                            LeaveDays = dr["LeaveDays"] == DBNull.Value ? 0 : Convert.ToInt64(dr["LeaveDays"]),
                                                            Overtime = dr["Overtime"] == DBNull.Value ? 0 : Convert.ToInt64(dr["Overtime"]),
                                                            CreateDate =
                                                                dr["CreateDate"] == DBNull.Value
                                                                    ? DateTime.MinValue
                                                                    : Convert.ToDateTime(dr["CreateDate"]),
                                                            ModifiedDate =
                                                                dr["ModifiedDate"] == DBNull.Value
                                                                    ? DateTime.MinValue
                                                                    : Convert.ToDateTime(dr["ModifiedDate"]),
                                                        };
                                lstAtt.Add(empAttDetails);

                            }
                        }
                        else
                        {
                            if (dr != null) dr.Close();
                            cmd.Connection = con;
                            cmd.CommandText = InsertBlankAttendanceRecordsForCurrentPayroll;
                            foreach (var t in idParams)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add(new OleDbParameter()
                                                       {
                                                           ParameterName = "@eid",
                                                           Value = Convert.ToInt64(t),
                                                           OleDbType = OleDbType.Numeric
                                                       });
                                cmd.Parameters.Add(new OleDbParameter()
                                {
                                    ParameterName = "@pm",
                                    Value = month,
                                    OleDbType = OleDbType.Numeric
                                });
                                cmd.Parameters.Add(new OleDbParameter()
                                {
                                    ParameterName = "@py",
                                    Value = year,
                                    OleDbType = OleDbType.Numeric
                                });
                                cmd.Parameters.Add(new OleDbParameter()
                                {
                                    ParameterName = "@cd",
                                    Value = DateTime.Now,
                                    OleDbType = OleDbType.Date
                                });

                                var status = cmd.ExecuteNonQuery();
                            }
                            GetAllEmpAttendance(empIds, month, year);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lstAtt;
        }

        public Utility.ActionStatus UpdateAttendanceDetails(EmpAttendance empAtt)
        {
            var state = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(UpdateAttendance, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   ParameterName = "@td",
                                                   Value = empAtt.TotalDays,
                                                   OleDbType = OleDbType.BigInt
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            ParameterName = "@wd",
                            Value = empAtt.WorkDays,
                            OleDbType = OleDbType.BigInt
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            ParameterName = "@ld",
                            Value = empAtt.LeaveDays,
                            OleDbType = OleDbType.BigInt
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            ParameterName = "@ot",
                            Value = empAtt.Overtime,
                            OleDbType = OleDbType.Numeric
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            ParameterName = "@md",
                            Value = empAtt.ModifiedDate,
                            OleDbType = OleDbType.Date
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            ParameterName = "@attId",
                            Value = empAtt.Id,
                            OleDbType = OleDbType.BigInt
                        });
                        var rowsEffected = cmd.ExecuteNonQuery();
                        if (rowsEffected <= 0)
                        {
                            state = Utility.ActionStatus.FAILURE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                state = Utility.ActionStatus.FAILURE;
            }
            return state;
        }
    }
}
