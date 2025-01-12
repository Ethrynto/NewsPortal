using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasMany<Post>(e => e.Posts)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId);
        
    }
    
}