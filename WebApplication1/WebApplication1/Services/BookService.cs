using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Copies)
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books
                .Include(b => b.Copies)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(CreateBookRequest createBookRequest)
        {
            Book book = new Book
            {
                Title = createBookRequest.Name,
                Author = createBookRequest.Author,
                Genre = createBookRequest.Genre
            };
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public bool Update(int bookId, BookUpdateRequest book)
        {
            var existing = _context.Books.Find(bookId);
            if (existing == null)
                return false;
            existing.Title = book.Name;
            existing.Author = book.Author;
            existing.Genre = book.Genre;
            existing.Year = book.Year;

            _context.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var book = _context.Books
                .Include(b => b.Copies)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
                return;

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        public bool AddCopies(int bookId, int count)
        {
            var book = _context.Books.Find(bookId);
            if (book == null)
                return false;

            for (int i = 0; i < count; i++)
            {
                _context.BookCopies.Add(new BookCopy
                {
                    BookId = bookId,
                    Status = Status.Available
                });
            }

            _context.SaveChanges();
            return true;
        }
        public List<Book> GetAllUserBooks(int userId)
        {
            var loans = _context.Loans.Where(l => l.UserId == userId)
                .Include(l => l.BookCopy)
                .ThenInclude(bc => bc.Book)
                .Select(l => l.BookCopy.Book)
                .ToList();
            if (loans == null)
                return new List<Book>();
            return loans;
        }
    }
}
