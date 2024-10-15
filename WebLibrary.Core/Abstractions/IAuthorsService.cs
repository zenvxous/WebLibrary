using WebLibrary.Core.Models;

namespace WebLibrary.Core.Abstractions;

public interface IAuthorsService
{
    Task<Author?> GetAuthorByName(string name);

    Task<Author> GetAuthorWithCreationByName(string name);
}