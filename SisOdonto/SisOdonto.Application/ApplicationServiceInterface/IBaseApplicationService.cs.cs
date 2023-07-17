using SisOdonto.Infra.CrossCutting.SysMessage;

namespace SisOdonto.Application.ApplicationServiceInterface
{
    public  interface IBaseApplicationService
    {
        MessageCollection Messages { get; set; }
    }
}
