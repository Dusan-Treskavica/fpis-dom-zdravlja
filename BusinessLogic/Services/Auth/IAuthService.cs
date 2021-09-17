using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthenticatedUserObject> PasswordAuthentication(AuthRequestModel authRequestModel);
        Task<AuthenticatedUserObject> RefreshTokenAuthentication(AuthRequestModel authRequestModel);
    }
}
