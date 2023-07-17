using Microsoft.EntityFrameworkCore;
using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Context;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Infra.Data.Repository
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(SisOdontoContext context) : base(context)
        {
        }

        public void AddCliente(Cliente cli)
        {
            _context.Add(cli); 
        }

        public void DeleteCliente(Cliente cli)
        {
            _context.Remove(cli);
        }

        public Cliente GetById(int id)
        {
            return _context.Set<Cliente>()
                   .Include(x => x.Agendamentos)
                   .Include(x => x.Orcamentos)
                   .Include(x => x.Tratamentos)
                   .Where(x => x.Id == id)
                   .FirstOrDefault();

        }

        public List<Cliente> GetListByParameters(int? id, string? name)
        {
            return _context.Set<Cliente>()
                   ?.Include(x => x.Agendamentos)
                   ?.Include(x => x.Orcamentos)
                   ?.Include(x => x.Tratamentos)
                   .Where(x => (id == 0 ? true : x.Id == id) &&
                               (name == null ? true : x.Nome.Contains(name))).ToList();
                               
        }

        public void UpdateCliente(Cliente cli)
        {
            _context.Update(cli);
        }
    }
}
