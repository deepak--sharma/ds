using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class DesignationBL
    {
        public List<Designation> GetDesignations()
        {
            return new DesignationDL().GetDesignations();
        }
    }
}
