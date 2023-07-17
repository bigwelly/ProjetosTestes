using AutoMapper;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Application.ApplicationServiceRepository
{
    public class CepApplicationService : BaseApplicationService, ICepApplicationService
    {
        private readonly ICepRepository _cepRepository;
        private string message = string.Empty;

        public CepApplicationService(ICepRepository cepRepository,
                                         IMapper mapper) : base(mapper)
        {
            _cepRepository = cepRepository;
        }

        public CepDTO GetByCodigo(int codigo)
        {
            var cep = _cepRepository.GetByCodigo(codigo);

            var _cep = new CepDTO();

            if (cep != null)
            {
                _cep.Codigo = cep.Codigo;
                _cep.Uf = cep.Uf;
                _cep.Municipio = cep.Municipio;
                _cep.Bairro = cep.Bairro;
                _cep.Logradouro = cep.Logradouro;
            }

            return _cep;
        }
    }
}
