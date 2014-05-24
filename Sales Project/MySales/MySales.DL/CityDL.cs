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
    public class CityDl
    {
        #region Private Constants
        const String SelectCity = "SELECT [ID],[CITYNAME],[STATEID] FROM [CITY]";
        #endregion

        #region Public Methods
        public List<City> GetCityList()
        {
            var cities = new List<City>();
            try
            {
                using (var con = DbManager.GetConnection())
                {
                    con.Open();
                    using (var cmd = new OleDbCommand(SelectCity, con))
                    {
                        var dr = cmd.ExecuteReader();
                        if (dr != null && dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                var theCity = new City
                                {
                                    Id = null != dr["ID"] && string.Empty != dr["ID"].ToString().Trim() ? long.Parse(dr["ID"].ToString().Trim()) : 0,
                                    StateId = null != dr["StateID"] && string.Empty != dr["StateID"].ToString().Trim() ? long.Parse(dr["StateID"].ToString().Trim()) : 0,
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
