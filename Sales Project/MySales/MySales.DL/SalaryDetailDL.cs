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
        const string ADD_EMP_SALARY = "INSERT INTO [Emp_Salary_Details] (EmpID,MonthlyGross,CreateDate) values (@EmpID,@MonthlyGross,@CreateDate)";
        private const string UpdateSalary = "UPDATE [Emp_Salary_Details] set MonthlyGross = @mg, ModifiedDate = @md where ID = @id";

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
        public int AddUpdateSalaryDetails(Employee emp)
        {
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var command = new OleDbCommand())
                    {
                        command.Connection = con;
                        command.Parameters.Clear();
                        if (emp.SalDetails.ID > 0)
                        {
                            command.CommandText = UpdateSalary;
                            command.Parameters.Add(new OleDbParameter { ParameterName = "@mg", Value = emp.SalDetails.MonthlyGross });
                            command.Parameters.Add(new OleDbParameter { ParameterName = "@md", Value = DateTime.Now, OleDbType = OleDbType.Date });
                            command.Parameters.Add(new OleDbParameter { ParameterName = "@id", Value = emp.SalDetails.ID });
                        }
                        else
                        {
                            command.CommandText = ADD_EMP_SALARY;
                            command.Parameters.Add(new OleDbParameter { ParameterName = "@EmpID", Value = emp.Id });
                            command.Parameters.Add(new OleDbParameter { ParameterName = "@MonthlyGross", Value = emp.SalDetails.MonthlyGross });
                            command.Parameters.Add(new OleDbParameter { ParameterName = "@CreateDate", Value = emp.SalDetails.CreateDate, OleDbType = OleDbType.Date });
                        }

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
