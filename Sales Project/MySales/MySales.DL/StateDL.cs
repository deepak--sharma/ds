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
    public class StateDL
    {
        #region Private Constants
        const String SELECT_STATE = "SELECT [ID],[STATENAME] FROM [STATE]";
        #endregion

        #region Public Methods
        public List<State> GetStateList()
        {
            List<State> states = new List<State>();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SELECT_STATE, con))
                    {
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                State theState = new State
                                {
                                    ID = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
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
