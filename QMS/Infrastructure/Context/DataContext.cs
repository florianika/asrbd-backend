using Application.FieldWork.SendFieldWorkEmail;
using Application.Queries.GetStatisticsFromBuilding;
using Application.Queries.GetStatisticsFromRules;
using Domain;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<Rule> Rules { get; set; }
        public DbSet<ProcessOutputLog> ProcessOutputLogs { get; set; }      
        public DbSet<FieldWork> FieldWorks { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<FieldWorkRule> FieldWorkRules { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<Jobs>Jobs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("QMSConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RuleConfiguration());
            modelBuilder.ApplyConfiguration(new ProcessOutputLogConfiguration());
            modelBuilder.ApplyConfiguration(new FieldWorkConfiguration());
            modelBuilder.ApplyConfiguration(new EmailTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new FieldWorkRuleConfiguration());
            modelBuilder.Entity<RuleStatisticsDTO>().HasNoKey();
            modelBuilder.Entity<BuildingStatisticsDTO>().HasNoKey();
            modelBuilder.Entity<UserDTO>().HasNoKey().ToView(null);
            modelBuilder.ApplyConfiguration(new StatisticsConfiguration());
            modelBuilder.ApplyConfiguration(new JobsConfigurations());
        }
    }
}
