using WebLibrary.Core.Enums;

namespace WebLibrary.Persistence.Entities;

public class BookEntity
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public Genres Genre { get; set; }
    
    public Guid AuthorId { get; set; }
    
    public AuthorEntity Author { get; set; }
}