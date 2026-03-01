using Microsoft.EntityFrameworkCore;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Infrastructure.Persistence.Context
{
    public class SupportHubDbContext : DbContext
    {
        public SupportHubDbContext(DbContextOptions<SupportHubDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SupportHubDbContext).Assembly);
        }
        
        public DbSet<Agent> Agents  { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}
