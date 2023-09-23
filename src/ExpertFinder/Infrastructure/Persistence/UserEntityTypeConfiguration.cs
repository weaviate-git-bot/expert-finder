using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using ExpertFinder.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace ExpertFinder.Infrastructure.Persistence;

public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany<Article>().WithOne().HasForeignKey(x=>x.AuthorId).OnDelete(DeleteBehavior.Cascade);

        builder.Property<byte[]>("Version").IsRowVersion();

        builder.Property(x => x.ExpertiseEmbedding).HasConversion(
            v => String.Join(";", v.Select(x => x.ToString(CultureInfo.InvariantCulture))),
            v => v.Split(';', StringSplitOptions.None).Select(float.Parse).ToArray());

        // The embedding size is 1536 because we're using the OpenAI embedding model 
        var emptyEmbedding = Enumerable.Repeat(0.0f, 1536).ToArray();
        
        builder.HasData(
            new User(Guid.Parse("aa828b23-96fa-4cb0-8d22-5e89a10ba3cd")) { FullName = "Willem Meints", ExpertiseEmbedding = emptyEmbedding },
            new User(Guid.Parse("8ba4ba14-bbd0-4f7c-a85a-90725f2bd93f")) { FullName = "Joop Snijder",  ExpertiseEmbedding = emptyEmbedding },
            new User(Guid.Parse("bef98bd4-795c-46b7-9295-427de33cb6a6")) { FullName = "Emiel Stoelinga", ExpertiseEmbedding = emptyEmbedding },
            new User(Guid.Parse("589ee3c2-4e59-48fb-a5ca-24dd5960d30a")) { FullName = "Rowan Terinathe" , ExpertiseEmbedding = emptyEmbedding },
            new User(Guid.Parse("20b18172-f06b-454b-bd48-3c5573fc4a1c")) { FullName = "Lucia Conde Moreno",  ExpertiseEmbedding = emptyEmbedding }
        );
    }
}