using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySales.BO;
using MySales.Utils;
using MySales.DL;

namespace MySales.BL
{
    public class PayrollBl
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
            var state = Utility.ActionStatus.SUCCESS;
            try
            {
                decimal salaryAmt1 = 0;
                decimal salaryAmt2 = 0;
                decimal netPayableSal = 0;
                decimal otAmt = 0;
                var objAdvanceDetailsBl = new AdvanceDetailsBl();
                var objPayroll = new Payroll();
                emp.Attendance = new EmpAttendanceBl(month, year).GetEmpAttendance(emp.Id);
                var lLeaveDays = emp.Attendance.LeaveDays;
                var lOtHrs = emp.Attendance.Overtime;
                emp.AdvanceDetails = objAdvanceDetailsBl.GetAdvDetails(emp.Id);
                var lAdvDedAmt = emp.AdvanceDetails.AdvanceDeduction;

                emp.SalDetails = new SalaryDetailBl().GetMonthlyGross(emp.Id);
                var daysInMonth = DateTime.DaysInMonth(year, month);
                emp.Attendance.WorkDays = daysInMonth - lLeaveDays;
                salaryAmt1 = (emp.SalDetails.MonthlyGross / daysInMonth) * emp.Attendance.WorkDays;
                otAmt = (emp.SalDetails.MonthlyGross / (daysInMonth * 8)) * lOtHrs;
                salaryAmt2 = salaryAmt1 + otAmt;
                if (emp.AdvanceDetails.Balance > 0)
                {
                    decimal salaryAmt3 = 0;
                    if (emp.AdvanceDetails.Balance >= lAdvDedAmt)
                    {
                        salaryAmt3 = salaryAmt2 - lAdvDedAmt;
                        emp.AdvanceDetails.Balance = emp.AdvanceDetails.Balance - lAdvDedAmt;
                    }
                    else
                    {
                        salaryAmt3 = salaryAmt2 - emp.AdvanceDetails.Balance;
                        emp.AdvanceDetails.Balance = 0;
                    }
                    if (emp.AdvanceDetails.Balance == 0)
                    {
                        emp.AdvanceDetails.IsActive = false;
                        objAdvanceDetailsBl.DeactivateAllAdvanceHistoryDetails(emp.Id);
                    }
                    objAdvanceDetailsBl.UpdateAdvanceDetails(emp);
                    netPayableSal = salaryAmt3;
                }
                else
                {
                    netPayableSal = salaryAmt2;
                }
                objPayroll.EmpId = emp.Id;
                objPayroll.DaysWorked = emp.Attendance.WorkDays;
                objPayroll.PYear = year;
                objPayroll.PMonth = month;
                objPayroll.NetPayable = decimal.Round(netPayableSal, 0);
                objPayroll.CreateDate = DateTime.Now;
                objPayroll.IsActive = true;
                objPayroll.AdvanceDedAmt = lAdvDedAmt;
                objPayroll.OvertimeAmt = otAmt;
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
            return new PayrollDl().AddPayroll(objPayroll);
        }

        public List<Employee> FetchPayrollData(int month, int year, Utility.PayrollStatus status, bool isActive)
        {
            var lstEmp = new EmployeeDl().GetAllEmployees();
            var lstPayroll = new PayrollDl().FetchPayrollData(month, year, status, isActive);
            var payrollData = from a in lstEmp
                              join b in lstPayroll on a.Id equals b.EmpId
                              select new Employee()
                              {
                                  PayrollDetails = b,
                                  Id = a.Id,
                                  EmpCode = a.EmpCode,
                                  FirstName = a.FirstName,
                                  MiddleName = a.MiddleName,
                                  LastName = a.LastName,
                                  FullName = a.FullName

                              };
            return payrollData.ToList();
        }

        public List<Employee> GetPayrollGridData(int month, int year)
        {
            return new PayrollDl().GetPayrollGridData(month, year);
        }
    }
}
