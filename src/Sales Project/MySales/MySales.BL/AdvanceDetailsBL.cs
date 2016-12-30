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
            if (emp.AdvanceDetails.TotalAdvance > 0)
            {
                //Add Advance History
                var result = _advanceDetailDl.AddAdvanceHistory(emp);
            }
            return _advanceDetailDl.AddAdvanceDetails(emp);
        }
        public Utility.ActionStatus SetupAdvanceDetails(Employee emp)
        {
            var result = Utility.ActionStatus.SUCCESS;

            if (emp.AdvanceDetails.Id <= 0)
            {
                result = AddAdvanceDetails(emp);
            }
            else
            {
                result = UpdateAdvanceDetails(emp);
                if(emp.AdvanceHistory.Count>0)
                {
                    var newHistory = emp.AdvanceHistory.FirstOrDefault(x => x.Id == 0);
                    if (newHistory != null)
                    {
                        _advanceDetailDl.AddAdvanceHistory(new Employee
                        {
                            Id = emp.Id,
                            AdvanceDetails = new AdvanceDetail
                            {
                                TotalAdvance = newHistory.TotalAdvance,
                                Balance = newHistory.Balance,
                                AdvanceDeduction = newHistory.AdvanceDeduction
                            }
                        });
                    }
                    var inActiveAdv = emp.AdvanceHistory.FirstOrDefault(x => x.IsActive == false);
                    if (inActiveAdv != null)
                    {
                        //Update advance history
                        _advanceDetailDl.UpdateAdvanceHistoryDetails(inActiveAdv.Id);
                    }                    
                }
            }

            return result;
        }


        public List<AdvanceDetail> GetEmployeeAdvHistory(Int64 empId,bool activeOnly)
        {
            return _advanceDetailDl.GetEmployeeAdvHistory(empId, activeOnly);
        }
    }
}
