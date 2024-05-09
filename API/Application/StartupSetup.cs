using SLIES.Domain.Utilidades;
using SLIES.Persistence.Commands;
using SLIES.Persistence.Queries;

namespace SLIES.API.Application
{
    public static class StartupSetup
    {
        public static IServiceCollection AddStartupSetup(this IServiceCollection service, IConfiguration configuration)
        {
            // Commands Persistance Services
            service.AddTransient<IConfigurationCommand, ConfigurationCommand>();
            service.AddTransient<IUserCommands, UserCommands>();
            service.AddTransient<ICourseCommands, CourseCommands>();

            // Queries Persistance Services
            service.AddTransient<IConfigurationQuerie, ConfigurationQuerie>();
            service.AddTransient<IGeneralesQueries, GeneralesQueries>();
            service.AddTransient<IUserQueries, UserQueries>();
            service.AddTransient<ILoginQueries, LoginQueries>();
            service.AddTransient<ICourseQuerie, CourseQuerie>();

            // Utilidades
            service.AddScoped<IPasswordUtility, PasswordUtility>();
            service.AddScoped<ISaveImagen, SaveImagen>();
            service.AddScoped<IGenerateToken, GenerateToken>();

            return service;
        }
    }
}
