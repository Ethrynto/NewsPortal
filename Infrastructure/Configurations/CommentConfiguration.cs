﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasOne<User>(e => e.User)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.UserId);
        
        builder.HasOne<Post>(e => e.Post)
            .WithMany(e => e.Comments)
            .HasForeignKey(e => e.PostId);
        
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(c => c.CreatedAt)
            .HasConversion(dateTimeConverter);
        
    }
    
}