using ExpertFinder.Domain;
using ExpertFinder.GraphQL.Articles;
using ExpertFinder.GraphQL.Users;
using ExpertFinder.Infrastructure;
using ExpertFinder.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainLayer();
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

app.MapGraphQL();
app.Run();