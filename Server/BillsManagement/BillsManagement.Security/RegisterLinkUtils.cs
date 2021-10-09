using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Security
{
    public class RegisterLinkUtils : IRegisterLinkUtils
    {
        private readonly Secrets _secrets;

        public RegisterLinkUtils(IOptions<Secrets> secrets)
        {
            this._secrets = secrets.Value;
        }

        public string GenerateRegisterToken(int housekeeperId, int buildingId, int entranceId)
        {
            // generate register token valid for 24h
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._secrets.JWT_Secret.ToString());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("housekeeperId", housekeeperId.ToString()),
                    new Claim("buildingId", buildingId.ToString()),
                    new Claim("entranceId", entranceId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ExtractedRegisterToken ValidateRegisterToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._secrets.JWT_Secret.ToString());
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                ExtractedRegisterToken extractedData = new ExtractedRegisterToken();
                //extractedData.OccupantId = int.Parse(jwtToken.Claims.First(x => x.Type == "occupantId").Value);
                extractedData.BuildingId = int.Parse(jwtToken.Claims.First(x => x.Type == "buildingId").Value);
                extractedData.EntranceId = int.Parse(jwtToken.Claims.First(x => x.Type == "entranceId").Value);

                return extractedData;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
