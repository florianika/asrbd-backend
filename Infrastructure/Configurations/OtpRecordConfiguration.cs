using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OtpRecordConfiguration : IEntityTypeConfiguration<Domain.OtpRecord>
    {
        public void Configure(EntityTypeBuilder<Domain.OtpRecord> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd()
                   .HasColumnType("bigint");
            builder.Property(x => x.UserId)
                   .IsRequired();
            builder.Property(x => x.CodeHash)
                   .IsRequired()
                   .HasMaxLength(128);
            builder.Property(x => x.ExpiresAt)
                   .IsRequired();
            builder.Property(x => x.ConsumedAt);
            builder.Property(x => x.Attempts)
                   .IsRequired()
                   .HasDefaultValue(0);
            builder.Property(x => x.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("SYSDATETIME()");
            builder.HasIndex(x => new { x.UserId, x.CodeHash })
                   .IsUnique()
                   .HasDatabaseName("UX_OtpRecords_User_Code");
            builder.HasIndex(x => x.UserId)
                   .HasDatabaseName("IX_OtpRecords_User");
        }
    }
}
