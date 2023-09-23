using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Services;
using ExpertFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Infrastructure.Services;

public class ContentManager: IContentManager
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ContentManager(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Article?> GetArticleByIdAsync(Guid articleId)
    {
        return await _applicationDbContext.Articles.SingleOrDefaultAsync(x => x.Id == articleId);
    }

    public async Task<IReadOnlyList<Article>> GetArticlesByAuthorId(Guid authorId)
    {
        return await _applicationDbContext.Articles.Where(x => x.AuthorId == authorId).ToListAsync();
    }
}