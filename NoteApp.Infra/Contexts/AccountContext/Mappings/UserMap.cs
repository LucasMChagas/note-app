using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Domain.Contexts.AccountContext.Entities;

namespace NoteApp.Infra.Contexts.AccountContext.Mappings;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(t => t.Image)
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(t => t.Email)
            .Property(t => t.Address)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(t => t.Email)
            .OwnsOne(t => t.Verification)
            .Property(t => t.Code)
            .HasColumnName("EmailVerificationCode")
            .IsRequired();

        builder.OwnsOne(t => t.Email)
            .OwnsOne(t => t.Verification)
            .Property(t => t.ExpiresAt)
            .HasColumnName("EmailVerificationExpiresAt")
            .IsRequired(false);

        builder.OwnsOne(t => t.Email)
            .OwnsOne(t => t.Verification)
            .Property(t => t.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .IsRequired(false);

        builder.OwnsOne(t => t.Email)
            .OwnsOne(t => t.Verification)
            .Ignore(t => t.IsActive);

        builder.OwnsOne(t => t.Password)
            .Property(t => t.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();

        builder.OwnsOne(t => t.Password)
            .Property(t => t.ResetCode)
            .HasColumnName("PasswordResetCode")
            .IsRequired();

        builder.HasMany(u => u.Notes)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Roles)
            .WithMany(n => n.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",

                role => role
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey("RoleId")
                .OnDelete(DeleteBehavior.Cascade),
                
                user => user
                .HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade));


    }
}
