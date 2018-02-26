using Bijankur.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Bijankur.DAL
{
    public class DataLayerContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ErrorMessage> ErrorMessage { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>().HasKey(m => m.UserId);
            builder.Entity<Roles>().HasKey(m => m.RoleId);
            builder.Entity<ErrorMessage>().HasKey(m => m.ErrorMessageId);
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .AddEnvironmentVariables();

            var configuration = builder.Build();

            var sqlConnectionString = configuration["Data:DefaultConnection:ConnectionString"];

            optionsBuilder.UseSqlServer(sqlConnectionString);
        }
    }
}
