namespace SisOdonto.Domain.DTO
{
    public class AspNetUserDTO
    {
        public AspNetUserDTO()
        {
            AspNetUserClaimsDTO = new HashSet<AspNetUserClaimDTO>();
            AspNetUserLoginsDTO = new HashSet<AspNetUserLoginDTO>();
            RolesDTO = new HashSet<AspNetRoleDTO>();
        }

        public string Id { get; set; } = null!;
        public int AccessFailedCount { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string? NormalizedEmail { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string? SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string? UserName { get; set; }
        public string? Nome { get; set; }
        public bool? UsuarioAtivo { get; set; }
        public DateTime? LastAccess { get; set; }

        public virtual ICollection<AspNetUserClaimDTO> AspNetUserClaimsDTO { get; set; }
        public virtual ICollection<AspNetUserLoginDTO> AspNetUserLoginsDTO { get; set; }

        public virtual ICollection<AspNetRoleDTO> RolesDTO { get; set; }
    }
}
