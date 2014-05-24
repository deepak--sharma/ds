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
    public class StateDl
    {
        #region Private Constants
        const String SelectState = "SELECT [ID],[STATENAME] FROM [STATE]";
        #endregion

        #region Public Methods
        public List<State> GetStateList()
        {
            var states = new List<State>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectState, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var theState = new State
                                {
                                    Id = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    Name = null != dr["StateName"] ? dr["StateName"].ToString() : string.Empty
                                };
                                states.Add(theState);
                            }
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
            return states;
        } 
        #endregion
    }
}
