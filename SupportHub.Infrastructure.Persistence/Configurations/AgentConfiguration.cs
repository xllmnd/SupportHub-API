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
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.ToTable("Agents");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(a => a.Email).IsUnique();

            builder.Property(a => a.CreateAt)
                .IsRequired()
                .HasDefaultValueSql("getutcdate()"); 
        }
    }
}
