using HealthCheckerCore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCheckerCore.Infrastructure.Data
{
    public class HealthCheckerContext : DbContext
    {
        public HealthCheckerContext(DbContextOptions<HealthCheckerContext> options) : base(options)
        {
        }

        public DbSet<MonitorConfig> MonitorConfig { get; set; }
        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MonitorConfig>(ConfigureCheckConfig);
        }

        private void ConfigureCheckConfig(EntityTypeBuilder<MonitorConfig> builder)
        {
            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(1000);

            builder.Property(ci => ci.Url)
                .IsRequired(true)
                .HasMaxLength(1000);
        }




    }
}
