using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class MonthlyPaid
    {
        public int Salary { get; set; }
        public int? Bonus { get; set; }
        public int MonthlyEmployeeId { get; set; }
    }
}
