using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.RefreshToken;

namespace Infrastructure.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.RefreshTokenId);
            builder.Property(x => x.RefreshTokenId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
            builder.Property(x => x.Value).IsRequired(false);
            builder.Property(x => x.Active).IsRequired(false);
            builder.Property(x => x.ExpirationDate).IsRequired(false);
        }
    }
}
