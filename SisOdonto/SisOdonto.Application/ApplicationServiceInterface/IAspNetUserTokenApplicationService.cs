using SisOdonto.Domain.DTO;

namespace SisOdonto.Application.ApplicationServiceInterface
{
    public interface IAspNetUserTokenApplicationService : IBaseApplicationService
    {
        AspNetUserTokenDTO GetByToken(string token);
        AspNetUserTokenDTO GetByUserId(string userId);
        void Add(AspNetUserTokenDTO userTokens);
        void Update(AspNetUserTokenDTO userTokens);
        void Remove(AspNetUserTokenDTO userTokens);
    }
}
