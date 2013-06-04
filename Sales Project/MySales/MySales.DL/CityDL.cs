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
    public class CityDL
    {
        #region Private Constants
        const String SELECT_CITY = "SELECT [ID],[CITYNAME],[STATEID] FROM [CITY]";
        #endregion

        #region Public Methods
        public List<City> GetCityList()
        {
            List<City> cities = new List<City>();
            try
            {
                using (OleDbConnection con = DBManager.GetConnection())
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(SELECT_CITY, con))
                    {
                        OleDbDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                City theCity = new City
                                {
                                    ID = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    StateID = null != dr["StateID"] && string.Empty != dr["StateID"].ToString().Trim() ? long.Parse(dr["StateID"].ToString().Trim()) : 0,
                                    Name = null != dr["CityName"] ? dr["CityName"].ToString() : string.Empty
                                };
                                cities.Add(theCity);
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
            return cities;
        }
        #endregion

    }
}
