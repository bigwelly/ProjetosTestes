using AutoMapper;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Application.ApplicationServiceRepository
{
    public class ClienteApplicationService : BaseApplicationService, IClienteApplicationService
    {
        private readonly IClienteRepository _clienteRepository;
        private string message = string.Empty;

        public ClienteApplicationService(IClienteRepository clienteRepository,
                                         IMapper mapper) : base(mapper)
        {
            _clienteRepository = clienteRepository;
        }

        public void AddCliente(ClienteDTO cli)
        {
            throw new NotImplementedException();
        }

        public void DeleteCliente(ClienteDTO cli)
        {
            throw new NotImplementedException();
        }

        public ClienteDTO GetById(int id)
        {
            ClienteDTO cliente = new ClienteDTO();

            var cli = _clienteRepository.GetById(id);
            //return ConvertToDTO<Cliente, ClienteDTO>(cli);

            cliente.Id = cli.Id;
            cliente.Nome = cli.Nome;
            cliente.DataNascimento = cli.DataNascimento.ToString("dd/MM/yyyy");
            cliente.IdcSexo = cli.IdcSexo;
            cliente.Cpf = cli.Cpf;
            cliente.Rg = cli.Rg;
            cliente.CodigoCep = cli.CodigoCep;
            cliente.Numero = cli.Numero;
            cliente.Complemento = cli.Complemento;
            cliente.DddTelefone = cli.DddTelefone;
            cliente.NumTelefone = cli.NumTelefone;
            cliente.DddCelular = cli.DddCelular;
            cliente.NumCelular = cli.NumCelular;
            cliente.Email = cli.Email;

            return cliente;
        }

        public List<ClienteDTO> GetListByParameters(int? id, string? name)
        {
            List<ClienteDTO> lstcliente= new List<ClienteDTO>();
            ClienteDTO cliente;

            var clientes = _clienteRepository.GetListByParameters(id, name);
            
            foreach(var cli in clientes)
            {
                cliente = new ClienteDTO();

                cliente.Id = cli.Id;
                cliente.Nome = cli.Nome;    
                cliente.DataNascimento = cli.DataNascimento.ToString("dd/MM/yyyy");
                cliente.IdcSexo = cli.IdcSexo;
                cliente.Cpf = cli.Cpf;
                cliente.Rg = cli.Rg;
                cliente.CodigoCep = cli.CodigoCep;
                cliente.Numero = cli.Numero;
                cliente.Complemento = cli.Complemento;
                cliente.DddTelefone = cli.DddTelefone;
                cliente.NumTelefone = cli.NumTelefone;
                cliente.DddCelular = cli.DddCelular;
                cliente.NumCelular = cli.NumCelular;
                cliente.Email = cli.Email;

                lstcliente.Add(cliente);

            }

            return lstcliente;

        }

        public void UpdateCliente(ClienteDTO cli)
        {
            throw new NotImplementedException();
        }
    }
}
