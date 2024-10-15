using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibrary.Persistence.Entities;

namespace WebLibrary.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Name)
            .IsRequired();  
        
        builder
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(a => a.AuthorId);
    }
}