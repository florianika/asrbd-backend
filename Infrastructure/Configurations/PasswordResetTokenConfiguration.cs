using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().HasColumnType("bigint");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.TokenHash).IsRequired().HasMaxLength(256);
            builder.Property(x => x.ExpiresAt).IsRequired();
            builder.Property(x => x.ConsumedAt).IsRequired(false);
            builder.Property(x => x.Attempts).HasDefaultValue(0);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("SYSDATETIME()");
            builder.HasIndex(x => x.UserId).HasDatabaseName("IX_PwReset_User");
            builder.HasIndex(x => x.TokenHash).HasDatabaseName("IX_PwReset_TokenHash");
        }
    }
}
