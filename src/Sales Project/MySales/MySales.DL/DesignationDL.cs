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
    public class DesignationDl
    {

        #region Private Constants
        const String SelectDesig = "SELECT [ID],[DESC] FROM [DESIGNATION] WHERE ISACTIVE=TRUE";
        #endregion

        #region Public Methods
        public List<Designation> GetDesignations()
        {
            var lstDesig = new List<Designation>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectDesig, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var theDesig = new Designation
                                {
                                    Id = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
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
