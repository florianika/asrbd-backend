
using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FieldWorkConfiguration : IEntityTypeConfiguration<FieldWork>
    {
        public void Configure(EntityTypeBuilder<FieldWork> builder)
        {
            builder.HasKey(fw=> fw.FieldWorkId);
            builder.Property(fw => fw.Description).HasColumnType("nvarchar(max)");
            builder.Property(fw => fw.Remarks).HasColumnType("nvarchar(max)");
            builder.Property(fw => fw.CreatedUser).IsRequired();
            builder.Property(fw => fw.CreatedTimestamp).IsRequired();
            builder.Property(fw => fw.UpdatedUser);
            builder.Property(fw => fw.UpdatedTimestamp);
            builder.Property(fw => fw.FieldWorkStatus).IsRequired()
                .HasConversion(c => c.ToString(), c => Enum.Parse<FieldWorkStatus>(c));
        }
    }
}
