using SisOdonto.Domain.DTO;

namespace SisOdonto.Application.ApplicationServiceInterface
{
    public interface IAspNetUserApplicationService : IBaseApplicationService
    {
        void UpdateLastAccess(string userId);

        List<AspNetUserListDTO> GetAllUsers();
    }
}
