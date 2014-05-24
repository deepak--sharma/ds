using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
namespace MySales.Utils
{
    public class DbManager
    {
        public DbManager()
        {
            //_con = ConfigurationManager.ConnectionStrings["LocalAccessDB"].ConnectionString.Trim();
        }


        private static readonly string Con = ConfigurationManager.ConnectionStrings["LocalAccessDB"].ConnectionString.Trim();

        public static OleDbConnection GetConnection()
        {
            return Con == "" ? null : new OleDbConnection(Con);
        }

        private static OleDbCommand GetCommand(string commandText, OleDbConnection con)
        {
            var cmd = new OleDbCommand(commandText, con);
            return cmd;
        }

        private static OleDbDataAdapter GetDataAdapter(OleDbCommand cmd)
        {
            var dataAdapter = new OleDbDataAdapter(cmd);
            return dataAdapter;
        }

        public void GetFilledDataset(DataSet inputDs, OleDbDataAdapter inputDataAdapter)
        {
            inputDataAdapter.Fill(inputDs);
        }
        public void GetFilledDataset(DataSet inputDs, string commandText)
        {
            using (var con = GetConnection())
            {
                using (var cmd = GetCommand(commandText, con))
                {
                    using (var adapter = GetDataAdapter(cmd))
                    {
                        adapter.Fill(inputDs);
                    }
                }
            }
        }

        public int ExecuteQuery(OleDbCommand cmd)
        {
            using (var con = GetConnection())
            {
                con.Open();
                using (cmd)
                {

                    var code = cmd.ExecuteScalar();
                    return (int)code;
                }
            }
        }

        public int ExecuteScalar(string query)
        {
            using (var con = GetConnection())
            {
                con.Open();
                using (var cmd = GetCommand(query, con))
                {
                    var code = cmd.ExecuteScalar();
                    return (int)code;
                }
            }
        }
    }
}
