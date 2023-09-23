using ExpertFinder.GraphQL.Articles;
using ExpertFinder.GraphQL.Users;
using ExpertFinder.Infrastructure;
using ExpertFinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddGraphQLServer()
    .ModifyRequestOptions(options => options.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .RegisterDbContext<ApplicationDbContext>()
    .AddType<ArticleType>()
    .AddType<UserType>()
    .AddQueryType()
    .AddTypeExtension<ArticleQueries>()
    .AddTypeExtension<UserQueries>()
    .AddMutationType()
    .AddTypeExtension<ArticleMutations>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

dbContext.Database.Migrate();

app.MapGraphQL();
app.Run();