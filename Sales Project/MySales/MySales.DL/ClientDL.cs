using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using MySales.BO;
using System.Data;
using MySales.Utils;

namespace MySales.DL
{

    public class ClientDL
    {
        private const string ClientInsertQuery = "Insert into Client (ID,Name,Address,Description,CreationDate) values (@id,@name,@add,@desc,@dt)";
        private const string SelectMaxClient = "Select MAX(ID) FROM CLIENT";
        private const string SelectClientNames = "Select Distinct(Name),ID from Client order by Name asc";
        public int InsertClient(Client theClient)
        {
            int statusCode = -1;
            try
            {                
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(ClientInsertQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theClient.ID));
                        cmd.Parameters.Add(new OleDbParameter("@name", theClient.Name));
                        cmd.Parameters.Add(new OleDbParameter("@add", theClient.Address));
                        cmd.Parameters.Add(new OleDbParameter("@desc", theClient.Description));
                        cmd.Parameters.Add(new OleDbParameter("@dt", theClient.CreateDate.ToString()));
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

        public long GetNextClientID()
        {
            long _clientID = 1;
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectMaxClient, con))
                    {
                        Object statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_clientID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out _clientID);
                            _clientID += 1;
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
            return _clientID;
        }

        public List<Client> GetClientNames()
        {
            List<Client> lstClient = new List<Client>();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SelectClientNames, con))
                    {
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Client objClient = new Client();
                                objClient.Name = dr["Name"].ToString();
                                //objClient.ID = lon dr["ID"];
                                long _id = 0;
                                long.TryParse(dr["ID"].ToString().Trim(), out _id);
                                objClient.ID = _id;
                                lstClient.Add(objClient);
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
            return lstClient;
        }
    }
}
