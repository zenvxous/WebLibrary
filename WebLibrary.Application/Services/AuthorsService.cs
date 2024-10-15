using WebLibrary.Core.Abstractions;
using WebLibrary.Core.Models;

namespace WebLibrary.Application.Services;

public class AuthorsService : IAuthorsService
{
    private readonly IAuthorsRepository _authorsRepository;

    public AuthorsService(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }

    public async Task<Author?> GetAuthorByName(string name)
    {
        return await _authorsRepository.GetByNameAsync(name);
    }

    public async Task<Author> GetAuthorWithCreationByName(string name)
    {
        return await _authorsRepository.GetByNameWithCreationAsync(name);
    }
    
}