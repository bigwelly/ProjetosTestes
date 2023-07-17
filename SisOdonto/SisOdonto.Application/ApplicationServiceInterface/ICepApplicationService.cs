using SisOdonto.Domain.DTO;

namespace SisOdonto.Application.ApplicationServiceInterface
{
    public interface ICepApplicationService : IBaseApplicationService
    {
        CepDTO GetByCodigo(int codigo);
    }
}
