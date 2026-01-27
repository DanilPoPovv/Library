using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.EntityFramework.Configuration
{
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.HasKey(bc => bc.Id);

            builder.Property(bc => bc.Status)
                   .IsRequired();

            builder.HasOne(bc => bc.Book)
                   .WithMany(b => b.Copies)
                   .HasForeignKey(bc => bc.BookId);
        }
    }
}
