using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Company.Models
{
    public partial class MonthlyPaid
    {
        public int Salary { get; set; }
        [RegularExpression(@"^[0-9]*$")]
        [Required]
        public int? Bonus { get; set; }
        public int MonthlyPaidEmployeeId { get; set; }

        public virtual Employee MonthlyPaidEmployee { get; set; }
    }
}
