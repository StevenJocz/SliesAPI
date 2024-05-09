using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.GenralesDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Domain.Entities.UserE;
using SLIES.Domain.Utilidades;
using SLIES.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Persistence.Commands
{
    public interface IUserCommands
    {
        Task<RespuestasDTO> createUser(UserDTOs userDTOs);
        Task<RespuestasDTO> assignTeacher(RegistreTeacherDTOs registreTeacherDTOs);
        Task<RespuestasDTO> assignAdmin(RegistreTeacherDTOs registreTeacherDTOs);
    }
    public class UserCommands : IUserCommands, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<UserCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPasswordUtility _passwordUtility;
        private readonly ISaveImagen _saveImagen;

        public UserCommands(ILogger<UserCommands> logger, IConfiguration configuration, IPasswordUtility passwordUtility, ISaveImagen saveImagen)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new SLIESDbContext(connectionString);
            _passwordUtility = passwordUtility;
            _saveImagen = saveImagen;
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

        #region CREAR CUENTA

        public async Task<RespuestasDTO> createUser(UserDTOs userDTOs)
        {
            _logger.LogTrace("Iniciando metodo UserCommands.createUser...");
            try
            {

                string rutaImagen = "wwwroot/ImagenesUser";
                var rutaPhoto = await _saveImagen.SaveImageAsync(userDTOs.foto, rutaImagen);

                var newUserDTOs = new UserDTOs
                {
                    nombre = userDTOs.nombre,
                    correo = userDTOs.correo,
                    tipoDocumento = userDTOs.tipoDocumento,
                    documento = userDTOs.documento,
                    fechaNacimiento = userDTOs.fechaNacimiento,
                    celular = userDTOs.celular,
                    pais = userDTOs.pais,
                    departamento = userDTOs.departamento,
                    ciudad = userDTOs.ciudad,
                    direccion = userDTOs.direccion,
                    foto = rutaPhoto,
                    activo = userDTOs.activo,
                    fechaRegistro = DateTime.UtcNow,
                };

                var UserE = UserDTOs.CreateE(newUserDTOs);
                await _context.UserEs.AddAsync(UserE);
                await _context.SaveChangesAsync();

                if (UserE.id_user != 0)
                {
                    var hashedPassword = await _passwordUtility.HashPassword(userDTOs.password);

                    var newUserLoginDTOs = new UserLoginDTOs
                    {
                        tipoUsuario = 3,
                        correo = userDTOs.correo,
                        password = hashedPassword,
                        activo = userDTOs.activo,
                        idUsuario = UserE.id_user,
                    };

                    var UserLoginE = UserLoginDTOs.CreateE(newUserLoginDTOs);
                    await _context.UserLoginEs.AddAsync(UserLoginE);
                    await _context.SaveChangesAsync();

                    if (UserLoginE.id_user_login != 0)
                    {
                        return new RespuestasDTO
                        {
                            resultado = true,
                            message = "¡Cuenta creada exitosamente!",
                        };
                    } else
                    {
                        return new RespuestasDTO
                        {
                            resultado = false,
                            message = "¡No se pudo crear la cuenta! Por favor, inténtalo de nuevo más tarde.",
                        };
                    }
                    
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo crear la cuenta! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UserCommands.createUser...");
                throw;
            }
        }

        #endregion


        #region ASIGNAR PROFESOR
        public async Task<RespuestasDTO> assignTeacher(RegistreTeacherDTOs registreTeacherDTOs)
        {
            _logger.LogTrace("Iniciando metodo UserCommands.assignTeacher...");
            try
            {
                var teacher = await _context.UserTeacherEs.FirstOrDefaultAsync(x => x.fk_tbl_user == registreTeacherDTOs.idUsuario);
                var login = await _context.UserLoginEs.FirstOrDefaultAsync(x => x.fk_tbl_user == registreTeacherDTOs.idUsuario);

                if (registreTeacherDTOs.accion == 1 )
                {
                    var user = await _context.UserEs.FirstOrDefaultAsync(x => x.id_user == registreTeacherDTOs.idUsuario);
                    if (teacher == null)
                    {
                        
                        if (user != null || login != null)
                        {
                            await _saveImagen.DeleteImage(user.s_photo);

                            string rutaImagen = "wwwroot/ImagenesUser";
                            var rutaPhoto = await _saveImagen.SaveImageAsync(registreTeacherDTOs.foto, rutaImagen);

                            user.s_photo = rutaPhoto;

                            if (login.fk_tbl_user_type != 1)
                            {
                                login.fk_tbl_user_type = 2;
                            }

                            var newUserTeacherDTOs = new UserTeacherDTOs
                            {
                                profesion = registreTeacherDTOs.profesion,
                                idUsuario = registreTeacherDTOs.idUsuario,
                            };

                            var UserTeacherE = UserTeacherDTOs.CreateE(newUserTeacherDTOs);
                            await _context.UserTeacherEs.AddAsync(UserTeacherE);
                            await _context.SaveChangesAsync();

                            return new RespuestasDTO
                            {
                                resultado = true,
                                message = "¡Profesor asignado exitosamente!",
                            };

                        }
                        else
                        {
                            return new RespuestasDTO
                            {
                                resultado = false,
                                message = "¡No se pudo asinar el profesor! Por favor, inténtalo de nuevo más tarde.",
                            };
                        }

                    }
                    else
                    {
                        await _saveImagen.DeleteImage(user.s_photo);

                        string rutaImagen = "wwwroot/ImagenesUser";
                        var rutaPhoto = await _saveImagen.SaveImageAsync(registreTeacherDTOs.foto, rutaImagen);

                        user.s_photo = rutaPhoto;

                        if (login.fk_tbl_user_type != 1)
                        {
                            login.fk_tbl_user_type = 2;
                        }

                        teacher.s_profession = registreTeacherDTOs.profesion;
                        teacher.bool_active = true;
                        await _context.SaveChangesAsync();

                        return new RespuestasDTO
                        {
                            resultado = true,
                            message = "¡Profesor asignado exitosamente!",
                        };
                    }
                }
                else
                {
                    if (login.fk_tbl_user_type != 1)
                    {
                        login.fk_tbl_user_type = 3;
                    }

                    teacher.bool_active = false;
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Profesor asignado exitosamente!",
                    };

                } 

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UserCommands.assignTeacher...");
                throw;
            }
        }
        #endregion

        #region ASIGNAR ADMINISTRADOR
        public async Task<RespuestasDTO> assignAdmin(RegistreTeacherDTOs registreTeacherDTOs)
        {
            _logger.LogTrace("Iniciando metodo UserCommands.assignAdmin...");
            try
            {
                var user = _context.UserEs.Find(registreTeacherDTOs.idUsuario);
                var login = _context.UserLoginEs.Find(registreTeacherDTOs.idUsuario);
                if (user != null || login != null)
                {
                    login.fk_tbl_user_type = 1;
                    _context.SaveChanges();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Administrador asignado exitosamente!",
                    };

                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo asinar el el administrador! Por favor, inténtalo de nuevo más tarde.",
                    };
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo UserCommands.assignAdmin...");
                throw;
            }
        }
        #endregion
    }
}
