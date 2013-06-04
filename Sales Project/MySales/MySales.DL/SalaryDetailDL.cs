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
    public class SalaryDetailDL
    {
        const string GET_ADV_DETAIL = "SELECT [MonthlyGross] from [Emp_Salary_Details] where [EmpID] = @empID";
        public SalaryDetail GetMonthlyGross(Int64 empID)
        {
            SalaryDetail salaryDetail = new SalaryDetail();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(GET_ADV_DETAIL, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empID));
                        salaryDetail.MonthlyGross = Convert.ToDecimal(cmd.ExecuteScalar());

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }
                }

            }
            catch
            {

            }

            return salaryDetail;

        }
    }
}
