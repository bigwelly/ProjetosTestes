namespace SisOdonto.Domain.DTO
{
    public class AspNetRoleClaimDTO
    {
        public int Id { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public string RoleId { get; set; } = null!;

        public virtual AspNetRoleDTO RoleDTO { get; set; } = null!;
    }
}
