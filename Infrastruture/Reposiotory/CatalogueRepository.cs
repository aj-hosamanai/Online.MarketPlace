using Domain.Online.MarketPlace;
using Domain.Online.MarketPlace.Base;
using Domain.Online.MarketPlace.Model;
using Infrastruture.Online.MarketPlace.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.Online.MarketPlace.Repository
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly OMPDbContext _dbContext;
        public CatalogueRepository(OMPDbContext oMPDbContext)
        {
            _dbContext = oMPDbContext ?? throw new ArgumentNullException(nameof(oMPDbContext));
        }
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(Catalogue catalogue)
        {
            _dbContext.Add(catalogue);
        }

        public void Delete(Catalogue catalogue)
        {
            _dbContext.Remove(catalogue);
        }

        public async Task<Catalogue> GetAll()
        {
           return await  _dbContext.Catalogues.AsNoTracking().FirstOrDefaultAsync();
        }

        public async  Task<Catalogue> GetById(int Id)
        {
           return await  _dbContext.Catalogues.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Catalogue catalogue)
        {
            _dbContext.Update(catalogue);
        }
    }
    public interface ICatalogueRepository : IRepository<Catalogue>
    {
        void Add(Catalogue catalogue);

        Task<Catalogue> GetAll();
        Task<Catalogue> GetById(int Id);
        void Update(Catalogue catalogue);

        void Delete(Catalogue  catalogue);
        void SaveChanges();
    }
}
