using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Domain.Entities.UserE;
using SLIES.Infrastructure;
using SLIES.Persistence.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Persistence.Queries
{
    public interface IUserQueries
    {
        Task<List<UsuariosDTOs>> ListUser();
        Task<UserInformationDTOs> UserId(int id);
        Task<List<UsuariosDTOs>> ListUserTeacher();
        Task<List<UserPermissionDTOs>> UserPermission(int idUser);
    }
    public class UserQueries : IUserQueries, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<UserQueries> _logger;
        private readonly IConfiguration _configuration;

        public UserQueries(ILogger<UserQueries> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new SLIESDbContext(connectionString);
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

        #region PERMISOS DE USUARIOS
        public async Task<List<UserPermissionDTOs>> UserPermission(int id)
        {
            _logger.LogTrace("Iniciando metodo UserQueries.UserPermission...");
            try
            {
                var permisos = await _context.UserPermissionEs.Where(x => x.fk_tbl_user_type == id).ToListAsync();
                var ListPermsios = new List<UserPermissionDTOs>();

                foreach (var item in permisos)
                {
                    var list = new UserPermissionDTOs
                    {
                        id = item.id_user_permission,
                        idUsuario = item.fk_tbl_user_type,
                        path = item.s_path,
                        icon = item.s_icon,
                        title = item.s_title,
                        activo = item.bool_active,
                    };

                    ListPermsios.Add(list);
                }

                return ListPermsios;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar  UserQueries.UserPermission");
                throw;
            }
        }
        #endregion

        #region lISTA DE USUARIOS
        public async Task<List<UsuariosDTOs>> ListUser()
        {
            _logger.LogTrace("Iniciando metodo UserQueries.ListUser...");
            try
            {
                var users = await _context.UserEs.ToListAsync();
                var ListUsers = new List<UsuariosDTOs>();

                foreach (var item in users)
                {
                    var rol = await _context.UserLoginEs.AsNoTracking().FirstOrDefaultAsync(x => x.fk_tbl_user == item.id_user);
                    var list = new UsuariosDTOs
                    {
                        id = item.id_user,
                        foto = item.s_photo,
                        nombre = item.s_name,
                        documento = item.s_document,
                        correo = item.s_email,
                        contacto = item.s_phone,
                        rol = rol.fk_tbl_user_type == 1 ? "Administrador" : rol.fk_tbl_user_type == 2 ? "Profesor" : "Usuario",
                    };

                    ListUsers.Add(list);
                }

                return ListUsers;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar  UserQueries.ListUser");
                throw;
            }
        }
        #endregion

        #region USUARIO
        public async Task<UserInformationDTOs> UserId(int id)
        {
            _logger.LogTrace("Iniciando metodo UserQueries.UserId...");
            try
            {
                var user = await _context.UserEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_user == id);

                var tipoDocumento = await _context.TypeDocumentEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_type_document == user.fk_tbl_type_document);

                var pais = await _context.CountryEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_country == user.fk_tbl_country);

                var departamento = await _context.StateEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_state == user.fk_tbl_country_state);

                var ciudad = await _context.CitiesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_city == user.fk_tbl_country_state_city);

                var rol = await _context.UserLoginEs.AsNoTracking().FirstOrDefaultAsync(x => x.fk_tbl_user == id);

                var esProfesor = await _context.UserTeacherEs.AsNoTracking().FirstOrDefaultAsync(x => x.fk_tbl_user == id);

                var list = new UserInformationDTOs
                {
                    id = user.id_user,
                    nombre = user.s_name,
                    correo = user.s_email,
                    tipoDocumento = tipoDocumento.s_abbreviation + " - " + tipoDocumento.s_name,
                    documento = user.s_document,
                    fechaNacimiento = user.dt_birth.ToString(),
                    celular = user.s_phone,
                    pais = pais.s_name,
                    departamento = departamento.s_name,
                    ciudad = ciudad != null  ? ciudad.s_name : "",
                    direccion = user.s_address,
                    foto = user.s_photo,
                    activo = user.byte_active,
                    fechaRegistro = user.dt_registration.ToString("yyyy-MM-dd HH:mm:ss"),
                    rol = rol.fk_tbl_user_type,
                    esProfesor =  esProfesor.bool_active
                };

                return list;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserQueries.UserId.");
                throw;
            }

        }
        #endregion

        #region lISTA DE PROFESORES
        public async Task<List<UsuariosDTOs>> ListUserTeacher()
        {
            _logger.LogTrace("Iniciando metodo UserQueries.ListUserTeacher...");
            try
            {
                var users = await _context.UserEs.ToListAsync();
                var teachers = await _context.UserTeacherEs.Where(x => x.bool_active == true).ToListAsync();

                var ListUsers = (from user in users
                                 join teacher in teachers
                                 on user.id_user equals teacher.fk_tbl_user
                                 select new UsuariosDTOs
                                 {
                                     id = teacher.id_user_teacher,
                                     foto = user.s_photo,
                                     nombre = user.s_name,
                                     documento = user.s_document,
                                     correo = user.s_email,
                                     contacto = user.s_phone,
                                     profesion = teacher.s_profession

                                 }).ToList();

                return ListUsers;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserQueries.ListUserTeacher");
                throw;
            }
        }

        #endregion
    }
}
