using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nibo.Prova.Domain.Models.Transactions;
using Nibo.Prova.Domain.SeedWork;
using Nibo.Prova.Infrastructure.Configurations;

namespace Nibo.Prova.Infrastructure
{
    public class NiboDbContext : DbContext, IUnitOfWork
    {
        public NiboDbContext(DbContextOptions<NiboDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await base.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // ...
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionConfigurations());

            
        }
    }
}
