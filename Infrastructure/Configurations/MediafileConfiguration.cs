using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(c => c.CreatedAt)
            .HasConversion(dateTimeConverter);
        
    }
    
}