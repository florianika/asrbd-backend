
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.JobId).IsRequired();
            builder.HasIndex(s => new { s.JobId});
            builder.Property(r => r.Municipality).HasMaxLength(500);
            builder.HasOne(s => s.Job)
                .WithMany()
                .HasForeignKey(s => s.JobId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
