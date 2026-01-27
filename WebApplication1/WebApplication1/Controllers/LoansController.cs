using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/loans")]
[Authorize]
public class LoansController : ControllerBase
{
    private readonly LoanService _loanService;
    public LoansController(LoanService loanService)
    {
        _loanService = loanService;
    }
    [HttpPost("issue")]
    [Authorize(Roles = "Admin,Librarian")]
    public IActionResult Issue(IssueBookRequest request)
    {
        var success = _loanService.IssueBook(request.UserId, request.BookCopyId);
        if (!success)
            return BadRequest("Невозможно выдать книгу");

        return Ok();
    }
    [HttpPost("return")]
    public IActionResult Return(ReturnBookRequest request)
    {
        var success = _loanService.ReturnBook(request.BookId, request.UserId);
        if (!success)
            return BadRequest("Невозможно вернуть книгу");

        return Ok();
    }

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "Admin,Librarian")]
    public IActionResult GetUserLoans(int userId)
    {
        var loans = _loanService.GetUserLoans(userId);
        return Ok(loans);
    }
}
