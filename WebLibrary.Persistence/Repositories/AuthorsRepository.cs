using Microsoft.EntityFrameworkCore;
using WebLibrary.Core.Abstractions;
using WebLibrary.Core.Models;
using WebLibrary.Persistence.Entities;

namespace WebLibrary.Persistence.Repositories;

public class AuthorsRepository : IAuthorsRepository
{
    private readonly ApplicationDbContext _context;

    public AuthorsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Author> GetByNameWithCreationAsync(string name)
    {
        var authorEntity = await _context.Authors
            .AsNoTracking()
            .Where(a => a.Name == name)
            .Include(authorEntity => authorEntity.Books)
            .FirstOrDefaultAsync();

        if (authorEntity == null)
        {
            authorEntity = new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            await _context.Authors.AddAsync(authorEntity);
            await _context.SaveChangesAsync();
        }
        
        var author = Author.Create(authorEntity.Id, authorEntity.Name, []).Author;
        
        foreach (var bookEntity in authorEntity.Books)
        {
            var book = Book.Create(
                bookEntity.Id, 
                bookEntity.Title, 
                bookEntity.Description,
                bookEntity.Genre, 
                author).Book;
            
            author.Books.Add(book);
        }
        
        return author;
    }

    public async Task<Author?> GetByNameAsync(string name)
    {
        var authorEntity = await _context.Authors
            .AsNoTracking()
            .Where(a => a.Name == name)
            .Include(authorEntity => authorEntity.Books)
            .FirstOrDefaultAsync();

        if (authorEntity != null)
        {
            var author = Author.Create(authorEntity.Id, authorEntity.Name, []).Author;
        
            foreach (var bookEntity in authorEntity.Books)
            {
                var book = Book.Create(
                    bookEntity.Id, 
                    bookEntity.Title, 
                    bookEntity.Description,
                    bookEntity.Genre, 
                    author).Book;
            
                author.Books.Add(book);
            }
            
            return author;
        }

        return null;
    }
    
}