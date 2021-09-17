using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class AuthenticatedUserObject
    {
        public AuthenticatedUserObject() : base()
        {
            UserName = "Not authorized";
            BearerToken = string.Empty;
            RefreshToken = string.Empty;
        }

        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public long Expires { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
