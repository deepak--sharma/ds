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
    public class DesignationDL
    {

        #region Private Constants
        const String SELECT_DESIG = "SELECT [ID],[DESC] FROM [DESIGNATION]";
        #endregion

        #region Public Methods
        public List<Designation> GetDesignations()
        {
            List<Designation> lstDesig = new List<Designation>();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SELECT_DESIG, con))
                    {
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Designation theDesig = new Designation
                                {
                                    ID = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    Desc = Convert.ToString(dr["DESC"])
                                };
                                lstDesig.Add(theDesig);
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
            return lstDesig;
        }
        #endregion
    }
}
