using System.ComponentModel.DataAnnotations;

namespace SisOdonto.Infra.CrossCutting.Identity.IdentityViewModels
{
    public class RegisterViewModel
    {
        //public string Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(40, ErrorMessage = "A {0} precisa ter no mínimo {2} e no máximo {1} caracter.", MinimumLength = 7)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "A {0} precisa ter no mínimo {2} e no máximo {1} caracter.", MinimumLength = 3)]
        [Display(Name = "Nome Usuário")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} precisa ter no mínimo {2} e no máximo {1} caracter.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma Sennha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação da senha são diferentes.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Usuário Ativo")]
        public bool UsuarioAtivo { get; set; }

        public string RoleId { get; set; }

        //[Display(Name = "Perfil")]
        //public IdentityRole IdentityRole { get; set; }
    }
}
