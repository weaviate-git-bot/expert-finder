using ExpertFinder.Data;
using ExpertFinder.Models;

namespace ExpertFinder.GraphQL.Users;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field("likes").Resolve(context =>
        {
            var user = context.Parent<User>();
            var dbContext = context.Services.GetRequiredService<ApplicationDbContext>();

            return dbContext.Likes.Count(x => x.Article.Author.Id == user.Id);
        });
    }
}