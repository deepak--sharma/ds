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

    public class ClientDl
    {
        private const string ClientInsertQuery = "Insert into Client (ID,Name,Address,Description,CreationDate) values (@id,@name,@add,@desc,@dt)";
        private const string SelectMaxClient = "Select MAX(ID) FROM CLIENT";
        private const string SelectClientNames = "Select Distinct(Name),ID from Client order by Name asc";
        public int InsertClient(Client theClient)
        {
            var statusCode = -1;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(ClientInsertQuery, con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new OleDbParameter("@id", theClient.Id));
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

        public long GetNextClientId()
        {
            long clientId = 1;
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectMaxClient, con))
                    {
                        var statusCode = cmd.ExecuteScalar();
                        if (statusCode != DBNull.Value)
                        {
                            //_clientID = (long)statusCode + 1;
                            long.TryParse(statusCode.ToString(), out clientId);
                            clientId += 1;
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
            return clientId;
        }

        public List<Client> GetClientNames()
        {
            var lstClient = new List<Client>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectClientNames, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null)
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var objClient = new Client
                                                {
                                                    Name = dr["Name"].ToString()
                                                };
                                    //objClient.ID = lon dr["ID"];
                                    long id = 0;
                                    long.TryParse(dr["ID"].ToString().Trim(), out id);
                                    objClient.Id = id;
                                    lstClient.Add(objClient);
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
            return lstClient;
        }
    }
}
