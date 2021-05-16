using Domain.Online.MarketPlace;
using Domain.Online.MarketPlace.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastruture.Online.MarketPlace.DataContext
{
    public class OMPDbContext : DbContext, IUnitOfWork
    {
        public OMPDbContext(DbContextOptions<OMPDbContext> options) : base(options) { }
        public OMPDbContext()
        {

        }
        public virtual DbSet<PartnerDetails> PartnerDetails { get; set; }   
         public virtual DbSet<Catalogue> Catalogues  { get; set; } 
           public virtual DbSet<Order>  Orders  { get; set; } 
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync();
            return true;
        }
    }
}
