using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.Utils;
using MySales.DL;

namespace MySales.BL
{
    public class PayrollBL
    {
        /* Algorithm:-
         * Monthly Inputs for Payroll Process:-
         *1.Attendance (No. of leave days)
         *2.OT Hrs
         *3.Adv Ded. Amt.
         * 
         * Payroll Process:-
         * 1.Use Empid
         * 2.Get Monthly Gross
         * 3.Calculate Days Worked (DW)
         *   DW = Total days in a month - Leave Days
         * 4.Calculate Salary Amount (Sal Amt) = (Monthly Gross / Total days in a month) * DW
         * 5.Get Overtime Hours = OT hrs
         * 6.Calculate O.T Amt = (Monthly gross / 8 hrs) * OT hrs
         * 7.Salary with OT = Sal Amt + OT Amt
         * 8.Deductions:-
         * 9.Get Advance Details
         * 10.Get Total Advance Amount (TA)
         * 10.Get Balance (Bal Amt)
         * 11.Get Adv Ded Amt. (AD Amt)
         * 12.Update Balance = TA - AD Amt
         * 13.Calculate net payable sal. = Sal + OT - AD amt.
         * 14.Update Payroll table.
         */
        public Utility.ActionStatus PayrollCalculator(Employee emp, int month, int year)
        {
            Utility.ActionStatus state = Utility.ActionStatus.SUCCESS;
            try
            {
                decimal salaryAmt1 = 0;
                decimal salaryAmt2 = 0;
                decimal salaryAmt3 = 0;
                decimal netPayableSal = 0;
                decimal OTAmt = 0;
                AdvanceDetailsBL objAdvanceDetailsBL = new AdvanceDetailsBL();
                Payroll objPayroll = new Payroll();
                decimal lAdvDedAmt = emp.AdvanceDetails.AdvanceDeduction;
                emp.Attendance = new EmpAttendanceBL().GetEmpAttendance(emp.Id, month, year);
                Int64 lLeaveDays = emp.Attendance.LeaveDays;
                decimal lOTHrs = emp.Attendance.Overtime;
                emp.AdvanceDetails = objAdvanceDetailsBL.GetAdvDetails(emp.Id);
                emp.SalDetails = new SalaryDetailBL().GetMonthlyGross(emp.Id);
                int daysInMonth = DateTime.DaysInMonth(year, month);
                emp.Attendance.WorkDays = daysInMonth - lLeaveDays;
                salaryAmt1 = (emp.SalDetails.MonthlyGross / daysInMonth) * emp.Attendance.WorkDays;
                OTAmt = (emp.SalDetails.MonthlyGross / 8) * lOTHrs;
                salaryAmt2 = salaryAmt1 + OTAmt;
                if (emp.AdvanceDetails.Balance > 0)
                {
                    if (emp.AdvanceDetails.Balance >= emp.AdvanceDetails.AdvanceDeduction)
                    {
                        salaryAmt3 = salaryAmt2 - emp.AdvanceDetails.AdvanceDeduction;
                        emp.AdvanceDetails.Balance = emp.AdvanceDetails.TotalAdvance - emp.AdvanceDetails.AdvanceDeduction;
                    }
                    else
                    {
                        salaryAmt3 = salaryAmt2 - emp.AdvanceDetails.Balance;
                        emp.AdvanceDetails.Balance = 0;
                    }
                    objAdvanceDetailsBL.UpdateAdvanceDetails(emp);
                    netPayableSal = salaryAmt3;
                }
                else
                {
                    netPayableSal = salaryAmt2;
                }
                objPayroll.EmpID = emp.Id;
                objPayroll.DaysWorked = emp.Attendance.WorkDays;
                objPayroll.PYear = year;
                objPayroll.PMonth = month;
                objPayroll.NetPayable = netPayableSal;
                objPayroll.CreateDate = DateTime.Now;
                objPayroll.IsActive = true;
                objPayroll.AdvanceDedAmt = emp.AdvanceDetails.AdvanceDeduction;
                objPayroll.OvertimeAmt = OTAmt;
                objPayroll.Status = Utility.PayrollStatus.CALCULATED;
                state = AddPayroll(objPayroll);

            }
            catch (Exception)
            {
                state = Utility.ActionStatus.FAILURE;
                //throw;
            }
            return state;
        }

        public Utility.ActionStatus AddPayroll(Payroll objPayroll)
        {
            return new PayrollDL().AddPayroll(objPayroll);
        }

    }
}
