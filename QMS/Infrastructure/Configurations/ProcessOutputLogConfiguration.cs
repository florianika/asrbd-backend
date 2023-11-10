
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class ProcessOutputLogConfiguration : IEntityTypeConfiguration<ProcessOutputLog>
    {
        public void Configure(EntityTypeBuilder<ProcessOutputLog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.BldId).IsRequired();
            builder.Property(p => p.EntId);
            builder.Property(p => p.DwlId);
            builder.Property(p => p.EntityType).IsRequired().HasConversion<string>();
            builder.Property(p => p.Variable).IsRequired();
            builder.Property(p => p.QualityAction).IsRequired().HasConversion<string>();
            builder.Property(p => p.QualityStatus).IsRequired().HasConversion<string>();
            builder.Property(p => p.QualityMessageAl);
            builder.Property(p => p.QualityMessageEn);
            builder.Property(p => p.ErrorColor).IsRequired().HasConversion<string>();
            builder.Property(p => p.CreatedUser).IsRequired();
            builder.Property(p => p.CreatedTimestamp).IsRequired();

            // Foreign Key
            builder.HasOne(p => p.Rule)
                .WithMany()
                .HasForeignKey(p => p.RuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
