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
            //// Seed initial data with all combinations and an empty VariableName
            //var combinations = Enum.GetValues(typeof(AccountRole))
            //    .Cast<AccountRole>()
            //    .SelectMany(role => Enum.GetValues(typeof(EntityType))
            //        .Cast<EntityType>()
            //        .Select(entityType => new RolePermission
            //        {
            //            Id = (long)role * 100 + (long)entityType,
            //            Role = role,
            //            EntityType = entityType,
            //            VariableName = "",
            //            Permission = Permission.NONE
            //        }));

            //builder.HasData(combinations);

        }
    }
}
