namespace ExpertFinder.Models;

public class Article
{
    public Article()
    {
        
    }

    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public User Author { get; set; } = default!;
}
