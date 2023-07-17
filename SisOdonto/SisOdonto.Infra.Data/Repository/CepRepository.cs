using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Context;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Infra.Data.Repository
{
    public class CepRepository : BaseRepository<Cep>, ICepRepository
    {
        public CepRepository(SisOdontoContext context) : base(context)
        {
        }

        public Cep GetByCodigo(int codigo)
        {
            return _context.Ceps.Where(c => c.Codigo == codigo).FirstOrDefault();
        }
    }
}
