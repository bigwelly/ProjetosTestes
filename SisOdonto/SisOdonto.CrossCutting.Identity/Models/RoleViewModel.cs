using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SisOdonto.Infra.CrossCutting.Identity.IdentityViewModels
{
    public  class RoleViewModel
    {

        [Required(ErrorMessage = "O Nome do Perfil é Obrigatório.")]
        [MinLength(6)]
        [MaxLength(30)]
        [DisplayName("Nome do Perfil")]
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
