using Domain;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(et => et.EmailTemplateId);
            builder.Property(et => et.Subject).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(et => et.Body).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(et => et.CreatedUser).IsRequired();
            builder.Property(et => et.CreatedTimestamp).IsRequired();
            builder.Property(et => et.UpdatedUser);
            builder.Property(et => et.UpdatedTimestamp);
        }
    }
}
