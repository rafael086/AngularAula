using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly DataContext _dataContext;
        public DataRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entidade) where T : class
        {
            _dataContext.Add(entidade);
        }

        public void Delete<T>(T entidade) where T : class
        {
            _dataContext.Remove(entidade);
        }
        public void Update<T>(T entidade) where T : class
        {
            _dataContext.Update(entidade);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _dataContext.SaveChangesAsync()) > 0;
        }

        private IQueryable<Evento> RetornaComPalestra(bool includePalestra)
        {
            IQueryable<Evento> query = _dataContext.Eventos.Include(x => x.Lotes).Include(x => x.RedeSociais);
            if (includePalestra)
                query = query.Include(p => p.PalestranteEventos).ThenInclude(x => x.Palestrante);
            query.OrderByDescending(x => x.DataEvento);
            return query;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestra = false)
        {
            return await RetornaComPalestra(includePalestra).ToArrayAsync();
        }

        

        public async Task<Evento[]> GetEventoByTemaAsync(string tema, bool includePalestra = false)
        {
           return await RetornaComPalestra(includePalestra).Where(x => x.Tema.ToLower().Contains(tema.ToLower())).ToArrayAsync();
        }

        public async Task<Evento> GetEventosByIdAsync(int id, bool includePalestra = false)
        {
            return await RetornaComPalestra(includePalestra).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        private IQueryable<Palestrante> GetPalestranteComEvento(bool includeEvento)
        {
            IQueryable<Palestrante> query = _dataContext.Palestrantes.Include(x => x.RedeSociais);
            if (includeEvento)
                query = query.Include(p => p.PalestranteEventos).ThenInclude(x => x.Evento);
            query.OrderBy(x => x.Nome);
            return query;
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includeEvento= false)
        {
            return await GetPalestranteComEvento(includeEvento).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Palestrante[]> GetAllPalestranteByNameAsync(string name, bool includeEvento)
        {
            return await GetPalestranteComEvento(includeEvento).Where(x => x.Nome.ToLower().Contains(name.ToLower())).ToArrayAsync();

        }
    }
}
