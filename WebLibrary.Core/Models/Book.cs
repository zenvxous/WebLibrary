using WebLibrary.Core.Enums;

namespace WebLibrary.Core.Models;

public class Book
{
    public const int MAX_TITLE_LENGTH = 100;
    public const int MAX_DESCRIPTION_LENGTH = 500;
    private Book(Guid id, string title, string description, Genres genre, Author author)
    {
        Id = id;
        Title = title;
        Description = description;
        Genre = genre;
        AuthorId = author.Id;
        Author = author;
    }
    
    public Guid Id { get; }
    
    public string Title { get; } 
    
    public string  Description { get; } 
    
    public Genres Genre { get; } 
    
    public Guid AuthorId { get; } 
    
    public Author Author { get; } 

    public static (Book? Book, string Error) Create(Guid id, string title, string description, Genres genre, Author author)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(title) || title.Length > MAX_TITLE_LENGTH)
            error = "Title length must be less than " + MAX_TITLE_LENGTH;
        
        if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            error = "Description length must be less than " + MAX_DESCRIPTION_LENGTH;
        
        if (error != string.Empty)
            return (null, error);
        
        var book = new Book(id, title, description, genre, author);
        
        return (book, error);
    }
}