using AutoMapper;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;

namespace SisOdonto.Infra.CrossCutting.AutoMapper
{
    public class DTOToEntityMapper : Profile
    {
        public DTOToEntityMapper()
        {
            CreateMap<AspNetUserTokenDTO, AspNetUserToken>();
            CreateMap<AspNetUserDTO, AspNetUser>();

        }
    }
}
