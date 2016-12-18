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
    public class AdvanceDetailDl
    {
        const string GetAdvDetail = "SELECT [ID],[EmpID], [AdvanceDeduction],[TotalAdvance],[Balance],[IsActive],[CreateDate],[ModifiedDate] from [Emp_Advance_Details] where [EmpID] = @empID";
        const string GetAllAdvDetail = "SELECT [AdvanceDeduction],[TotalAdvance],[Balance] from [Emp_Advance_Details]";
        const string InsertAdvanceHistory = "INSERT INTO Emp_Advance_History (EmpID,TotalAdvance,AdvanceDeduction,Balance,CreateDate) values (@empid,@total,@deduct,@bal,@cd);";
        const string GetEmployeeAdvanceHistory = "SELECT * FROM Emp_Advance_History WHERE [EmpID]=@empID";
        const string UAdvDetail = "UPDATE [Emp_Advance_Details] set [Balance]=@bal where [EmpID]=@empID";
        private const string InsertAdvanceDetail = "Insert into Emp_Advance_Details (EmpID,TotalAdvance,AdvanceDeduction,Balance,CreateDate,ModifiedDate) values (@empid,@total,@deduct,@bal,@cd,@md);";
        private const string DeleteAdvanceDetail = "delete from Emp_Advance_Details where ";
        public AdvanceDetail GetAdvDetails(Int64 empId)
        {
            var advanceDetail = new AdvanceDetail();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(GetAdvDetail, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empId));
                        //advanceDatail.AdvanceDeduction = Convert.ToDecimal(cmd.ExecuteScalar());
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                advanceDetail.Id = Convert.ToInt32(dr["ID"]);
                                advanceDetail.EmpId = Convert.ToInt32(dr["EmpID"]);
                                advanceDetail.AdvanceDeduction = Convert.ToDecimal(dr["AdvanceDeduction"]);
                                advanceDetail.TotalAdvance = Convert.ToDecimal(dr["TotalAdvance"]);
                                advanceDetail.Balance = Convert.ToDecimal(dr["Balance"]);
                                advanceDetail.IsActive = Convert.ToBoolean(dr["IsActive"]);
                                advanceDetail.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                                advanceDetail.ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
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
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(UAdvDetail, con))
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

        public Utility.ActionStatus AddAdvanceDetails(Employee emp)
        {
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(InsertAdvanceDetail, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@empid",
                                                   Value = emp.Id
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@total",
                            Value = emp.AdvanceDetails.TotalAdvance
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@deduct",
                                                   Value = emp.AdvanceDetails.AdvanceDeduction
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@bal",
                                                   Value = emp.AdvanceDetails.TotalAdvance //first time Balance will be same as total advance
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.DBDate,
                                                   ParameterName = "@cd",
                                                   Value = DateTime.Now
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.DBDate,
                                                   ParameterName = "@md",
                                                   Value = DateTime.Now
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

        public Utility.ActionStatus AddAdvanceHistory(Employee emp)
        {
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(InsertAdvanceHistory, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@empid",
                                                   Value = emp.Id
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                        {
                            OleDbType = OleDbType.Numeric,
                            ParameterName = "@total",
                            Value = emp.AdvanceDetails.TotalAdvance
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@deduct",
                                                   Value = emp.AdvanceDetails.AdvanceDeduction
                                               });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.Numeric,
                                                   ParameterName = "@bal",
                                                   Value = emp.AdvanceDetails.TotalAdvance //first time Balance will be same as total advance
                        });
                        cmd.Parameters.Add(new OleDbParameter()
                                               {
                                                   OleDbType = OleDbType.DBDate,
                                                   ParameterName = "@cd",
                                                   Value = DateTime.Now
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


        public List<AdvanceDetail> GetEmployeeAdvHistory(Int64 empId)
        {
            var advanceDetail = new AdvanceDetail();
            var lstAdvHistory = new List<AdvanceDetail>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(GetEmployeeAdvanceHistory, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@empID", empId));
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                advanceDetail.Id = Convert.ToInt32(dr["ID"]);
                                advanceDetail.EmpId = Convert.ToInt32(dr["EmpID"]);
                                advanceDetail.AdvanceDeduction = Convert.ToDecimal(dr["AdvanceDeduction"]);
                                advanceDetail.TotalAdvance = Convert.ToDecimal(dr["TotalAdvance"]);
                                advanceDetail.Balance = Convert.ToDecimal(dr["Balance"]);
                                advanceDetail.IsActive = Convert.ToBoolean(dr["IsActive"]);
                                advanceDetail.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                                lstAdvHistory.Add(advanceDetail);
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
            return lstAdvHistory;
        }

    }
}
