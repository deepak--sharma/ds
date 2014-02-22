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
        const string GET_ALL_ADV_DETAIL = "SELECT [AdvanceDeduction],[TotalAdvance],[Balance] from [Emp_Advance_Details]";
        const string U_ADV_DETAIL = "UPDATE [Emp_Advance_Details] set [Balance]=@bal where [EmpID]=@empID";
        private const string INSERT_ADVANCE_DETAIL = "Insert into Emp_Advance_Details (EmpID,TotalAdvance,AdvanceDeduction,Balance,CreateDate,ModifiedDate) values (@empid,@total,@deduct,@bal,@cd,@md);";
        private const string DELETE_ADVANCE_DETAIL = "delete from Emp_Advance_Details where ";
        public AdvanceDetail GetAdvDetails(Int64 empId)
        {
            var advanceDetail = new AdvanceDetail();
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(GET_ADV_DETAIL, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empId));
                        //advanceDatail.AdvanceDeduction = Convert.ToDecimal(cmd.ExecuteScalar());
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
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
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(U_ADV_DETAIL, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@bal", emp.AdvanceDetails.Balance));
                        cmd.Parameters.Add(new OleDbParameter("@empID", emp.Id));
                        //advanceDatail.AdvanceDeduction = Convert.ToDecimal(cmd.ExecuteScalar());
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

        public Utility.ActionStatus AddAdvanceDetails(AdvanceDetail adv)
        {
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(INSERT_ADVANCE_DETAIL, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@empid",
                                                   Value = adv.EmpID
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@total",
                            Value = adv.TotalAdvance
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@deduct",
                                                   Value = adv.AdvanceDeduction
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@bal",
                                                   Value = adv.Balance
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.DBDate,
                                                   ParameterName = "@cd",
                                                   Value = adv.CreateDate
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.DBDate,
                                                   ParameterName = "@md",
                                                   Value = adv.CreateDate
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
