using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EntityFramework.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(b => b.Author)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(b => b.Genre)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.HasMany(x => x.Copies)
                    .WithOne(x => x.Book)
                    .HasForeignKey(x => x.BookId);
                    
        }
    }
}
