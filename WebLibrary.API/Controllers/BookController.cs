using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Contracts;
using WebLibrary.Core.Abstractions;
using WebLibrary.Core.Enums;
using WebLibrary.Core.Models;

namespace WebLibrary.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{ 
    private readonly IBooksService _booksService;
    private readonly IAuthorsService _authorsService;

    public BookController(IBooksService booksService, IAuthorsService authorsService)
    {
        _booksService = booksService;
        _authorsService = authorsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponse>>> GetBooks()
    {
        var books = await _booksService.GetAllBooksAsync();

        var response = books.Select(b => new BookResponse(
            b.Id,
            b.Title,
            b.Description,
            b.Genre.ToString(),
            b.Author.Name));
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid >> CreateBook([FromBody]BookRequest request)
    {
        var author = await _authorsService.GetAuthorWithCreationByName(request.AuthorName);
        if (!Enum.TryParse<Genres>(request.Genre, out var genre))
        {
            return BadRequest("Invalid genre");
        }
        
        var (book, error) = Book.Create(
            Guid.NewGuid(), 
            request.Title, 
            request.Description, 
            genre,
            author);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        if (await _booksService.BookExistsAsync(book.Title, book.Genre, book.Author.Id))
        {
            return Conflict("Book already exists");
        }
            
        var bookId = await _booksService.CreateBookAsync(book);
        
        return Ok(bookId);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BookRequest request)
    {
        if (request.Title.Length > Book.MAX_TITLE_LENGTH)
            return BadRequest("Title length must be less than " + Book.MAX_TITLE_LENGTH);

        if (request.Description.Length > Book.MAX_DESCRIPTION_LENGTH)
            return BadRequest("Description length must be less than " + Book.MAX_DESCRIPTION_LENGTH);
        
        if (!Enum.TryParse<Genres>(request.Genre, out var genre))
        {
            return BadRequest("Invalid genre");
        }
        
        var bookId = await _booksService.UpdateBookAsync(id, request.Title, request.Description, genre);
        
        return Ok(bookId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteBook(Guid id)
    {
        return Ok(await _booksService.DeleteBookAsync(id));
    }
}