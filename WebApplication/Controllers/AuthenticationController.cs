using BusinessLogic.Services.Auth;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Route("api/domzdravlja/v1/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService authService;

        public AuthenticationController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequestModel authRequestModel)
        {
            if (authRequestModel == null)
            {
                return new StatusCodeResult(500);
            }
            switch (authRequestModel.GrantType)
            {
                case "password":
                    AuthenticatedUserObject authUserObjectPassword = await this.authService.PasswordAuthentication(authRequestModel);
                    return this.CreateAuthenticationResponse(authUserObjectPassword);
                case "refresh_token":
                    AuthenticatedUserObject authUserObjectRefreshToken = await this.authService.RefreshTokenAuthentication(authRequestModel);
                    return this.CreateAuthenticationResponse(authUserObjectRefreshToken);
                default:
                    return Unauthorized(new ApiResponse { HttpStatusCode = HTTPResponseCodes.UNAUTHORIZED, Message = "Korisnik nije autorizovan.", Success = false });
            }
            
        }

        private IActionResult CreateAuthenticationResponse(AuthenticatedUserObject authUserObject)
        {
            if (authUserObject.IsAuthenticated)
            {
                return Ok(new ApiResponse
                {
                    HttpStatusCode = HTTPResponseCodes.OK,
                    Success = true,
                    Data = authUserObject
                });
            }
            else
            {
                return Unauthorized(new ApiResponse
                {
                    HttpStatusCode = HTTPResponseCodes.OK,
                    Success = false,
                    Message = "Korisnik nije autorizovan.",
                    Data = authUserObject
                });
            }
        }

        //private async Task<IActionResult> PasswordAuthentication(AuthRequestModel authRequestModel)
        //{
        //    AuthenticatedUserObject userObject = new AuthenticatedUserObject();
        //    var user = await userManager.FindByNameAsync(authRequestModel.Username);
        //    if (user != null && await userManager.CheckPasswordAsync(user, authRequestModel.Password))
        //    {
        //        var userRoles = await userManager.GetRolesAsync(user);
        //        List<Claim> authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        }

        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Key));
        //        JwtSecurityToken jwtToken = new JwtSecurityToken(
        //            issuer: this.jwtSettings.Issuer,
        //            audience: this.jwtSettings.Audience,
        //            claims: authClaims,
        //            expires: DateTime.Now.AddMinutes(5),
        //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        //        userObject.UserName = authRequestModel.Username;
        //        userObject.BearerToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        //        userObject.Expires = (DateTime.Now.AddHours(3) - DateTime.Now).Ticks;
        //        userObject.IsAuthenticated = true;
        //        userObject.Claims = authClaims;
        //        return Ok(new ApiResponse
        //        {
        //            HttpStatusCode = HTTPResponseCodes.OK,
        //            Success = true,
        //            Data = userObject
        //        });
        //    }
        //    return Json(new ApiResponse
        //    {
        //        HttpStatusCode = HTTPResponseCodes.NOT_FOUND,
        //        Success = false,
        //        Message = "Username i Password nisu ispravni."
        //    });
        //}

        //private async Task<IActionResult> RefreshTokenAuthentication(AuthRequestModel authRequestModel)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
