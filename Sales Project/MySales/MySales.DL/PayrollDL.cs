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
        private const String INSERT_PAYROLL = "Insert into [Emp_Payroll] ([EmpID],[OvertimeAmt],[AdvanceDedAmt],[DaysWorked],[NetPayable],[PMonth],[PYear],[Status],[CreateDate]) values (@empid,@otamt,@adamt,@dw,@np,@pmonth,@pyear,@status,@created)";

        public Utility.ActionStatus AddPayroll(Payroll objPayroll)
        {
            Utility.ActionStatus state = Utility.ActionStatus.SUCCESS;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(INSERT_PAYROLL, con))
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
    }
}
