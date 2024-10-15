using WebLibrary.Core.Enums;
using WebLibrary.Core.Models;

namespace WebLibrary.Core.Abstractions;

public interface IBooksRepository
{
    Task<List<Book>> GetAllAsync();
    Task<List<Book>> GetAllByAuthorAsync(Author author);
    Task<Guid> CreateAsync(Book book);
    Task<Guid> UpdateAsync(Guid id, string title, string description, Genres genre);
    Task<Guid> DeleteAsync(Guid id);
}