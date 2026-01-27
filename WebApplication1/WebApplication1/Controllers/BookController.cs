using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Requests;
using WebApplication1.Services;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_bookService.GetAll());
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = _bookService.GetById(id);
        if (book == null)
            return NotFound();

        return Ok(book);
    }
    [HttpPost]
    [Authorize(Roles = "Admin,Librarian")]
    public IActionResult Create(CreateBookRequest createBookRequest)
    {
        _bookService.Add(createBookRequest);
        return Ok();
    }
    [HttpPut("{id}")]
    [Authorize]
    public IActionResult Update(int id, BookUpdateRequest bookUpdateRequest)
    {
        bool success = _bookService.Update(id,bookUpdateRequest);
        if (!success)
            return NotFound();
        return NoContent();
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        _bookService.Delete(id);
        return NoContent();
    }
    [HttpPost("{id}/copies")]
    [Authorize(Roles = "Admin,Librarian")]
    public IActionResult AddCopies(int id, AddBookCopiesRequest request)
    {
        if (request.Count <= 0)
            return BadRequest("Количество должно быть больше 0");

        var success = _bookService.AddCopies(id, request.Count);
        if (!success)
            return NotFound("Книга не найдена");

        return Ok();
    }
    [HttpGet("user/{userId}")]
    public IActionResult GetAllUserBooks(int userId) 
    {
        var books = _bookService.GetAllUserBooks(userId);
        return Ok(books);
    }
}
