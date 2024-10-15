using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibrary.Core.Models;
using WebLibrary.Persistence.Entities;

namespace WebLibrary.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
{
    public void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        builder.HasKey(b => b.Id);
    
        builder.Property(b => b.Title)
            .HasMaxLength(Book.MAX_TITLE_LENGTH)
            .IsRequired();

        builder.Property(b => b.Description)
            .HasMaxLength(Book.MAX_DESCRIPTION_LENGTH);
        
        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books);
    }
}