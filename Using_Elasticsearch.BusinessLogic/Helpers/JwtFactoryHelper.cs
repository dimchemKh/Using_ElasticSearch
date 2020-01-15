using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.Common.Configs;
using Using_Elasticsearch.Common.Constants;
using Using_Elasticsearch.Common.Exceptions;
using Using_Elasticsearch.Common.Views.Authentification.Response;
using Using_Elasticsearch.DataAccess.Entities;

namespace Using_Elasticsearch.BusinessLogic.Helpers
{
    public class JwtFactoryHelper : IJwtFactoryHelper
    {
        private readonly IOptions<JwtConfig> _jwtConfig;
        private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha256;

        public JwtFactoryHelper(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }
        public string ValidateToken(string token)
        {
            var refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            if (refreshToken.ValidTo < DateTime.UtcNow)
            {
                throw new ProjectException(StatusCodes.Status401Unauthorized);
            }

            var email = refreshToken.Claims.First(x => x.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                throw new ProjectException(StatusCodes.Status404NotFound, Messages.UserNotFound);
            }

            return email;
        }
        public ResponseGenerateAuthentificationView Generate(ApplicationUser user, IEnumerable<UserPermission> permissions)
        {
            var result = new ResponseGenerateAuthentificationView();
            
            var accessClaims = GetAccessTokenClaims(user, permissions);

            var refreshClaims = GetRefreshTokenClaims(user);

            result.AccessToken = Generate(accessClaims, _jwtConfig.Value.AccessTokenExpiration);

            result.RefreshToken = Generate(refreshClaims, _jwtConfig.Value.RefreshTokenExpiration);

            return result;
        }
        private List<Claim> GetAccessTokenClaims(ApplicationUser user, IEnumerable<UserPermission> permissions)
        {
            var claims = GetRefreshTokenClaims(user);

            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var tempStr = new List<string>();

            foreach (var item in permissions)
            {
                var page = item.Page.ToString();
                var result = item.GetType().GetProperties().Where(x => x.Name.StartsWith("Can")).Select(z => $"{page}.{z.Name.ToString()}={z.GetValue(item).ToString().ToLower()}");

                tempStr.Add(string.Join(',', result.Select(x => x)));
            }

            var str = string.Join(',', tempStr.Select(x => x));

            claims.Add(new Claim(nameof(UserPermission), str));

            return claims;
        }
        private List<Claim> GetRefreshTokenClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            return claims;
        }
        private string Generate(List<Claim> claims, TimeSpan tokenExpiration)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Value.JwtKey));

            var credential = new SigningCredentials(key, SecurityAlgorithm);

            var token = new JwtSecurityToken(
             issuer: _jwtConfig.Value.JwtIssuer,
             audience: _jwtConfig.Value.JwtAudience,
             claims: claims,
             expires: DateTime.Now.Add(tokenExpiration),
             signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
