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
    public class OrderRepository : IOrderRepository
    {
        private readonly OMPDbContext _dbContext;
        public OrderRepository(OMPDbContext oMPDbContext)
        {
            _dbContext = oMPDbContext ?? throw new ArgumentNullException(nameof(oMPDbContext));
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(Order order)
        {
            _dbContext.Add(order);
        }

        public async  Task<IEnumerable<Order>> GetById(int id )
        {
            return await _dbContext.Orders.Where(x => x.OrderId == id).AsNoTracking().ToListAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();

        }

        public void SaveChangesAsync()
        {
            _dbContext.SaveChangesAsync();
        }
    }
    public interface IOrderRepository : IRepository<PartnerDetails>
    {
        void Add(Order order);
        Task<IEnumerable<Order>> GetById(int id);
        void SaveChanges();
        void SaveChangesAsync();
    }
}
