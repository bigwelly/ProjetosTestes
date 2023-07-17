using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Domain.CommandsParameters;
using System.Data;

namespace SisOdonto.Service.API.Controllers
{
    public class BasicRecordsController : BaseController
    {
        private readonly IClienteApplicationService _clienteApplicationService;
        private readonly ICepApplicationService _cepApplicationService;

        public BasicRecordsController(IClienteApplicationService clienteApplicationService,
                                      ICepApplicationService cepApplicationService)
        {
            _clienteApplicationService = clienteApplicationService;
            _cepApplicationService = cepApplicationService;
        }

        [HttpPost]
        //[Route("{id}/{name?}")]
        [Authorize(Roles = "admin, employee")]
        public ObjectResult GetListByParameters(ListCostumersParameters param)
        {
            var clientes = _clienteApplicationService.GetListByParameters(param.id, param.name);

            return TreatResponseList(clientes, _clienteApplicationService.Messages);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "admin, employee")]
        public ActionResult GetById(int id)
        {
            var cliente = _clienteApplicationService.GetById(id);

            return TreatResponseObject(cliente, _clienteApplicationService.Messages);
        }

        [HttpPost]
        //[Route("{codigo}")]
        [Authorize(Roles = "admin, employee")]
        public ObjectResult GetCepByCodigo(GetCepParameters param)
        {
            var cep = _cepApplicationService.GetByCodigo(param.Codigo);

            return TreatResponseObject(cep, _cepApplicationService.Messages);
        }
    }
}
