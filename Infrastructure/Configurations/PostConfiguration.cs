using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(e => e.Content)
            .IsRequired();
        
        builder.HasMany<Comment>(e => e.Comments)
            .WithOne(e => e.Post)
            .HasForeignKey(e => e.PostId);
        
        builder.HasOne<Category>(e => e.Category)
            .WithMany(e => e.Posts)
            .HasForeignKey(e => e.CategoryId);
        
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(c => c.CreatedAt)
            .HasConversion(dateTimeConverter);

    }
    
}