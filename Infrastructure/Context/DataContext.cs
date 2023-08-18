
using Application.User.CreateUser.Request;
using Application.User.GetAllUsers;
using Domain.Claim;
using Domain.RefreshToken;
using Domain.User;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Domain.Claim.Claim> Claim { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("ASRBDConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>()
            .Property(u => u.AccountRole)
            .HasConversion<string>();

            base.OnModelCreating(modelBuilder);

        }
    }
}
