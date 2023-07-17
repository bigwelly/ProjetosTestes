namespace SisOdonto.Domain.DTO
{
    public class AspNetUserLoginDTO
    {
        public string LoginProvider { get; set; } = null!;
        public string ProviderKey { get; set; } = null!;
        public string? ProviderDisplayName { get; set; }
        public string UserId { get; set; } = null!;

        public virtual AspNetUserDTO UserDTO { get; set; } = null!;
    }
}
