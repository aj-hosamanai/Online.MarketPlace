using Domain.Online.MarketPlace;
using Domain.Online.MarketPlace.Base;
using Domain.Online.MarketPlace.Model;
using Infrastruture.Online.MarketPlace.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.Online.MarketPlace.Repository
{

    public  class PartnerDetailRepository: IPartnerDetailRepository
    {
        private readonly OMPDbContext _dbContext;
        public PartnerDetailRepository(OMPDbContext oMPDbContext)
        {
            _dbContext = oMPDbContext ?? throw new ArgumentNullException(nameof(oMPDbContext));
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(PartnerDetails partnerDetails)
        {
            _dbContext.Add(partnerDetails);
        }

        public async Task<PartnerDetails> GetAll()
        {
            return await _dbContext.PartnerDetails.AsNoTracking().FirstOrDefaultAsync();
        }

        public void SaveChanges()
        {
           _dbContext.SaveChanges();
        }
        
    }

    public interface IPartnerDetailRepository : IRepository<PartnerDetails>
    {
        void Add(PartnerDetails partnerDetails);

        Task<PartnerDetails> GetAll();

      void SaveChanges();
       
    }
}
