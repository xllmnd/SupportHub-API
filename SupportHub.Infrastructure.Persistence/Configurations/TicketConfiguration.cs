using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Infrastructure.Persistence.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(t => t.Id); 

            builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).IsRequired();

            builder.HasOne(t => t.Customer)
                   .WithMany(c => c.Tickets)
                   .HasForeignKey(t => t.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(t => t.AssignedAgent)
                   .WithMany(a => a.Tickets)
                   .HasForeignKey(t => t.AssignedAgentId)
                   .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}
