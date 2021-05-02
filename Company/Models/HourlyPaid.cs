using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class HourlyPaid
    {
        public int HourlyWage { get; set; }
        public int? OvertimeWage { get; set; }
        public int HourlyEmployeeId { get; set; }

        public virtual Employee HourlyEmployee { get; set; }
    }
}
