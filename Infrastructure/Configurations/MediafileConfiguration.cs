using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class MediafileConfiguration : IEntityTypeConfiguration<Mediafile>
{
    public void Configure(EntityTypeBuilder<Mediafile> builder)
    {
        builder.ToTable("Mediafiles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.FileName)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(e => e.FileType)
            .IsRequired()
            .HasMaxLength(255);
        
    }
    
}