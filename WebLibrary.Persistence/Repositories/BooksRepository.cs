using Microsoft.EntityFrameworkCore;
using WebLibrary.Core.Abstractions;
using WebLibrary.Core.Enums;
using WebLibrary.Core.Models;
using WebLibrary.Persistence.Entities;

namespace WebLibrary.Persistence.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly ApplicationDbContext _context;

    public BooksRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Book>> GetAllAsync()
    {
        var bookEntities = await _context.Books
            .AsNoTracking()
            .Include(b => b.Author) 
            .ToListAsync();

        var authors = new Dictionary<Guid, Author>();

        var books = bookEntities
            .Select(b => 
            {
                if (!authors.ContainsKey(b.Author.Id))
                {
                    authors[b.Author.Id] = Author.Create(
                        b.Author.Id, 
                        b.Author.Name, 
                        new List<Book>()
                    ).Author;
                }

                authors[b.Author.Id].Books.Add(Book.Create(
                    b.Id, 
                    b.Title, 
                    b.Description, 
                    b.Genre, 
                    authors[b.Author.Id]
                ).Book!);
                
                

                return Book.Create(
                    b.Id, 
                    b.Title, 
                    b.Description, 
                    b.Genre, 
                    authors[b.Author.Id]
                ).Book!;
            })
            .ToList();

        return books;
    }

    public async Task<List<Book>> GetAllByAuthorAsync(Author author)
    {
        var bookEntities = await _context.Books
            .AsNoTracking()
            .Where(a => a.AuthorId == author.Id)
            .Include(b => b.Author) 
            .ToListAsync();

        var authors = new Dictionary<Guid, Author>();

        var books = bookEntities
            .Select(b => 
            {
                if (!authors.ContainsKey(b.Author.Id))
                {
                    authors[b.Author.Id] = Author.Create(
                        b.Author.Id, 
                        b.Author.Name, 
                        new List<Book>()
                    ).Author;
                }

                authors[b.Author.Id].Books.Add(Book.Create(
                    b.Id, 
                    b.Title, 
                    b.Description, 
                    b.Genre, 
                    authors[b.Author.Id]
                ).Book!);
                
                

                return Book.Create(
                    b.Id, 
                    b.Title, 
                    b.Description, 
                    b.Genre, 
                    authors[b.Author.Id]
                ).Book!;
            })
            .ToList();

        return books;
    }

    public async Task<Guid> CreateAsync(Book book)
    {
        var authorEntity = await _context.Authors
            .FirstOrDefaultAsync(a => a.Id == book.Author.Id);
        
        var bookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Genre = book.Genre,
            AuthorId = authorEntity.Id,
            Author = authorEntity 
        };
        
        authorEntity.Books.Add(bookEntity);
        
        await _context.Books.AddAsync(bookEntity);
        await _context.SaveChangesAsync();

        return bookEntity.Id; 
    }

    public async Task<Guid> UpdateAsync(Guid id, string title, string description, Genres genre)
    {
        await _context.Books
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, title)
                .SetProperty(b => b.Description, description)
                .SetProperty(b => b.Genre, genre));
        
        return id;
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        await _context.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}