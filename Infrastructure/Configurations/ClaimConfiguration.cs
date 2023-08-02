using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Claim;

namespace Infrastructure.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
    {
        public void Configure(EntityTypeBuilder<Claim> builder)
        {
            builder.HasKey(x => x.ClaimId);
            builder.Property(x => x.ClaimId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
        }
    }
}
