using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class State
    {
        #region Private members

        #endregion
        #region Constructor
        public State()
        {
            City = new City();
        }
        #endregion
        #region Public Properties

        public City City { get; set; }

        public long Id { get; set; }
        public String Name { get; set; }
        #endregion

    }
}
