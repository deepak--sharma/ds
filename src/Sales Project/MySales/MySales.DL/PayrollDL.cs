using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.Utils;
using MySales.BO;
using System.Data.OleDb;

namespace MySales.DL
{
    public class PayrollDl
    {
        private const String InsertPayroll =
            "Insert into [Emp_Payroll] ([EmpID],[OvertimeAmt],[AdvanceDedAmt],[DaysWorked],[NetPayable],[PMonth],[PYear],[Status],[CreateDate]) values (@empid,@otamt,@adamt,@dw,@np,@pmonth,@pyear,@status,@created)";

        private const String FetchPayroll =
            "select [ID],[EmpID],[OvertimeAmt],[AdvanceDedAmt],[DaysWorked],[NetPayable],[PMonth],[PYear],[Status],[IsActive],[CreateDate] from [Emp_Payroll] where [PMonth] = @pm AND [PYear] = @py AND [Status] = @st AND [IsActive] = @act";
        private const string GetPayrollGridDataQuery = "SELECT e.ID,e.FirstName,e.MiddleName,e.LastName, att.Overtime,adv.TotalAdvance,p.OvertimeAmt,p.AdvanceDedAmt,p.DaysWorked,p.NetPayable,p.Status AS PStatus FROM ((Employee e INNER JOIN Emp_Attendance att ON ((e.ID = att.EmpID) AND (att.PayrollMonth=@pm) AND (att.PayrollYear=@py)) ) LEFT JOIN Emp_Advance_Details adv ON (e.ID = adv.EmpID)) LEFT JOIN Emp_Payroll p ON ((e.ID = p.EmpID) AND (p.PMonth = @pm) AND (p.PYear = @py))";
        public Utility.ActionStatus AddPayroll(Payroll objPayroll)
        {
            var state = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(InsertPayroll, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empid", objPayroll.EmpId));
                        cmd.Parameters.Add(new OleDbParameter("@otamt", objPayroll.OvertimeAmt));
                        cmd.Parameters.Add(new OleDbParameter("@adamt", objPayroll.AdvanceDedAmt));
                        cmd.Parameters.Add(new OleDbParameter("@dw", objPayroll.DaysWorked));
                        cmd.Parameters.Add(new OleDbParameter("@np", objPayroll.NetPayable));
                        cmd.Parameters.Add(new OleDbParameter("@pmonth", objPayroll.PMonth));
                        cmd.Parameters.Add(new OleDbParameter("@pyear", objPayroll.PYear));
                        cmd.Parameters.Add(new OleDbParameter("@status", objPayroll.Status));
                        //cmd.Parameters.Add(new OleDbParameter("@isactive", objPayroll.IsActive));
                        cmd.Parameters.Add(new OleDbParameter("@created", objPayroll.CreateDate.ToString()));
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            state = Utility.ActionStatus.FAILURE;
                        }
                    }
                }
            }
            catch (Exception)
            {
                state = Utility.ActionStatus.FAILURE;
            }

            return state;

        }

        public List<Payroll> FetchPayrollData(int month, int year, Utility.PayrollStatus status, bool isActive)
        {
            var lst = new List<Payroll>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(FetchPayroll, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@pm", month));
                        cmd.Parameters.Add(new OleDbParameter("@py", year));
                        cmd.Parameters.Add(new OleDbParameter("@st", status));
                        cmd.Parameters.Add(new OleDbParameter("@act", isActive));
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var payroll = new Payroll()
                                {
                                    Id = dr["ID"] == DBNull.Value || dr["ID"] == null ? 0 : long.Parse(dr["ID"].ToString()),
                                    EmpId = dr["EmpID"] == DBNull.Value || dr["EmpID"] == null ? 0 : long.Parse(dr["EmpID"].ToString()),
                                    OvertimeAmt = dr["OvertimeAmt"] == DBNull.Value || dr["OvertimeAmt"] == null ? 0 : decimal.Parse(dr["OvertimeAmt"].ToString()),
                                    AdvanceDedAmt = dr["AdvanceDedAmt"] == DBNull.Value || dr["AdvanceDedAmt"] == null ? 0 : decimal.Parse(dr["AdvanceDedAmt"].ToString()),
                                    DaysWorked = dr["DaysWorked"] == DBNull.Value || dr["DaysWorked"] == null ? 0 : long.Parse(dr["DaysWorked"].ToString()),
                                    NetPayable = dr["NetPayable"] == DBNull.Value || dr["NetPayable"] == null ? 0 : decimal.Parse(dr["NetPayable"].ToString()),
                                };
                                lst.Add(payroll);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;

        }

        public List<Employee> GetPayrollGridData(int month,int year)
        {
            var lstPayrollGrid = new List<Employee>();

            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(GetPayrollGridDataQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@pm", month));
                        cmd.Parameters.Add(new OleDbParameter("@py", year));
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var emp = new Employee()
                                {
                                    Id = dr["ID"] == DBNull.Value || dr["ID"] == null ? 0 : long.Parse(dr["ID"].ToString()),
                                    FirstName = dr["FirstName"] == DBNull.Value ? string.Empty : dr["FirstName"].ToString(),
                                    MiddleName = dr["MiddleName"] == DBNull.Value ? string.Empty : dr["MiddleName"].ToString(),
                                    LastName = dr["LastName"] == DBNull.Value ? string.Empty : dr["LastName"].ToString(),
                                    Attendance = new EmpAttendance
                                    {
                                        Overtime = dr["Overtime"] == DBNull.Value || dr["Overtime"] == null ? 0 : decimal.Parse(dr["Overtime"].ToString())
                                    },
                                    AdvanceDetails = new AdvanceDetail
                                    {
                                        TotalAdvance = dr["TotalAdvance"] == DBNull.Value || dr["TotalAdvance"] == null ? 0 : decimal.Parse(dr["TotalAdvance"].ToString())
                                    },
                                    PayrollDetails = new Payroll
                                    {
                                        AdvanceDedAmt = dr["AdvanceDedAmt"] == DBNull.Value || dr["AdvanceDedAmt"] == null ? 0 : decimal.Parse(dr["AdvanceDedAmt"].ToString()),
                                        OvertimeAmt = dr["OvertimeAmt"] == DBNull.Value || dr["OvertimeAmt"] == null ? 0 : decimal.Parse(dr["OvertimeAmt"].ToString()),
                                        DaysWorked = dr["DaysWorked"] == DBNull.Value || dr["DaysWorked"] == null ? 0 : Int64.Parse(dr["DaysWorked"].ToString()),
                                        NetPayable = dr["NetPayable"] == DBNull.Value || dr["NetPayable"] == null ? 0 : decimal.Parse(dr["NetPayable"].ToString()),
                                        Status = dr["PStatus"] == DBNull.Value || dr["PStatus"] == null ? Utility.PayrollStatus.TOBECALCULATED : (Utility.PayrollStatus)(Convert.ToInt32(dr["PStatus"]))
                                    }
                                };
                                lstPayrollGrid.Add(emp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstPayrollGrid;
        }
    }
}
