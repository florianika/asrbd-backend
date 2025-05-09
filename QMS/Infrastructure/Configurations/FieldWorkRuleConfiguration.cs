
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FieldWorkRuleConfiguration : IEntityTypeConfiguration<FieldWorkRule>
    {
        public void Configure(EntityTypeBuilder<FieldWorkRule> builder)
        {
            builder.HasKey(fwr => fwr.Id);
            builder.Property(fwr => fwr.RuleId).IsRequired();
            builder.Property(fwr => fwr.FieldWorkId).IsRequired();
            builder.Property(fwr => fwr.CreatedUser).IsRequired();
            builder.Property(fwr => fwr.CreatedTimestamp).IsRequired();
        }
    }
}
