using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDataRepository
    {
        void Add<T>(T entidade) where T : class;
        void Update<T>(T entidade) where T : class;
        void Delete<T>(T entidade) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Evento[]> GetEventoByTemaAsync(string tema, bool includePalestra);
        Task<Evento[]> GetAllEventosAsync(bool includePalestra);
        Task<Evento> GetEventosByIdAsync(int id, bool includePalestra);
        Task<Palestrante[]> GetAllPalestranteByNameAsync(string name, bool includeEvento);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEvento);
    }
}
