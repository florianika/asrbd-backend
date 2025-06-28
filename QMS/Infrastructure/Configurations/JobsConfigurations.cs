
using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class JobsConfigurations : IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.CreatedUser).IsRequired();
            builder.Property(j => j.CreatedTimestamp).IsRequired();
            builder.HasIndex(j => new { j.Id });
            builder.Property(j => j.Status).IsRequired().HasConversion(c => c.ToString(),
                c => Enum.Parse<JobStatus>(c));
            builder.HasOne(j => j.FieldWork)
               .WithMany()
               .HasForeignKey(s => s.FieldWorkId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
