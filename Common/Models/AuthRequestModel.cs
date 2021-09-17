using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class AuthRequestModel
    {
        public string GrantType { get; set; } // password or refresh_token
        public string Username { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }
    }
}
