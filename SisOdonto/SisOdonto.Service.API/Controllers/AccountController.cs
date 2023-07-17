using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SisOdonto.Infra.CrossCutting.Identity.Models;
using SisOdonto.Infra.CrossCutting.SysMessage.Enumerate;
using SisOdonto.Infra.CrossCutting.SysMessage;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.DTO.Authenticate;
using SisOdonto.Infra.CrossCutting.Identity.IdentityViewModels;
using SisOdonto.Application.ApplicationServiceInterface;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SisOdonto.Service.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppJwtSettings _appJwtSettings;
        private readonly JWTSettings _jwtSettings;
        private readonly IAspNetUserTokenApplicationService _aspNetUserTokenApplicationService;
        private readonly IAspNetUserApplicationService _aspNetUserApplicationService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAspNetUserTokenApplicationService aspNetUserTokenApplicationService,
            IAspNetUserApplicationService aspNetUserApplicationService,
            IOptions<AppJwtSettings> appJwtSettings,
            IOptions<JWTSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appJwtSettings = appJwtSettings.Value;
            _jwtSettings = jwtSettings.Value;
            _aspNetUserTokenApplicationService = aspNetUserTokenApplicationService;
            _aspNetUserApplicationService = aspNetUserApplicationService;
        }

        //[HttpGet]
        ////[Route("getuser")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> GetUser(string id)
        //{
        //    RegisterViewModel model = new RegisterViewModel();

        //    var user = await _userManager.FindByIdAsync(id).ConfigureAwait(true);

        //    model.Id = id;
        //    model.Email = user.Email;
        //    model.Password = user.PasswordHash;
        //    model.Password = user.PasswordHash;
        //    model.ConfirmPassword = user.PasswordHash;

        //    var roles = await _userManager.GetRolesAsync(user);

        //    var roleName = roles.FirstOrDefault();

        //    var identityRole = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(true);

        //    model.RoledId = identityRole.Id;

        //    return StatusCode(StatusCodes.Status200OK, model);
        //}

        [HttpPost]
        //[Route("register")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao carregar dados do usuário.");

            var userExists = await _userManager.FindByEmailAsync(model.Email);  

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Já existe um usuário com esse login.");
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Nome, UsuarioAtivo = model.UsuarioAtivo };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var identityUser = await _userManager.FindByIdAsync(user.Id);

                var userRoles = await _userManager.GetRolesAsync(user);

                var role = await _roleManager.FindByIdAsync(model.RoleId);

                if ((userRoles == null) || (!userRoles.Any(userRoles => userRoles.Contains(role.Name))))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);

                    // User claim for write customers data
                    if (role.Name == "Admin")
                    {
                        await _userManager.AddClaimAsync(user, new Claim(role.Name, "Write")).ConfigureAwait(true);
                    }
                    else
                    {
                        await _userManager.AddClaimAsync(user, new Claim(role.Name, "Read")).ConfigureAwait(true);
                    }                   

                    return StatusCode(StatusCodes.Status200OK, "Usuário registrado com sucesso!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "O usuário já possui esse perfil associado a ele.");
                }
            }

            MessageRecord msg;
            MessageCollection messages = new MessageCollection();

            foreach (var error in result.Errors)
            {
                msg = new MessageRecord();
                msg.Description = error.Description;
                msg.Type = MessageType.ValidationError;

                messages.Add(msg);
            }

            return TreatResponseObject<object>(null, messages);
        }

        [AllowAnonymous]
        [HttpPost]
        //[Route("login")]
        public async Task<ObjectResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status500InternalServerError, "Login informado está inválido.");

            SignInResult result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Email);

                var userRoles = await _userManager.GetRolesAsync(user);

                var authenticatedResponse = CreateToken(user, (List<string>)userRoles);

                await _userManager.SetAuthenticationTokenAsync(user, "SisOdonto", "RefreshToken", authenticatedResponse.AccessToken);

                _aspNetUserApplicationService.UpdateLastAccess(user.Id);

                return StatusCode(StatusCodes.Status200OK, authenticatedResponse);
            }

            if (result.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status423Locked, "Usuário está bloqueado.");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Usuário e/ou senha incorreto.");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ObjectResult ListRoles()
        {
            var roles = _roleManager.Roles.ToList();

            return TreatResponseList(roles, null);
        }

        [HttpPost]
        //[Route("register")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao carregar dados do usuário.");

            var user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user != null)
            {
                user.Nome = model.Nome;
                user.UsuarioAtivo = model.UsuarioAtivo; 

                var identityUser = await _userManager.UpdateAsync(user);

                if (identityUser.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var role = await _roleManager.FindByIdAsync(model.RoleId);

                    if (userRoles.Count > 0)
                    {                        

                        if (userRoles.Any(o => o == role.Name))
                        {
                            return StatusCode(StatusCodes.Status200OK, "Usuário atualizado com sucesso!");
                        }
                        else
                        {
                            var claim = await _userManager.GetClaimsAsync(user);
                            var claimResult = await _userManager.RemoveClaimAsync(user, claim.FirstOrDefault());
                            var identityResult = await _userManager.RemoveFromRoleAsync(user, userRoles.FirstOrDefault());

                            if (identityResult.Succeeded)
                            {
                                var result = await _userManager.AddToRoleAsync(user, role.Name);

                                // User claim for write customers data
                                if (role.Name == "Admin")
                                {
                                    await _userManager.AddClaimAsync(user, new Claim(role.Name, "Write")).ConfigureAwait(true);
                                }
                                else
                                {
                                    await _userManager.AddClaimAsync(user, new Claim(role.Name, "Read")).ConfigureAwait(true);
                                }

                                return StatusCode(StatusCodes.Status200OK, "Usuário alterado com sucesso!");
                            }
                        }
                    }
                    else
                    {
                        var result = await _userManager.AddToRoleAsync(user, role.Name);

                        // User claim for write customers data
                        if (role.Name == "Admin")
                        {
                            await _userManager.AddClaimAsync(user, new Claim(role.Name, "Write")).ConfigureAwait(true);
                        }
                        else
                        {
                            await _userManager.AddClaimAsync(user, new Claim(role.Name, "Read")).ConfigureAwait(true);
                        }

                        return StatusCode(StatusCodes.Status200OK, "Usuário alterado com sucesso!");
                    }
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Houve um erro ao atualizar os dados do usuário!");

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ObjectResult GetUsers()
        {
            var users = _aspNetUserApplicationService.GetAllUsers();

            return TreatResponseList(users, null);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name).ConfigureAwait(true);
                if (!roleExist)
                {
                    var user = new IdentityRole
                    {
                        Name = model.Name,
                        NormalizedName = model.NormalizedName
                    };

                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(model.Name)).ConfigureAwait(true);

                    if (result.Succeeded)
                    {
                        return StatusCode(StatusCodes.Status200OK, "Perfil criado com sucesso!");
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar um novo perfil.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Esse perfil já existe.");
                }
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Informações do perfil inválidas.");
        }

        /// <summary>
        /// Realiza a autenticação do usuário via refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ObjectResult> RefreshToken(RefreshTokenParameterDTO refreshToken)
        {
            if (String.IsNullOrWhiteSpace(refreshToken.RefreshToken))
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Sessão Expirada.");
            }

            var authenticatedResponseDTO = await UpdateRefreshToken(refreshToken.RefreshToken);

            if (authenticatedResponseDTO != null)
            {
                return StatusCode(StatusCodes.Status200OK, authenticatedResponseDTO);
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Sessão Expirada.");
            }
        }

        /// <summary>
        /// Realiza logout
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>http://getbootstrap.com.br/
        [AllowAnonymous]
        [HttpPost]
        //[Route("logout")]
        public async Task<ObjectResult> Logout(RefreshTokenParameterDTO refreshToken)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(refreshToken.RefreshToken))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Sessão Expirada.");
                }

                AspNetUserTokenDTO userToken = _aspNetUserTokenApplicationService.GetByToken(refreshToken.RefreshToken);

                ApplicationUser user = await _userManager.FindByIdAsync(userToken.UserId);

                await _userManager.RemoveAuthenticationTokenAsync(user, "SisOdonto", refreshToken.RefreshToken);

                await _signInManager.SignOutAsync().ConfigureAwait(true);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao realizar logout.");
            }

            return StatusCode(StatusCodes.Status200OK, null);
        }

        private AuthenticatedResponseDTO CreateToken(ApplicationUser userAccessControl, List<string> roles)
        {
            var refreshToken = GetRefreshToken(userAccessControl);

            //if (!String.IsNullOrWhiteSpace(refreshToken))
            //{
                return FeedAuthenticatedResponseData(userAccessControl, refreshToken, roles);
            //}
            //else
            //{
            //    return null;
            //}
        }

        private async Task<AuthenticatedResponseDTO> UpdateRefreshToken(string refreshToken)
        {
            AspNetUserTokenDTO userToken = _aspNetUserTokenApplicationService.GetByToken(refreshToken);

            ApplicationUser user = await _userManager.FindByIdAsync(userToken.UserId);

            var userRoles = await _userManager.GetRolesAsync(user);

            var newRefreshToken = GetRefreshToken(user);

            if (!String.IsNullOrWhiteSpace(newRefreshToken))
            {
                return FeedAuthenticatedResponseData(user, newRefreshToken, (List<string>)userRoles);
            }
            else
            {
                return null;
            }
        }

        private AuthenticatedResponseDTO FeedAuthenticatedResponseData(ApplicationUser userAccessControl, string? refreshToken, List<string> roles)
        {
            return new AuthenticatedResponseDTO()
            {
                AccessToken = CreateAccessToken(userAccessControl, roles),
                RefreshToken = (refreshToken == null ? CreateAccessToken(userAccessControl, roles) : refreshToken),
                UserID = userAccessControl.Id,
                Login = userAccessControl.Email,
                Name = userAccessControl.UserName,
                Email = userAccessControl.Email,
                Profiles = String.Join(',', roles.Select(o => o.ToLower())),
                Resources = roles.Select(o => o.ToLower()).ToList(),
                UsuarioAtivo = userAccessControl.UsuarioAtivo
            };
        }

        private string CreateAccessToken(IdentityUser userAccessControl, List<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("fmFGn5agHZkuG2N0e1zaEJIQtGVoNN5P");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, String.Join(',', roles.Select(o => o.ToLower()))),
                    new Claim("UserId", userAccessControl.Id.ToString()),
                    new Claim("Username", userAccessControl.Email),
                    new Claim("UserEmail", userAccessControl.Email)
                }),
                Issuer = _appJwtSettings.Issuer,
                Audience = _appJwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        private string? GetRefreshToken(ApplicationUser userAccessControl)
        {
            if (userAccessControl?.LastAccess != null)
            {
                var date = (DateTime)userAccessControl.LastAccess;

                if (date < date.AddHours(1))
                {
                    //Criar um método para alterar apenas o token, mantendo a data do ultimo acesso.
                    //para assim, aumetar a rotatividade do token, caso alguém o roube
                    AspNetUserTokenDTO userToken = _aspNetUserTokenApplicationService.GetByUserId(userAccessControl.Id);
                    if (userToken != null)
                    {
                        return userToken.Value;
                    }                    
                }
            }

            return null;
        }

        private bool IsLockedOut(IdentityUser user)
        {
            return !(user.AccessFailedCount <= 3);
        }

        //private string GetFullJwt(string email)
        //{
        //    return new JwtBuilder()
        //        .WithUserManager(_userManager)
        //        .WithJwtSettings(_appJwtSettings)
        //        .WithEmail(email)
        //        .WithJwtClaims()
        //        .WithUserClaims()
        //        .WithUserRoles()
        //        .BuildToken();
        //}
    }
}

