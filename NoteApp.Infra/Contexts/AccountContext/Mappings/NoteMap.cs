using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Infra.Contexts.AccountContext.Mappings;

public class NoteMap : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("Notes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Body)
            .HasColumnName("Body")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(512)
            .IsRequired();


        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("DATETIME2")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("DATETIME2");
    }
}
