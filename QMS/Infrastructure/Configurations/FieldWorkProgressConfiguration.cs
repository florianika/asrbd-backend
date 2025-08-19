using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FieldWorkProgressConfiguration : IEntityTypeConfiguration<Domain.FieldWorkProgress>
    {
        public void Configure(EntityTypeBuilder<FieldWorkProgress> builder)
        {
            builder.HasKey(fp => fp.Id);
            builder.Property(fp => fp.FieldworkId).IsRequired();
            builder.HasIndex(fp => new { fp.FieldworkId});
        }
    }
}
