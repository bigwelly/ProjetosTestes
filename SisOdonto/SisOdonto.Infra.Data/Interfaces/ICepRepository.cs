using SisOdonto.Domain.Models;

namespace SisOdonto.Infra.Data.Interfaces
{
    public interface ICepRepository : IBaseRepository<Cep>
    {
        Cep GetByCodigo(int codigo);
    }
}
