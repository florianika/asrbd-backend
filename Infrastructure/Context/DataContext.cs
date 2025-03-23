using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
    #nullable disable
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
        public DbSet<Claim> Claim { get; set; }
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
                .Entity<Domain.RolePermission>()
                .Property(r => r.EntityType)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .Entity<Domain.RolePermission>()
                .Property(r => r.Permission)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .Entity<Domain.RolePermission>()
                .Property(r => r.Role)
                .HasConversion<string>()
                .HasColumnType("varchar(10)");
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        }
    }
}
