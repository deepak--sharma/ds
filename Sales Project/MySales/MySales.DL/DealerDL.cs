using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using System.Data.OleDb;
using System.Data;
using MySales.Utils;

namespace MySales.DL
{
    public class DealerDl
    {

        private const string DealerInsertQuery = "Insert into Dealer (ID,Name,Address,Description,CreationDate) values (@id,@name,@add,@desc,@dt)";
        private const string SelectMaxDealer = "Select MAX(ID) FROM Dealer";
        private const string SelectDealerNames = "Select Distinct(Name),ID from Dealer order by Name asc";

        public int InsertDealer(Dealer theDealer)
        {
            int statusCode = -1;
            try
            {
                theDealer.Id = GetNextDealerId();
                if (theDealer.Id < 1)
                {
                    return statusCode;
                }
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(DealerInsertQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theDealer.Id));
                        cmd.Parameters.Add(new OleDbParameter("@name", theDealer.Name));
                        cmd.Parameters.Add(new OleDbParameter("@add", theDealer.Address));
                        cmd.Parameters.Add(new OleDbParameter("@desc", theDealer.Description));
                        cmd.Parameters.Add(new OleDbParameter("@dt", theDealer.CreateDate.ToString()));
                        statusCode = cmd.ExecuteNonQuery();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return statusCode;
        }

        public long GetNextDealerId()
        {
            long dealerId = 1;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectMaxDealer, con))
                    {
                        var statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_DealerID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out dealerId);
                            dealerId += 1;
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dealerId;
        }


        public List<Dealer> GetDealerNames()
        {
            var lstDealer = new List<Dealer>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectDealerNames, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null)
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var objDealer = new Dealer();
                                    objDealer.Name = dr["Name"].ToString();
                                    //objDealer.ID = lon dr["ID"];
                                    long id = 0;
                                    long.TryParse(dr["ID"].ToString().Trim(), out id);
                                    objDealer.Id = id;
                                    lstDealer.Add(objDealer);
                                }
                            }

                            dr.Close();
                            dr.Dispose();
                        }
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstDealer;
        }
    }
}
