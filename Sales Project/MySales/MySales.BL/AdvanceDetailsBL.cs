using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.DL;
using MySales.BO;
using MySales.Utils;

namespace MySales.BL
{
    public class AdvanceDetailsBL
    {
        public AdvanceDetail GetAdvDetails(Int64 empID)
        {
            return new AdvanceDetailDL().GetAdvDetails(empID);

        }


        public Utility.ActionStatus UpdateAdvanceDetails(Employee emp)
        {
            Utility.ActionStatus code = Utility.ActionStatus.SUCCESS;
            try
            {
                code = new AdvanceDetailDL().UpdateAdvanceDetails(emp);
            }
            catch (Exception)
            {

            }
            return code;
        }

    }
}
