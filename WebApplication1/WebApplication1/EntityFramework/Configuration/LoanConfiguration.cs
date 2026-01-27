using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LoanDate)
               .IsRequired();

        builder.HasOne(x => x.BookCopy)
               .WithMany()
               .HasForeignKey(x => x.BookCopyId);

        builder.HasOne(x => x.User)
               .WithMany(x => x.Loans)
               .HasForeignKey(x => x.UserId);
    }
}
