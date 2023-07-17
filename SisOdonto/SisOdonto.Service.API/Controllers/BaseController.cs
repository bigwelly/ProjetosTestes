using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SisOdonto.Infra.CrossCutting.SysMessage.Enumerate;
using SisOdonto.Infra.CrossCutting.SysMessage;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SisOdonto.Infra.CrossCutting.Identity;
using AutoMapper;
using SisOdonto.Infra.CrossCutting.Identity.Models;
using SisOdonto.Domain.DTO;

namespace SisOdonto.Service.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[Action]")]
    [ApiController]

    public class BaseController : Controller
    {
        public ObjectResult TreatResponseObject<T>(T data, MessageCollection messages)
        {
            var response = new Response<T>();

            if (data != null) response.Data.Add(data);

            return TreatResponse(response, messages);
        }

        public ObjectResult TreatResponseList<T>(IEnumerable<T> data, MessageCollection messages)
        {
            var response = new Response<T>();

            if (data != null) response.Data.AddRange(data);

            return TreatResponse(response, messages);
        }

        private ObjectResult TreatResponse<T>(Response<T> response, MessageCollection messages)
        {
            response.MessageList.Copy(messages);

            var statusCode = StatusCodes.Status200OK;
            if (response != null)
            {
                response.ResponseType = ResponseType.Success;

                if (response.MessageList.HasSystemError())
                {
                    statusCode = StatusCodes.Status500InternalServerError;
                    response.ResponseType = ResponseType.SystemError;
                }
                else if (response.MessageList.HasValidationError())
                {
                    statusCode = StatusCodes.Status400BadRequest;
                    response.ResponseType = ResponseType.BusinessError;
                }
            }

            return StatusCode(statusCode, response);
        }

        protected bool ValidateCurrentToken(string token)
        {
            var mySecret = "iWnZ!u4Nakfk8j37ssj3820&#%hjcdk@!fggcaEVFzxcTFS42dsW@!dd3";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "SisOdonto";
            var myAudience = "http://localhost";

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected TokenInformation ExtractTokenInformation()
        {
            var headers = Request.Headers;
            var token = headers["Authorization"].ToString();

            var indexOfSpace = token.IndexOf(" ") + 1;
            var jwt = token.Substring(indexOfSpace, token.Length - indexOfSpace);

            var isTokenValid = ValidateCurrentToken(jwt);

            if (isTokenValid)
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenRead = handler.ReadJwtToken(jwt);

                var tokenInformation = new TokenInformation
                {
                    UserName = tokenRead.Claims.FirstOrDefault(c => c.Type == "Username")?.Value,
                    UserEmail = tokenRead.Claims.FirstOrDefault(c => c.Type == "UserEmail")?.Value
                };

                return tokenInformation;
            }
            else
            {
                return null;
            }
        }
    }
}
