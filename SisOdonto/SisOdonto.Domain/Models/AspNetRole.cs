using System;
using System.Collections.Generic;

namespace SisOdonto.Domain.Models
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
        }

        public string Id { get; set; } = null!;
        public string? ConcurrencyStamp { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public virtual ICollection<AspNetUser> Users { get; set; }
    }
}
