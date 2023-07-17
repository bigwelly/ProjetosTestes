using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;

namespace SisOdonto.Infra.Data.Interfaces
{
    public interface IAspNetUserRepository : IBaseRepository<AspNetUser>
    {
        void UpdateLastAccess(AspNetUser user);
        AspNetUser GetByUserId (string userId);

        List<AspNetUserListDTO> GetAspNetUserList();
    }
}
