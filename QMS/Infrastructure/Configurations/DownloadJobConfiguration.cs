using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class DownloadJobConfiguration : IEntityTypeConfiguration<DownloadJob>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DownloadJob> builder)
        {
            builder.HasKey(j => j.Id);
            builder.Property(j => j.ReferenceYear).IsRequired();
            builder.Property(j => j.CreatedBy).IsRequired();
            builder.HasIndex(j => j.ReferenceYear).IsUnique(); //only one record per year
            builder.Property(j => j.Status)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<DownloadStatus>(v));
            builder.Property(j => j.FileUrl)
                   .HasMaxLength(500);
        }
    }
}
