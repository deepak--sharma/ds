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
    public class DealerDL
    {

        private const string DealerInsertQuery = "Insert into Dealer (ID,Name,Address,Description,CreationDate) values (@id,@name,@add,@desc,@dt)";
        private const string SelectMaxDealer = "Select MAX(ID) FROM Dealer";
        private const string SelectDealerNames = "Select Distinct(Name),ID from Dealer order by Name asc";

        public int InsertDealer(Dealer theDealer)
        {
            int statusCode = -1;
            try
            {
                theDealer.ID = GetNextDealerID();
                if (theDealer.ID < 1)
                {
                    return statusCode;
                }
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(DealerInsertQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theDealer.ID));
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

        public long GetNextDealerID()
        {
            long _DealerID = 1;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectMaxDealer, con))
                    {
                        Object statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_DealerID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out _DealerID);
                            _DealerID += 1;
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
            return _DealerID;
        }


        public List<Dealer> GetDealerNames()
        {
            List<Dealer> lstDealer = new List<Dealer>();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectDealerNames, con))
                    {
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Dealer objDealer = new Dealer();
                                objDealer.Name = dr["Name"].ToString();
                                //objDealer.ID = lon dr["ID"];
                                long _id = 0;
                                long.TryParse(dr["ID"].ToString().Trim(), out _id);
                                objDealer.ID = _id;
                                lstDealer.Add(objDealer);
                            }
                        }
                        dr.Close();
                        dr.Dispose();
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
