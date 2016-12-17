using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.DL;
using MySales.BO;
using MySales.Utils;

namespace MySales.BL
{
    public class AdvanceDetailsBl
    {
        private readonly AdvanceDetailDl _advanceDetailDl = new AdvanceDetailDl();
        public AdvanceDetail GetAdvDetails(Int64 empId)
        {
            return _advanceDetailDl.GetAdvDetails(empId);

        }


        public Utility.ActionStatus UpdateAdvanceDetails(Employee emp)
        {
            var code = Utility.ActionStatus.SUCCESS;
            try
            {
                code = _advanceDetailDl.UpdateAdvanceDetails(emp);
            }
            catch (Exception)
            {

            }
            return code;
        }

        public Utility.ActionStatus AddAdvanceDetails(Employee emp)
        {
            return _advanceDetailDl.AddAdvanceDetails(emp);
        }
        public Utility.ActionStatus SetupAdvanceDetails(Employee emp)
        {
            /*  Business rules 
            * 1. Check if prev advance details exist or not.
            * 2. Add if no prev data found.
            * 3. Update if prev advance details exists.
            * 4. If prev advance detail exists and the new amt entered is less than the prev then don't add/update.
            */
            var prevAdv = emp.AdvanceDetails ?? GetAdvDetails(emp.Id);
            var result = Utility.ActionStatus.SUCCESS;
            if (prevAdv.Balance > 0)
            {
                var newAdv = new AdvanceDetail {
                    TotalAdvance = prevAdv.TotalAdvance + emp.AdvanceDetails.TotalAdvance,
                    EmpId = emp.Id,
                    AdvanceDeduction = emp.AdvanceDetails.AdvanceDeduction == prevAdv.AdvanceDeduction ? prevAdv.AdvanceDeduction : emp.AdvanceDetails.AdvanceDeduction,
                    Balance = emp.AdvanceDetails.Balance == prevAdv.Balance ? prevAdv.Balance : emp.AdvanceDetails.Balance,
                    ModifiedDate = DateTime.Now
                };
                result = UpdateAdvanceDetails(emp);
            }
            else
            {
                result = AddAdvanceDetails(emp);
            }
            return result;
        }
    }
}
