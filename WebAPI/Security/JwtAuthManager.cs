using Application.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Security
{
    public class JwtAuthManager: IJwtAuthManager
    {
        private readonly JwtTokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }

        public string GenerateToken(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                    _jwtTokenConfig.Issuer,
                    shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
                    claims,
                    expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
