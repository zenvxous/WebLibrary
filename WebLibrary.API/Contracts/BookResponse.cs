namespace WebLibrary.API.Contracts;

public record class BookResponse(
    Guid Id,
    string Title,
    string Description,
    string Genre,
    string AuthorName);