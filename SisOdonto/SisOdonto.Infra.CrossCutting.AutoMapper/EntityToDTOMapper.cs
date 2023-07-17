using AutoMapper;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;

namespace SisOdonto.Infra.CrossCutting.AutoMapper
{
    public class EntityToDTOMapper : Profile
    {
        public EntityToDTOMapper()
        {
            CreateMap<AspNetUser, AspNetUserDTO>();
            CreateMap<AspNetUserToken, AspNetUserTokenDTO>();

        }
    }
}
