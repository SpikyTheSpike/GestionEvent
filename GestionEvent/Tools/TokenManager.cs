using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestionEvent.Tools
{
    public class TokenManager
    {
        private string _secret;
        public TokenManager(IConfiguration config)
        {
            _secret = config.GetSection("JwtInfo").GetSection("secret").Value;
        }

        public string GenerateToken(Member m)
        {
            //Créer la signing key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            SigningCredentials credentils = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Création du payload /claims
            Claim[] myClaims = new[] {
            new Claim("MemberId",  m.MemberId.ToString()),
            new Claim(ClaimTypes.GivenName,  m.Pseudo)
            };

            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: myClaims,
                signingCredentials: credentils,
                expires: DateTime.Now.AddDays(1)
                );

            //Génération du token 
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(securityToken);
        }


    }
}
