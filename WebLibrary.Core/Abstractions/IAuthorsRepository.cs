using WebLibrary.Core.Models;

namespace WebLibrary.Core.Abstractions;

public interface IAuthorsRepository
{
    Task<Author?> GetByNameAsync(string name);

    Task<Author> GetByNameWithCreationAsync(string name);
}