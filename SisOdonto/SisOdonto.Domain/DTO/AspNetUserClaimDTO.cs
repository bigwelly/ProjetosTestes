namespace SisOdonto.Domain.DTO
{
    public class AspNetUserClaimDTO
    {
        public int Id { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
        public string UserId { get; set; } = null!;

        public virtual AspNetUserDTO UserDTO { get; set; } = null!;
    }
}
