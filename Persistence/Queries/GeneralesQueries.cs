using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.GeneralesDTOs;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Domain.Entities.GeneralesE;
using SLIES.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Persistence.Queries
{
    public interface IGeneralesQueries
    {
        // Country
        Task<List<CountryDTOs>> ListCountry();
        // State
        Task<List<StateDTOs>> ListState(int idCountry);
        // Cities
        Task<List<CitiesDTOs>> ListCities(int idState);
    }
    public class GeneralesQueries : IGeneralesQueries, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<ConfigurationQuerie> _logger;
        private readonly IConfiguration _configuration;

        public GeneralesQueries(ILogger<ConfigurationQuerie> logger, IConfiguration configuration)
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

        #region PAISES
        public async Task<List<CountryDTOs>> ListCountry()
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.ListCountry...");
            try
            {
                var countrys = await _context.CountryEs.ToListAsync();
                var ListCountry = new List<CountryDTOs>();

                foreach (var item in countrys)
                {
                    var list = new CountryDTOs
                    {
                        id = item.id_country,
                        nombre = item.s_name,
                    };

                    ListCountry.Add(list);
                }

                return ListCountry;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesQueries.ListCountry");
                throw;
            }

        }
        #endregion

        #region DEPARTAMENTOS
        public async Task<List<StateDTOs>> ListState(int idCountry)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.ListState...");
            try
            {
                var state = await _context.StateEs.Where(x => x.fk_tbl_country == idCountry).ToListAsync();
                var ListState = new List<StateDTOs>();

                foreach (var item in state)
                {
                    var list = new StateDTOs
                    {
                        id = item.id_state,
                        nombre = item.s_name,
                        pais = item.fk_tbl_country
                    };

                    ListState.Add(list);
                }

                return ListState;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesQueries.ListState");
                throw;
            }

        }
        #endregion

        #region CIUDADES
        public async Task<List<CitiesDTOs>> ListCities(int idState)
        {
            _logger.LogTrace("Iniciando metodo GeneralesQueries.ListCities...");
            try
            {
                var state = await _context.CitiesEs.Where(x => x.fk_tbl_country_state == idState).ToListAsync();
                var ListCities = new List<CitiesDTOs>();

                foreach (var item in state)
                {
                    var list = new CitiesDTOs
                    {
                        id = item.id_city,
                        nombre = item.s_name,
                        estado = item.fk_tbl_country_state
                    };

                    ListCities.Add(list);
                }

                return ListCities;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesQueries.ListCities");
                throw;
            }

        }
        #endregion
    }
}
