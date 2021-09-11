using Frota.Carros.Api.DTOs.SettingOptions;
using Frota.Carros.Domain.Models.Usuario;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Frota.Carros.Api.Authentication
{
    public class AuthenticationJwtService : IAuthenticationService
    {
        private readonly JwtKeyOptions _jwtKeyOptions;

        public AuthenticationJwtService(IOptionsMonitor<JwtKeyOptions> optionsMonitor)
        {
            _jwtKeyOptions = optionsMonitor.CurrentValue;
        }

        public string GerarToken(Usuario usuario)
        {
            var secret = Encoding.ASCII.GetBytes(_jwtKeyOptions.Secret);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return token;
        }
    }
}
