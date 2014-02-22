using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySales.BO
{
    public class EmpAttendance
    {
        public long ID
        { get; set; }
        public long EmpID
        { get; set; }
        public int Month
        { get; set; }
        public int Year
        { get; set; }
        public long TotalDays
        { get; set; }
        public long WorkDays
        { get; set; }
        public long LeaveDays
        { get; set; }
        public Decimal Overtime
        { get; set; }
        public DateTime CreateDate
        { get; set; }
        public DateTime ModifiedDate
        { get; set; }
        public char AttAction { get; set; }
    }
}
