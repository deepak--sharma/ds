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
        private const string GetMissingAttendance = "SELECT e.Id FROM Employee e LEFT JOIN Emp_Attendance att ON ((e.ID = att.EmpID) AND (att.PayrollMonth=@pm) AND (att.PayrollYear=@py)) WHERE att.EmpID IS NULL AND e.IsActive=true";
        public int Month { get; set; }
        public int Year { get; set; }
        public EmpAttendanceDl(int month,int year)
        {
            Month = month;
            Year = year;
            SynchroniseAttendanceData();
        }

        public EmpAttendance GetEmpAttendance(long empId)
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
                        cmd.Parameters.Add(new OleDbParameter("@mnt", Month));
                        cmd.Parameters.Add(new OleDbParameter("@yr", Year));
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                empAttDetails.EmpId = empId;
                                empAttDetails.Id = Convert.ToInt64(dr["ID"]);
                                empAttDetails.TotalDays = Convert.ToInt64(dr["TotalDays"]);
                                empAttDetails.Month = Month;
                                empAttDetails.Year = Year;
                                empAttDetails.WorkDays = Convert.ToInt64(dr["WorkDays"]);
                                empAttDetails.LeaveDays = Convert.ToInt64(dr["LeaveDays"]);
                                empAttDetails.Overtime = Convert.ToDecimal(dr["Overtime"]);
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

        public List<EmpAttendance> GetAllEmpAttendance(string empIds)
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
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@mnt", Value = Month });
                        cmd.Parameters.Add(new OleDbParameter { ParameterName = "@yr", Value = Year });
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
                                    Month = Month,
                                    Year = Year,
                                    WorkDays = dr["WorkDays"] == DBNull.Value ? 0 : Convert.ToInt64(dr["WorkDays"]),
                                    LeaveDays = dr["LeaveDays"] == DBNull.Value ? 0 : Convert.ToInt64(dr["LeaveDays"]),
                                    Overtime = dr["Overtime"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Overtime"]),
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
                                    Value = Month,
                                    OleDbType = OleDbType.Numeric
                                });
                                cmd.Parameters.Add(new OleDbParameter()
                                {
                                    ParameterName = "@py",
                                    Value = Year,
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
                            lstAtt = GetAllEmpAttendance(empIds);
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

        private void SynchroniseAttendanceData()
        {
            /*
             * 1. Get employees who have missing attendance for given payroll month-year
             * 2. Insert blank record for such employees.
             */
            var lstEmpIdWithMissingAttendance = new List<string>();
            using (var con = DbManager.GetConnection())
            {
                con.Open();
                using (var cmd = new OleDbCommand())
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.CommandText = GetMissingAttendance;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter { ParameterName = "@pm", Value = Month });
                    cmd.Parameters.Add(new OleDbParameter { ParameterName = "@py", Value = Year });
                    var dr = cmd.ExecuteReader();
                    if (dr != null && dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (DBNull.Value != dr["Id"])
                            {
                                lstEmpIdWithMissingAttendance.Add(Convert.ToString(dr["Id"]));
                            }
                        }
                    }
                }
                if(lstEmpIdWithMissingAttendance.Count >0)
                {
                    var sb = new StringBuilder();
                    foreach (var eid in lstEmpIdWithMissingAttendance)
                    {
                        AddAttendanceDetails(new EmpAttendance {
                            EmpId = Convert.ToInt64(eid),
                            Month = Month,
                            Year = Year,
                            TotalDays = DateTime.DaysInMonth(Year,Month),
                            WorkDays = 0,
                            LeaveDays = 0,
                            Overtime = 0,
                            CreateDate = DateTime.Now                         
                        });
                    }                    
                }

                
            }
        }
    }
}
