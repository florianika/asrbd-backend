
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
            builder.HasIndex(fwr => new { fwr.FieldWorkId, fwr.RuleId }).IsUnique(); //cdo rregull duhet te jete vetem njehere ne fushaten e dhene
            builder.HasIndex(fwr => new { fwr.FieldWorkId });
            builder.HasIndex(fwr => new { fwr.RuleId });
            builder.HasOne(fwr => fwr.FieldWork)
                .WithMany(fw => fw.FieldWorkRules)
                .HasForeignKey(fwr => fwr.FieldWorkId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fwr => fwr.Rule)
                .WithMany()
                .HasForeignKey(fwr => fwr.RuleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
