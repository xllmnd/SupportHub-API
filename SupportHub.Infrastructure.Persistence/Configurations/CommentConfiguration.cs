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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.Property(c => c.Body).IsRequired().HasMaxLength(2000);

            builder.HasOne(c => c.Ticket)
                   .WithMany(t => t.Comments)
                   .HasForeignKey(c => c.TicketId);

            builder.HasOne(c => c.Agent)
                   .WithMany(a => a.Comments)
                   .HasForeignKey(c => c.AgentId);

            builder.HasOne(c => c.Customer)
                   .WithMany(cust => cust.Comments)
                   .HasForeignKey(c => c.CustomerId);
        }
    }
}
