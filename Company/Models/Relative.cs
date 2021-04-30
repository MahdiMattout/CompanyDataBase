using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class Relative
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
    }
}
