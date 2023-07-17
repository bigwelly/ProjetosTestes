using AutoMapper;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Domain.DTO;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Application.ApplicationServiceRepository
{
    public class AspNetUserApplicationService : BaseApplicationService, IAspNetUserApplicationService
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private string message = string.Empty;

        public AspNetUserApplicationService(IAspNetUserRepository aspNetUserRepository,
                                                 IMapper mapper) : base(mapper)
        {
            _aspNetUserRepository = aspNetUserRepository;
        }

        public List<AspNetUserListDTO> GetAllUsers()
        {
            return _aspNetUserRepository.GetAspNetUserList();
        }

        public void UpdateLastAccess(string userId)
        {
            try
            {                 
                var _user = _aspNetUserRepository.GetByUserId(userId);

                if (_user != null)
                {
                    _user.LastAccess = DateTime.Now;
                    _aspNetUserRepository.Update(_user);    
                }
            }
            catch (Exception e)
            {
                message = "Erro ao atualizar o campo LastAccess do usuário. Erro: " + e.Message;
            }
        }
    }
}
