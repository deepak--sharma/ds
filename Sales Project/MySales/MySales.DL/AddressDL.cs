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
    public class AddressDL
    {
        const string GET_ADDRESS = "SELECT [ID],[Line1],[State],[City],[Pincode] from [Address] where [ID] = @id";
        const string INSERT_ADDRESS = "Insert into [Address] (Line1,State,City,Pincode) Values (@line1,@state,@city,@pin)";
        const string GET_ID = "Select @@Identity";
        const string UPDATE_ADDRESS = "UPDATE [Address] set [Line1]=@line1,[State]=@state,[City]=@city,[Pincode]=@pin where [ID]=@id";
        private const string DELETE_ADDRESS = "Delete from [Address] where ID=@id";
        public Address GetAddress(Int64 id)
        {
            var address = new Address();
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(GET_ADDRESS, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", id));
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                address.Id = Convert.ToInt64(dr["ID"]);
                                address.Line1 = Convert.ToString(dr["Line1"]);
                                address.StateId = Convert.ToInt64(dr["State"]);
                                address.CityId = Convert.ToInt64(dr["City"]);
                                address.Pincode = Convert.ToInt64(dr["Pincode"]);
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

            return address;

        }
        public Utility.ActionStatus AddAddress(Address address)
        {
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                using (var con = DBManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(INSERT_ADDRESS, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@line1", address.Line1));
                        cmd.Parameters.Add(new OleDbParameter("@state", address.StateId));
                        cmd.Parameters.Add(new OleDbParameter("@city", address.CityId));
                        cmd.Parameters.Add(new OleDbParameter("@pin", address.Pincode));
                        var rowsEffected = cmd.ExecuteNonQuery();
                        if (rowsEffected > 0)
                        {
                            cmd.CommandText = GET_ID;
                            address.Id = Convert.ToInt64(cmd.ExecuteScalar());
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
                code = Utility.ActionStatus.FAILURE;
            }
            return code;
        }
        public void DeleteAddress(Int64 id)
        {
            using (var con = DBManager.GetConnection())
            {
                con.Open();
                using (var cmd = new OleDbCommand(DELETE_ADDRESS, con))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new OleDbParameter("@id", id));
                    cmd.ExecuteNonQuery();
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        //public Utility.ActionStatus UpdateAdvanceDetails(Employee emp)
        //{
        //    Utility.ActionStatus code = Utility.ActionStatus.SUCCESS;
        //    try
        //    {
        //        using (OleDbConnection con = DBManager.GetConnection())
        //        {
        //            con.Open();
        //            using (OleDbCommand cmd = new OleDbCommand(U_ADV_DETAIL, con))
        //            {
        //                cmd.Parameters.Clear();
        //                cmd.Parameters.Add(new OleDbParameter("@bal", emp.AdvanceDetails.Balance));
        //                cmd.Parameters.Add(new OleDbParameter("@empID", emp.Id));
        //                //advanceDatail.AdvanceDeduction = Convert.ToDecimal(cmd.ExecuteScalar());
        //                int rowsEffected = cmd.ExecuteNonQuery();
        //                if (rowsEffected > 0)
        //                {
        //                    code = Utility.ActionStatus.SUCCESS;
        //                }
        //                else
        //                {
        //                    code = Utility.ActionStatus.FAILURE;
        //                }
        //                if (con.State == ConnectionState.Open)
        //                {
        //                    con.Close();
        //                }
        //            }
        //        }

        //    }
        //    catch
        //    {

        //    }
        //    return code;
        //}
    }
}
