using ExpertFinder.Data;
using ExpertFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.GraphQL;

public class AuthorResolver
{
    public User GetAuthor(Guid id, [Service]ApplicationDbContext dbContext)
    {
        return dbContext.Articles.Include(x=>x.Author).Single(x => x.Id == id).Author;
    }
}
