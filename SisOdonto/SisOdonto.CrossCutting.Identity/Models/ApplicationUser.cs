using Microsoft.AspNetCore.Identity;

namespace SisOdonto.Infra.CrossCutting.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public bool UsuarioAtivo { get; set; }
        public DateTime? LastAccess { get; set; }
    }
}
