using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework;
using WebApplication1.Models;

public class LoanService
{
    private readonly LibraryDbContext _context;

    public LoanService(LibraryDbContext context)
    {
        _context = context;
    }

    public bool IssueBook(int userId, int bookId)
    {
        var user = _context.Users.Find(userId);
        if (user == null)
            return false; 

        var copy = _context.BookCopies.FirstOrDefault(c => c.BookId == bookId && c.Status == Status.Available);
        if (copy == null || copy.Status != Status.Available)
            return false; 

        var loan = new Loan
        {
            Id = Guid.NewGuid().ToString(),
            UserId = userId,
            BookCopyId = copy.Id,
            LoanDate = DateTime.UtcNow,
            ReturnDate = null
        };

        copy.Status = Status.Issued;

        _context.Loans.Add(loan);
        _context.SaveChanges();

        return true;
    }

    public bool ReturnBook(int bookId, int userId)
    {
        var loan = _context.Loans
            .Include(l => l.BookCopy)
            .FirstOrDefault(l =>
                l.UserId == userId &&
                l.ReturnDate == null &&
                l.BookCopy.BookId == bookId);
        if (loan == null)
            return false; 

        loan.ReturnDate = DateTime.UtcNow;

        var copy = _context.BookCopies.Find(bookId);
        if (copy != null)
            copy.Status = Status.Available;

        _context.SaveChanges();
        return true;
    }

    public List<Loan> GetUserLoans(int userId)
    {
        return _context.Loans
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.LoanDate)
            .ToList();
    }
}
