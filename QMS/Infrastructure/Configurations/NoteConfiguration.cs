using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.NoteId);
            builder.Property(n => n.NoteText).HasColumnType("nvarchar(max)");
            builder.Property(n => n.BldId).IsRequired();
            builder.Property(n => n.UserId).IsRequired();
            builder.Property(n => n.CreatedUser).IsRequired();
            builder.Property(n=>n.CreatedUser).HasMaxLength(500);
            builder.Property(n => n.UpdatedUser).HasMaxLength(500);
            builder.Property(n => n.CreatedTimestamp).IsRequired();
            builder.Property(n => n.UpdatedUser);
            builder.Property(n => n.UpdatedTimestamp);
        }
    }
}
