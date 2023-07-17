using SisOdonto.Domain.Models;

namespace SisOdonto.Domain.DTO
{
    public class AspNetRoleDTO
    {
        public AspNetRoleDTO()
        {
            AspNetRoleClaimsDTO = new HashSet<AspNetRoleClaimDTO>();
            UsersDTO = new HashSet<AspNetUserDTO>();
        }

        public string Id { get; set; } = null!;
        public string? ConcurrencyStamp { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }

        public virtual ICollection<AspNetRoleClaimDTO> AspNetRoleClaimsDTO { get; set; }

        public virtual ICollection<AspNetUserDTO> UsersDTO { get; set; }
    }
}
