using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
namespace MySales.DL
{
    public class DBManager
    {
        public DBManager()
        {
            //_con = ConfigurationManager.ConnectionStrings["LocalAccessDB"].ConnectionString.Trim();
        }
        private static string _con = ConfigurationManager.ConnectionStrings["LocalAccessDB"].ConnectionString.Trim();

        public static OleDbConnection GetConnection()
        {
            if (_con == "") return null;
            else
                return new OleDbConnection(_con);
        }
        private OleDbCommand GetCommand(string commandText, OleDbConnection con)
        {
            OleDbCommand cmd = new OleDbCommand(commandText, con);
            if (cmd == null) return null;
            else return cmd;
        }
        private OleDbDataAdapter GetDataAdapter(OleDbCommand cmd)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmd);
            if (dataAdapter == null) return null;
            else return dataAdapter;
        }

        public void GetFilledDataset(DataSet inputDS, OleDbDataAdapter inputDataAdapter)
        {
            inputDataAdapter.Fill(inputDS);
        }
        public void GetFilledDataset(DataSet inputDS, string commandText)
        {
            using (OleDbConnection con = GetConnection())
            {
                using (OleDbCommand cmd = GetCommand(commandText, con))
                {
                    using (OleDbDataAdapter adapter = GetDataAdapter(cmd))
                    {
                        adapter.Fill(inputDS);
                    }
                }
            }
        }

        public int ExecuteQuery(OleDbCommand cmd)
        {
            using (OleDbConnection con = GetConnection())
            {
                con.Open();
                using (cmd)
                {
                   
                    Object code = cmd.ExecuteScalar();
                    return (int)code;
                }
            }
        }

        public int ExecuteScalar(string query)
        {
            using (OleDbConnection con = GetConnection())
            {
                con.Open();
                using (OleDbCommand cmd = GetCommand(query, con))
                {                    
                    Object code = cmd.ExecuteScalar();
                    return (int)code;
                }
            }
        }
    }
}
