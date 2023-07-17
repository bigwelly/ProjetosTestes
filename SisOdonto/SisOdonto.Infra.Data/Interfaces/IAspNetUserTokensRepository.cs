using SisOdonto.Domain.Models;

namespace SisOdonto.Infra.Data.Interfaces
{
    public interface IAspNetUserTokensRepository : IBaseRepository<AspNetUserToken>
    {
        AspNetUserToken GetByToken(string token);
        AspNetUserToken GetByUserId(string userId);
        void Add(AspNetUserToken userTokens);
        void Atualizar(AspNetUserToken userTokens); 
        void Remove(AspNetUserToken userTokens);
    }
}
