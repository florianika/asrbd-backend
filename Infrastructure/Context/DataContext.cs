using Domain.RefreshToken;
using Domain.RolePermission;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        public DbSet<RolePermission> RolePermissions { get; set; }
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
            
            modelBuilder
                .Entity<User>()
                .Property(u => u.AccountRole)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .Entity<User>()
                .Property(u => u.AccountStatus)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .Entity<RolePermission>()
                .Property(r => r.EntityType)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .Entity<RolePermission>()
                .Property(r => r.Permission)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .Entity<RolePermission>()
                .Property(r => r.Role)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        }
    }
}
