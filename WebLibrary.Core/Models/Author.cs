namespace WebLibrary.Core.Models;

public class Author
{
    private Author(Guid id, string name, List<Book> books)
    {
        Id = id;
        Name = name;
        Books = books;
    }
    public Guid Id { get; }
    
    public string Name { get; }
    
    public List<Book> Books { get; set; } = [];

    public static (Author Author, string Error) Create(Guid id, string name, List<Book> books)
    {
        var error = string.Empty;
        
        if(string.IsNullOrEmpty(name))
            error = "Name is required";

        var author = new Author(id, name, books);
        
        return (author, error);
    }
}