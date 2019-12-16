using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Using_Elasticsearch.BusinessLogic.Helpers.Interfaces;
using Using_Elasticsearch.Common.Configs;
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

        private List<Claim> GetAccessTokenClaims(ApplicationUser user)
        {
            var claims = GetRefreshTokenClaims(user);

            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            return claims;
        }
        private List<Claim> GetRefreshTokenClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

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
        public ResponseGenerateAuthentificationView ValidateData(string token)
        {
            var response = new ResponseGenerateAuthentificationView();

            var refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            //if (refreshToken.ValidTo < DateTime.Now)
            //{
            //    //response.Errors.Add(Constants.Errors.TokenExpire);
            //    return response;
            //}

            //var value = refreshToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            //if (!long.TryParse(value, out long userId))
            //{
            //    //response.Errors.Add(Constants.Errors.UserIdInvalid);
            //    return response;
            //}

            //response.UserId = userId;

            return response;
        }
        public ResponseGenerateAuthentificationView Generate(ApplicationUser user)
        {
            var result = new ResponseGenerateAuthentificationView();

            var accessClaims = GetAccessTokenClaims(user);

            var refreshClaims = GetRefreshTokenClaims(user);

            result.AccessToken = Generate(accessClaims, _jwtConfig.Value.AccessTokenExpiration);

            result.RefreshToken = Generate(refreshClaims, _jwtConfig.Value.RefreshTokenExpiration);

            return result;
        }
    }
}
