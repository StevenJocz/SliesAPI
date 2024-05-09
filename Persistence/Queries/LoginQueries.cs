using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.GenralesDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Domain.Utilidades;
using SLIES.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Persistence.Queries
{
    public interface ILoginQueries
    {
        Task<Responselogin> Login(string correo, string password);
    }
    public class LoginQueries : ILoginQueries, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<LoginQueries> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordUtility _passwordUtility;
        private readonly IGenerateToken _generateToken;

        public LoginQueries(ILogger<LoginQueries> logger, IConfiguration configuration, IPasswordUtility passwordUtility, IGenerateToken generateToken)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new SLIESDbContext(connectionString);
            _passwordUtility = passwordUtility;
            _generateToken = generateToken;
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

        #region LOGIN
        public async Task<Responselogin> Login(string correo, string password)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListTypeDocumentId...");
            try
            {
                correo = correo.Trim();
                password = password.Trim();

                var userExists = await _context.UserLoginEs.AsNoTracking().FirstOrDefaultAsync(x => x.s_email == correo);

                var VerifyPassword = await _passwordUtility.VerifyPassword(password, userExists.s_password);

                if (VerifyPassword)
                {
                    var user = await _context.UserEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_user == userExists.fk_tbl_user);

                    var newDateUserDTOs = new DateUserDTOs
                    {
                        idUsuario = user.id_user,
                        nombre = user.s_name,
                        correo = user.s_email,
                        foto = user.s_photo,
                        tipoUsuario = userExists.fk_tbl_user_type
                    };

                    var token = await _generateToken.Token(newDateUserDTOs);

                    return new Responselogin
                    {
                        result = true,
                        message = "¡Inicio de sesión exitos!",
                        Token = token
                    };

                }
                else
                {
                    return new Responselogin
                    {
                        result = false,
                        message = "Las credenciales de correo electrónico o contraseña proporcionadas son inválidas. Por favor, verifica la información ingresada e intenta nuevamente.",
                        Token = ""
                    };
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListTypeDocumentId");
                throw;
            }

        }
        #endregion
    }
}
