using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SLIES.Domain.DTOs.UserDTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Text;

namespace SLIES.Domain.Utilidades
{
    public interface IGenerateToken
    {
        Task<string> Token(DateUserDTOs dateUserDTOs);
    }

    public class GenerateToken : IGenerateToken
    {
        private readonly ILogger<GenerateToken> _logger;

        public GenerateToken(ILogger<GenerateToken> logger)
        {
            _logger = logger;
        }

        public async Task<string> Token(DateUserDTOs dateUserDTOs)
        {
            _logger.LogTrace("Iniciando metodo GenerateToken.Token...");
            try
            {

                var key = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                var keyBytes = Encoding.ASCII.GetBytes(key);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(dateUserDTOs.idUsuario)));
                claims.AddClaim(new Claim("idUsuario", Convert.ToString(dateUserDTOs.idUsuario)));
                claims.AddClaim(new Claim("nombre", dateUserDTOs.nombre));
                claims.AddClaim(new Claim("correo", dateUserDTOs.correo));
                claims.AddClaim(new Claim("foto", dateUserDTOs.foto));
                claims.AddClaim(new Claim("tipoUsuario", Convert.ToString(dateUserDTOs.tipoUsuario)));

                var credencialesToken = new SigningCredentials
                (
                   new SymmetricSecurityKey(keyBytes),
                   SecurityAlgorithms.HmacSha256Signature
                );

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddDays(10),
                    SigningCredentials = credencialesToken
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return tokenCreado;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GenerateToken.Token...");
                throw;
            }
        }
    }
}
