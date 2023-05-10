using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PTN.WebAPI.Auth
{
    public class JwtAuth
    {
        private SymmetricSecurityKey GetCredential()
        {
            const string key = "bencokguvenliyim";

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public JwtSecurityToken GetToken(IEnumerable<Claim> claimsIdentities)
        {
            return new JwtSecurityToken
            (
                claims: claimsIdentities,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(GetCredential(), SecurityAlgorithms.HmacSha256)
            );
        }

        public JwtSecurityToken? ValidateToken(string tokenString)
        {
            try 
            {
                var handler = new JwtSecurityTokenHandler().ValidateToken
                    (
                        tokenString,
                        new TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            IssuerSigningKey = GetCredential()
                        },
                        out SecurityToken validatedToken
                    );
                return (JwtSecurityToken)validatedToken;
            }
            catch(System.Exception) 
            {
                return null;
            }
        }
    }
}
