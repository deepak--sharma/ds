using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.DL;

namespace MySales.BL
{
    public class DesignationBl
    {
        public List<Designation> GetDesignations()
        {
            return new DesignationDl().GetDesignations();
        }
    }
}
