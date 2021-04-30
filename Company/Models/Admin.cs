using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class Admin
    {
        public Admin()
        {
            CompanyPcs = new HashSet<CompanyPc>();
        }

        public int AdminId { get; set; }

        public virtual ICollection<CompanyPc> CompanyPcs { get; set; }
    }
}
