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
        const string GET_GROSS_SALARY = "SELECT [MonthlyGross] from [Emp_Salary_Details] where [EmpID] = @empID";
        const string ADD_EMP_SALARY = "INSERT INTO [Emp_Salary_Details] (EmpID,MonthlyGross,CreatedDate) values (@EmpID,@MonthlyGross,@CreatedDate)";
        public SalaryDetail GetMonthlyGross(Int64 empID)
        {
            SalaryDetail salaryDetail = new SalaryDetail();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(GET_GROSS_SALARY, con))
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
        public int AddSalaryDetails(Employee emp)
        {
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var command = new OleDbCommand(ADD_EMP_SALARY, con))
                    {
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@EmpID", Value = emp.Id });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@MonthlyGross", Value = emp.SalDetails.MonthlyGross });
                        command.Parameters.Add(new OleDbParameter { ParameterName = "@CreateDate", Value = emp.SalDetails.CreateDate });
                        return command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
