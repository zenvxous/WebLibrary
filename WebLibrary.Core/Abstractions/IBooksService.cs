using WebLibrary.Core.Enums;
using WebLibrary.Core.Models;

namespace WebLibrary.Core.Abstractions;

public interface IBooksService
{
    Task<List<Book>> GetAllBooksAsync();
    Task<List<Book>> GetAllBooksByAuthorAsync(Author author);
    Task<Guid> CreateBookAsync(Book book);
    Task<Guid> UpdateBookAsync(Guid id, string title, string description, Genres genre);
    Task<Guid> DeleteBookAsync(Guid id);
    Task<bool> BookExistsAsync(string title, Genres genre, Guid authorId);
}