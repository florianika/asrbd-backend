using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<Domain.Claim>
    {
        public void Configure(EntityTypeBuilder<Domain.Claim> builder)
        {
            builder.HasKey(x => x.ClaimId);
            builder.Property(x => x.ClaimId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
        }
    }
}
