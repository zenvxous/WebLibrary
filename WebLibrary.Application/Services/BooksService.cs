using WebLibrary.Core.Abstractions;
using WebLibrary.Core.Enums;
using WebLibrary.Core.Models;

namespace WebLibrary.Application.Services;

public class BooksService : IBooksService
{
    private readonly IBooksRepository _booksRepository;

    public BooksService(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        var books = await _booksRepository.GetAllAsync();

        return books.OrderBy(b => b.Author.Name).ToList();
    }

    public async Task<List<Book>> GetAllBooksByAuthorAsync(Author author)
    {
        var books = await _booksRepository.GetAllByAuthorAsync(author);
        
        return books.OrderBy(b => b.Title).ToList();
    }

    public async Task<Guid> CreateBookAsync(Book book)
    {
        return await _booksRepository.CreateAsync(book);
    }

    public async Task<Guid> UpdateBookAsync(Guid id, string title, string description, Genres genre)
    {
        return await _booksRepository.UpdateAsync(id, title, description, genre);
    }

    public async Task<Guid> DeleteBookAsync(Guid id)
    {
        return await _booksRepository.DeleteAsync(id);
    }

    public async Task<bool> BookExistsAsync(string title, Genres genre, Guid authorId)
    {
        var books = await _booksRepository.GetAllAsync();
        
        var book = books.FirstOrDefault(b => b.Title == title && b.Genre == genre && b.AuthorId == authorId);
        
        return book != null;
    }
}