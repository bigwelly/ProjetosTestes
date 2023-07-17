using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Context;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Infra.Data.Repository
{
    public class AspNetUserTokensRepository : BaseRepository<AspNetUserToken>, IAspNetUserTokensRepository
    {
        public AspNetUserTokensRepository(SisOdontoContext context) : base(context)
        {
        }

        public void Add(AspNetUserToken userTokens)
        {
            _context.Add(userTokens);
        }

        public void Remove(AspNetUserToken userTokens)
        {
            _context.Remove(userTokens);
        }

        public AspNetUserToken GetByToken(string token)
        {
            return _context.AspNetUserTokens.Where(t => t.Value == token).FirstOrDefault();
        }

        public AspNetUserToken GetByUserId(string userId)
        {
            return _context.AspNetUserTokens.Where(t => t.UserId == userId).FirstOrDefault();
        }

        public void Atualizar(AspNetUserToken userTokens)
        {
            _context.Update(userTokens);
        }
    }
}
