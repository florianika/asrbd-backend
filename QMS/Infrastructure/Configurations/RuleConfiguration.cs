
using Domain;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.LocalId).IsRequired().HasMaxLength(255).IsUnicode(false);
            builder.Property(r => r.Variable).IsRequired().HasMaxLength(255);
            builder.Property(r => r.NameAl).HasMaxLength(255);
            builder.Property(r => r.NameEn).HasMaxLength(255);
            builder.Property(r => r.DescriptionAl).HasMaxLength(1000);
            builder.Property(r => r.DescriptionEn).HasMaxLength(1000);
            builder.Property(r => r.Version).IsRequired().HasMaxLength(50);
            builder.Property(r => r.VersionRationale).HasMaxLength(1000);
            builder.Property(r => r.Expression).IsRequired();
            builder.Property(r => r.RuleRequirement).HasMaxLength(1000);
            builder.Property(r => r.Remark).HasMaxLength(1000);
            builder.Property(r => r.QualityMessageAl).HasMaxLength(255);
            builder.Property(r => r.QualityMessageEn).HasMaxLength(255);
            builder.Property(r => r.CreatedUser).IsRequired();
            builder.Property(r => r.CreatedTimestamp).IsRequired();
            builder.Property(r => r.UpdatedUser);
            builder.Property(r => r.UpdatedTimestamp);

            builder.HasIndex(r => r.LocalId).IsUnique();

            // Enums
            builder.Property(r => r.EntityType).IsRequired().HasConversion(c => c.ToString(), c => Enum.Parse<EntityType>(c));
            builder.Property(r => r.QualityAction).IsRequired().HasConversion(c => c.ToString(), c => Enum.Parse<QualityAction>(c));;
            builder.Property(r => r.RuleStatus).IsRequired().HasConversion(c => c.ToString(), c => Enum.Parse<RuleStatus>(c));;
        }
    }
}
