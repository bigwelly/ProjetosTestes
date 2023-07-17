using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;

namespace SisOdonto.Infra.Data.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        Cliente GetById(int id);
        List<Cliente> GetListByParameters(int? id, string? name);
        void AddCliente(Cliente cli);
        void DeleteCliente(Cliente cli);
        void UpdateCliente(Cliente cli);

    }
}
