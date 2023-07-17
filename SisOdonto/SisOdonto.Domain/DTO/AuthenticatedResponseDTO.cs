using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisOdonto.Domain.DTO
{
    public  class AuthenticatedResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UserID { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Profiles { get; set; }
        public List<string> Resources { get; set; }
        public string Email { get; set; }
        public bool UsuarioAtivo { get; set; }
    }
}
