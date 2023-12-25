using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TwitterLite.Server.Helpers
{
    public class JwtService
    {
        private string secureKey = "tjis is a very secured key hjkdsqfqs ffqsdgb qklfgj";

        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwtHeader = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(jwtHeader, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private JwtSecurityToken GetJwtSecurityToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);

            tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }

        public bool IsTokenExpired(string jwtToken)
        {
            try
            { 
                return !(DateTime.Now < GetJwtSecurityToken(jwtToken).ValidTo.ToLocalTime());
            }
            catch { return false; }
        }

        public int GetUserIdFromToken(string jwtToken)
        {
            try
            {
                return int.Parse(GetJwtSecurityToken(jwtToken).Issuer);
            }
            catch { return 0; }
        }
    }
}
