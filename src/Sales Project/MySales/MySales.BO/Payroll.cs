﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class Payroll
    {
        public long Id
        { get; set; }
        public long EmpId
        { get; set; }
        public decimal OvertimeAmt
        { get; set; }
        public decimal AdvanceDedAmt
        { get; set; }
        public long DaysWorked
        { get; set; }
        public decimal NetPayable
        { get; set; }
        public int PMonth
        { get; set; }
        public int PYear
        { get; set; }
        public Utils.Utility.PayrollStatus Status
        { get; set; }
        public bool IsActive
        { get; set; }
        public DateTime CreateDate
        { get; set; }
        public DateTime ModifiedDate
        { get; set; }
    }
}
