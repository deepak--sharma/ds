using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.Utils;
using MySales.BO;
using System.Data.OleDb;

namespace MySales.DL
{
    public class PayrollDL
    {
        private const String InsertPayroll =
            "Insert into [Emp_Payroll] ([EmpID],[OvertimeAmt],[AdvanceDedAmt],[DaysWorked],[NetPayable],[PMonth],[PYear],[Status],[CreateDate]) values (@empid,@otamt,@adamt,@dw,@np,@pmonth,@pyear,@status,@created)";

        private const String FetchPayroll =
            "select [ID],[EmpID],[OvertimeAmt],[AdvanceDedAmt],[DaysWorked],[NetPayable],[PMonth],[PYear],[Status],[IsActive],[CreateDate] from [Emp_Payroll] where [PMonth] = @pm AND [PYear] = @py AND [Status] = @st AND [IsActive] = @act";

        public Utility.ActionStatus AddPayroll(Payroll objPayroll)
        {
            Utility.ActionStatus state = Utility.ActionStatus.SUCCESS;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(InsertPayroll, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empid", objPayroll.EmpID));
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
                using (var con = DBManager.GetConnection())
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
                                    ID = dr["ID"] == DBNull.Value || dr["ID"] == null ? 0 : long.Parse(dr["ID"].ToString()),
                                    EmpID = dr["EmpID"] == DBNull.Value || dr["EmpID"] == null ? 0 : long.Parse(dr["EmpID"].ToString()),
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
    }
}
