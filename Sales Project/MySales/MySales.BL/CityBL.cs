using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class CityBl
    {
        public List<City> GetCityList()
        {
            var cities = new CityDl().GetCityList();
            return cities;
        }
    }
}
