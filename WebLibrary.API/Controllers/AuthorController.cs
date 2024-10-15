using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts;
using WebLibrary.Core.Abstractions;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IBooksService _booksService;
    private readonly IAuthorsService _authorsService;

    public AuthorController(IBooksService booksService, IAuthorsService authorsService)
    {
        _booksService = booksService;
        _authorsService = authorsService;
    }
    
    [HttpGet("{authorName}")]
    public async Task<ActionResult<List<BookResponse>>> GetBooksByAuthorName(string authorName)
    {
        var author = await _authorsService.GetAuthorByName(authorName);

        if (author == null)
            return NotFound();
        
        var books = await _booksService.GetAllBooksByAuthorAsync(author);
        
        var response = books.Select(b => new BookResponse(
            b.Id,
            b.Title,
            b.Description,
            b.Genre.ToString(),
            b.Author.Name));
        
        return Ok(response);
    }
}