using SisOdonto.Domain.DTO;

namespace SisOdonto.Application.ApplicationServiceInterface
{
    public interface IClienteApplicationService : IBaseApplicationService
    {
        ClienteDTO GetById(int id);
        List<ClienteDTO> GetListByParameters(int? id, string? name);
        void AddCliente(ClienteDTO cli);
        void DeleteCliente(ClienteDTO cli);
        void UpdateCliente(ClienteDTO cli);
    }
}
