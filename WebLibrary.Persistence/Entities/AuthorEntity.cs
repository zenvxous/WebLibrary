namespace WebLibrary.Persistence.Entities;

public class AuthorEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public List<BookEntity> Books { get; set; } = [];
}