using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using Microsoft.EntityFrameworkCore;

namespace ExpertFinder.Infrastructure.Persistence;

public class ArticleRepository : IArticleRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ArticleRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task CreateAsync(Article article)
    {
        await _applicationDbContext.AddAsync(article);
        await _applicationDbContext.SaveChangesAsync();
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