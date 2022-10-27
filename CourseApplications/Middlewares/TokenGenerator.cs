using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CourseApplications.Constants;
using CourseApplications.Models;
using System.Security.Claims;

namespace CourseApplications.Middlewares
{
    public class TokenGenerator
    {
        public string Generate(Candidate candidate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = AddClaims(candidate),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Settings.Secret)), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.Now.AddHours(8)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private static ClaimsIdentity AddClaims(Candidate candidate)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Name, candidate.CandidateId.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Role, "Student"));

            return claims;
        }
    }
}