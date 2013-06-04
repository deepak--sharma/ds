using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class State
    {
        #region Private members
        private City _city;
        #endregion
        #region Constructor
        public State()
        {
            _city = new City();
        }
        #endregion
        #region Public Properties

        public City City
        {
            get { return _city; }
            set { _city = value; }
        }
        public long ID { get; set; }
        public String Name { get; set; }
        #endregion

    }
}
