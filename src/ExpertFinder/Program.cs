using ExpertFinder.Data;
using ExpertFinder.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase"));
});

builder.Services.AddGraphQLServer()
    .ModifyRequestOptions(options => options.IncludeExceptionDetails = builder.Environment.IsDevelopment())
    .RegisterDbContext<ApplicationDbContext>()
    .AddType<ArticleType>()
    .AddTypeExtension<QueryType>()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

dbContext.Database.Migrate();

app.MapGraphQL();
app.Run();