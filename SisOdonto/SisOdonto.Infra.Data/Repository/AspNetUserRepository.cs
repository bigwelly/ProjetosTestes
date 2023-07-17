using Microsoft.EntityFrameworkCore;
using SisOdonto.Domain.DTO;
using SisOdonto.Domain.Models;
using SisOdonto.Infra.Data.Context;
using SisOdonto.Infra.Data.Interfaces;

namespace SisOdonto.Infra.Data.Repository
{
    public class AspNetUserRepository : BaseRepository<AspNetUser>, IAspNetUserRepository
    {
        public AspNetUserRepository(SisOdontoContext context) : base(context)
        {
        }

        public void Add(AspNetUser userTokens)
        {
            _context.Add(userTokens);
        }

        public List<AspNetUserListDTO> GetAspNetUserList()
        {
            List<AspNetUserListDTO> lstUsers = new List<AspNetUserListDTO>();
            AspNetUserListDTO userList;

            var users = _context.AspNetUsers
                        .Include("Roles").ToList();

            foreach(var iten in users)
            {
                userList = new AspNetUserListDTO();
                userList.Id = iten.Id;
                userList.UserName = iten.UserName;
                userList.Nome = iten.Nome;
                userList.PassWord = iten.PasswordHash;
                userList.ConfirmPassWord = iten.PasswordHash;
                userList.UsuarioAtivo = (bool)iten.UsuarioAtivo == true? "Sim":"Não";
                if (iten.Roles.Count > 0)
                {
                    userList.IdRole = iten.Roles.FirstOrDefault().Id == null ? null : iten.Roles.FirstOrDefault().Id;
                    userList.NameRole = iten.Roles.FirstOrDefault().Name == null ? null : iten.Roles.FirstOrDefault().Name;
                }

                lstUsers.Add(userList);
            }

            return lstUsers;
        }

        public AspNetUser GetByUserId(string userId)
        {
            return FindById(userId);
        }

        public void UpdateLastAccess(AspNetUser user)
        {
            _context.Update(user);
        }
    }
}
