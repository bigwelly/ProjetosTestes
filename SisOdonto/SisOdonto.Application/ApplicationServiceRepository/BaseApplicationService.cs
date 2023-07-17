using AutoMapper;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Infra.CrossCutting.Extension.Paging;
using SisOdonto.Infra.CrossCutting.SysMessage;
using SisOdonto.Infra.CrossCutting.SysMessage.Enumerate;
using System.Collections;

namespace SisOdonto.Application.ApplicationServiceRepository
{
    public abstract class BaseApplicationService : BasePaging, IBaseApplicationService
    {
        public MessageCollection Messages { get; set; }
        protected IMapper Mapper { get; set; }

        public BaseApplicationService(IMapper maper)
        {
            Mapper = maper;
            Messages = new MessageCollection();
        }

        protected void ValidateEmpty<T>(T obj, MessageType messageType = MessageType.Warning)
        {
            bool isEmpty = false;

            if (obj is IList)
            {
                isEmpty = !(obj != null && ((IList)obj).Count > 0);
            }
            else
            {
                isEmpty = (obj == null);
            }

            if (isEmpty)
            {
                Messages.AddMessage("Registro não encontrado!", messageType);
            }
        }


        protected List<TDestiny> ConvertToDTOList<TSource, TDestiny>(List<TSource> source)
        {
            return Mapper.Map<List<TSource>, List<TDestiny>>(source);
        }

        protected TDestiny ConvertToDTO<TSource, TDestiny>(TSource source)
        {
            return Mapper.Map<TDestiny>(source);
        }

        protected TDestinyDTO TreatResult<TSource, TDestinyDTO>(TSource entity, bool validateEmpty = true)
        {
            if (validateEmpty)
            {
                ValidateEmpty(entity);
            }

            return ConvertToDTO<TSource, TDestinyDTO>(entity);
        }

        protected IList<TDestinyDTO> TreatResultList<TSource, TDestinyDTO>(List<TSource> entity, bool validateEmpty = true)
        {
            if (validateEmpty)
            {
                ValidateEmpty(entity);
            }

            return typeof(TSource) != typeof(TDestinyDTO) ? ConvertToDTOList<TSource, TDestinyDTO>(entity) : (IList<TDestinyDTO>)entity;
        }
    }
}
