﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class AdvanceDetail
    {
        public long ID
        { get; set; }
        public long EmpID
        { get; set; }
        public decimal TotalAdvance
        { get; set; }
        public decimal AdvanceDeduction
        { get; set; }
        public int PMonth
        { get; set; }
        public int PYear
        { get; set; }
        public decimal Balance
        { get; set; }
        public DateTime CreateDate
        { get; set; }
        public DateTime ModifiedDate
        { get; set; }
    }
}
