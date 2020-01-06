using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Using_Elasticsearch.Common.Configs;

namespace Using_Elasticsearch.Presentation.Common.Extensions
{
    public static class JwtExtension
    {
        public static void AddJwt(IServiceCollection services)
        {
            var jwtOptions = services.BuildServiceProvider().GetService<IOptions<JwtConfig>>();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Value.JwtKey));

            var tokenValidationParameter = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,

                //ValidIssuer = jwtOptions.Value.JwtIssuer,
                ValidateIssuer = false,

                //ValidAudience = jwtOptions.Value.JwtAudience,
                ValidateAudience = false,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(scheme =>
            {
                scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //scheme.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = tokenValidationParameter;
             });
        }
    }
}
