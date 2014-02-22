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
        private AdvanceDetailDL _advanceDetailDl = new AdvanceDetailDL();
        public AdvanceDetail GetAdvDetails(Int64 empID)
        {
            return _advanceDetailDl.GetAdvDetails(empID);

        }


        public Utility.ActionStatus UpdateAdvanceDetails(Employee emp)
        {
            Utility.ActionStatus code = Utility.ActionStatus.SUCCESS;
            try
            {
                code = _advanceDetailDl.UpdateAdvanceDetails(emp);
            }
            catch (Exception)
            {

            }
            return code;
        }

        public Utility.ActionStatus AddAdvanceDetails(AdvanceDetail adv)
        {
            /*  Business rules 
             * 1. Check if prev advance details exist or not.
             * 2. Add if no prev data found.
             * 3. Update if there is a prev advance details exists.
             * 4. If prev advance detail exists and the new amt entered is less than the prev then don't add/update.
             */
            return _advanceDetailDl.AddAdvanceDetails(adv);
        }

    }
}
