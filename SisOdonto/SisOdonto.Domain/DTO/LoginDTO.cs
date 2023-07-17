using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace SisOdonto.Domain.DTO.Authenticate
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
