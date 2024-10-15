namespace WebLibrary.API.Contracts;

public record class BookRequest(
    string Title,
    string Description,
    string Genre,
    string AuthorName);