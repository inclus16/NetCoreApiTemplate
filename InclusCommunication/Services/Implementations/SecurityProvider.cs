using InclusCommunication.Entities;
using InclusCommunication.Http.Requests;
using InclusCommunication.Http.Responses;
using InclusCommunication.Services.Interfaces;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InclusCommunication.Services.Implementations
{
    public class SecurityProvider
    {
        private readonly IRepository<Credentials> Credentials;

        private readonly IConfiguration Configuration;
        public SecurityProvider(
            IRepository<Credentials> credentials
            ,IConfiguration configuration)
        {
            Credentials = credentials;
            Configuration = configuration;
        }

        public string GetToken(LoginRequest  request)
        {
            Credentials credentials = Credentials.First(x => x.Login == request.Login);
            var test = Credentials.All();
            if (credentials == null||!VerifyUser(credentials.Password,request.Password))
            {
                return null;
            }
            return BuildToken(credentials.User); ;
        }

        private bool VerifyUser(string dbPassword, string providedPassword)
        {
            return Argon2.Verify(dbPassword, providedPassword);
        }

        private string BuildToken(User user)
        {
            DateTime now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                  notBefore: now,
                  claims: GetIdentity(user).Claims,
                  issuer: Configuration.GetValue<string>("JwtIssuer"),
                  expires: now.Add(TimeSpan.FromMinutes(Configuration.GetValue<double>("JwtLifetime"))),
                  signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JwtToken"))), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(User user)
        {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.Name)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            
        }

    }
}
