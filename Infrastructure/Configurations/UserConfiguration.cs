using Domain.Enum;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        { 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property<string>(x => x.Email).IsRequired();
            builder.Property<string>(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.LastName).HasMaxLength(255);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasMany(u=>u.Claims)
                .WithOne(c=>c.User)
                .HasForeignKey(c=>c.UserId);

            builder.HasOne(u=>u.RefreshToken)
                .WithOne(rt=>rt.User)
                .HasForeignKey<RefreshToken>(rt=>rt.UserId);

            builder.Property(b => b.AccountRole).HasConversion(c => c.ToString(), c => Enum.Parse<AccountRole>(c));
            builder.Property(b => b.AccountStatus).HasConversion(c => c.ToString(), c => Enum.Parse<AccountStatus>(c));

            builder.Property(x => x.AccountStatus).HasMaxLength(25);
            builder.Property(x => x.AccountRole).HasMaxLength(25);
        }
    }
}
