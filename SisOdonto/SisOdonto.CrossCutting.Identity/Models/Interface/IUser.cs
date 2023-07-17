using System.Security.Claims;

namespace SisOdonto.Infra.CrossCutting.Identity.Models.Interface
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
