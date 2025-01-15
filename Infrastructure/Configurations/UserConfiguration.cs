using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany<Comment>(e => e.Comments)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId);
        
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(c => c.VerifiedAt)
            .HasConversion(dateTimeConverter);
        
        builder.Property(c => c.CreatedAt)
            .HasConversion(dateTimeConverter);
        
        builder.Property(c => c.UpdatedAt)
            .HasConversion(dateTimeConverter);
        
    }
}