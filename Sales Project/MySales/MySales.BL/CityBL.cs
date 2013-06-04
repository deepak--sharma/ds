using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class CityBL
    {
        public List<City> GetCityList()
        {
            List<City> cities = new List<City>();

            cities = new CityDL().GetCityList();
            return cities;
        }
    }
}
