using Domain.Enum;
using Domain.RolePermission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Configurations
{
    public class RolePermisionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.VariableName).HasMaxLength(30);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.EntityType).IsRequired();
            builder.Property(x => x.Permission).IsRequired();
            // Create a unique constraint for the combination of Role, EntityType, and VariableName
            builder.HasIndex(rp => new { rp.Role, rp.EntityType, rp.VariableName })
                .IsUnique();
            builder.Property(b => b.Role).HasConversion(c => c.ToString(), c => Enum.Parse<AccountRole>(c));
            builder.Property(b => b.EntityType).HasConversion(c => c.ToString(), c => Enum.Parse<EntityType>(c));
            builder.Property(b => b.Permission).HasConversion(c => c.ToString(), c => Enum.Parse<Permission>(c));
            builder.Property(x => x.Role).HasMaxLength(25);
            builder.Property(x => x.EntityType).HasMaxLength(25);
            builder.Property(x=>x.Permission).HasMaxLength(25);
        }

    }
}
