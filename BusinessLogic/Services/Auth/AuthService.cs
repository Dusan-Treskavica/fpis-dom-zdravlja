using Common.Models;
using DataAccess.DB;
using DataAccess.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IDBBroker dbBroker;
        private readonly JwtSettings jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IDBBroker dbBroker, JwtSettings jwtSettings)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dbBroker = dbBroker;
            this.jwtSettings = jwtSettings;
        }


        public async Task<AuthenticatedUserObject> PasswordAuthentication(AuthRequestModel authRequestModel)
        {
            AuthenticatedUserObject userObject = new AuthenticatedUserObject();
            var user = await userManager.FindByNameAsync(authRequestModel.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, authRequestModel.Password))
            {
                RefreshToken refreshToken = this.KreirajNoviRefreshToken(user.Id);

                await this.dbBroker.DodajNoviRefreshTokenIObrisiStareAsync(refreshToken);

                userObject = await this.KreirajNoviJwtAccessToken(user, refreshToken.Value);                
            }
            return userObject;
        }


        public async Task<AuthenticatedUserObject> RefreshTokenAuthentication(AuthRequestModel authRequestModel)
        {
            AuthenticatedUserObject userObject = new AuthenticatedUserObject();

            // check if the received refreshToken exists
            RefreshToken refreshTokenDb = this.dbBroker.VratiRefreshToken(authRequestModel.RefreshToken);
            if(refreshTokenDb != null)
            {
                // check if refresh token is expired
                if (refreshTokenDb.ExpiryTime < DateTime.UtcNow)
                {
                    return userObject;
                }

                var user = await this.userManager.FindByIdAsync(refreshTokenDb.UserId);
                if (user == null)
                {
                    // UserId not found or invalid
                    return userObject;
                }

                RefreshToken noviRefreshToken = this.KreirajNoviRefreshToken(refreshTokenDb.UserId);
                await this.dbBroker.ObrisiStariIDodajNoviToken(refreshTokenDb, noviRefreshToken);

                userObject = await this.KreirajNoviJwtAccessToken(user, noviRefreshToken.Value);
            }
            return userObject;
        }

        private RefreshToken KreirajNoviRefreshToken(string userId)
        {
            return new RefreshToken()
            {
                UserId = userId,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddMinutes(this.jwtSettings.RefreshTokenExpiresIn)
            };
        }

        private async Task<AuthenticatedUserObject> KreirajNoviJwtAccessToken(ApplicationUser user, string refreshToken)
        {
            var userRoles = await userManager.GetRolesAsync(user);
            List<Claim> authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Key));
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: this.jwtSettings.Issuer,
                audience: this.jwtSettings.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new AuthenticatedUserObject
            { 
                UserName = user.UserName,
                BearerToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken,
                Expires = (DateTime.Now.AddHours(3) - DateTime.Now).Ticks,
                IsAuthenticated = true,
                Claims = authClaims
            };
        }
    }
}
