using AutoMapper;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Application.ApplicationServiceRepository
{
    public class AspNetUserTokenApplicationService : BaseApplicationService, IAspNetUserTokenApplicationService
    {
        private readonly IAspNetUserTokensRepository _aspNetUserTokenRepository;
        private string message = string.Empty;

        public AspNetUserTokenApplicationService(IAspNetUserTokensRepository aspNetUserTokenRepository,
                                                 IMapper mapper) : base(mapper)
        {
            _aspNetUserTokenRepository = aspNetUserTokenRepository;
        }

        public void Add(AspNetUserTokenDTO userTokens)
        {
            try
            {
                AspNetUserTokenDTO dto = userTokens;
                var userToken = TreatResult<AspNetUserTokenDTO, AspNetUserToken>(dto);

                if (userToken != null)
                {
                    if (userToken.UserId == null)
                    {
                        message = "O campo UserID não informado.";
                        Messages.AddSystemError(message);
                    }
                    else if (userToken.LoginProvider == null)
                    {
                        message = "O campo LoginProvider não informado.";
                        Messages.AddSystemError(message);
                    }
                    else if (userToken.Name == null)
                    {
                        message = "O campo Name não informado.";
                        Messages.AddSystemError(message);
                    }
                    else if (userToken.Value == null)
                    {
                        message = "O campo Value não informado.";
                        Messages.AddSystemError(message);
                    }
                    else
                    {
                        _aspNetUserTokenRepository.Add(userToken);
                    }
                }
            } 
            catch (Exception e)
            {
                message = "Erro ao gravar o Token do usuário. Erro: " + e.Message;
            }
        }

        public AspNetUserTokenDTO GetByToken(string token)
        {
            var userToken = _aspNetUserTokenRepository.GetByToken(token);
            return ConvertToDTO<AspNetUserToken,AspNetUserTokenDTO>(userToken); 
        }

        public AspNetUserTokenDTO GetByUserId(string userId)
        {
            var userToken = _aspNetUserTokenRepository.GetByUserId(userId);
            return ConvertToDTO<AspNetUserToken, AspNetUserTokenDTO>(userToken);
        }

        public void Remove(AspNetUserTokenDTO userTokens)
        {
            try
            {
                AspNetUserTokenDTO dto = userTokens;
                var userToken = TreatResult<AspNetUserTokenDTO, AspNetUserToken>(dto);

                if (userToken != null)
                {
                    _aspNetUserTokenRepository.Delete(userToken);
                }
            }
            catch (Exception e)
            {
                message = "Erro ao excluir o Token do usuário. Erro: " + e.Message;
            }
        }

        public void Update(AspNetUserTokenDTO userTokens)
        {
            try
            {
                AspNetUserTokenDTO dto = userTokens;
                var userToken = TreatResult<AspNetUserTokenDTO, AspNetUserToken>(dto);

                if (userToken != null)
                {
                    if (userToken.UserId == null)
                    {
                        message = "O campo UserID não informado.";
                        Messages.AddSystemError(message);
                    }
                    else if (userToken.LoginProvider == null)
                    {
                        message = "O campo LoginProvider não informado.";
                        Messages.AddSystemError(message);
                    }
                    else if (userToken.Name == null)
                    {
                        message = "O campo Name não informado.";
                        Messages.AddSystemError(message);
                    }
                    else if (userToken.Value == null)
                    {
                        message = "O campo Value não informado.";
                        Messages.AddSystemError(message);
                    }
                    else
                    {
                        _aspNetUserTokenRepository.Atualizar(userToken);
                    }
                }
            }
            catch (Exception e)
            {
                message = "Erro ao atualizar o Token do usuário. Erro: " + e.Message;
            }
        }
    }
}
