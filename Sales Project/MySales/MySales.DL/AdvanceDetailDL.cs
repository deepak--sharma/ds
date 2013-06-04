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
    public class AdvanceDetailDL
    {
        const string GET_ADV_DETAIL = "SELECT [AdvanceDeduction],[TotalAdvance],[Balance] from [Emp_Advance_Details] where [EmpID] = @empID";
        const string U_ADV_DETAIL = "UPDATE [Emp_Advance_Details] set [Balance]=@bal where [EmpID]=@empID";
        public AdvanceDetail GetAdvDetails(Int64 empID)
        {
            AdvanceDetail advanceDetail = new AdvanceDetail();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(GET_ADV_DETAIL, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empID));
                        //advanceDatail.AdvanceDeduction = Convert.ToDecimal(cmd.ExecuteScalar());
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                advanceDetail.AdvanceDeduction = Convert.ToDecimal(dr["AdvanceDeduction"]);
                                advanceDetail.TotalAdvance = Convert.ToDecimal(dr["TotalAdvance"]);
                                advanceDetail.Balance = Convert.ToDecimal(dr["Balance"]);
                            }
                        }
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

            return advanceDetail;

        }

        public Utility.ActionStatus UpdateAdvanceDetails(Employee emp)
        {
            Utility.ActionStatus code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(U_ADV_DETAIL, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@bal", emp.AdvanceDetails.Balance));
                        cmd.Parameters.Add(new OleDbParameter("@empID", emp.Id));
                        //advanceDatail.AdvanceDeduction = Convert.ToDecimal(cmd.ExecuteScalar());
                        int rowsEffected = cmd.ExecuteNonQuery();
                        if (rowsEffected > 0)
                        {
                            code = Utility.ActionStatus.SUCCESS;
                        }
                        else
                        {
                            code = Utility.ActionStatus.FAILURE;
                        }
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
            return code;
        }
    }
}
