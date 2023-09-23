using ExpertFinder.Domain.Aggregates.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace ExpertFinder.Infrastructure.Persistence;

public class ArticleEntityTypeConfiguration: IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.Property<byte[]>("Version").IsRowVersion();
        
        builder.Property(x => x.Embedding).HasConversion(
            v => String.Join(";", v.Select(x => x.ToString(CultureInfo.InvariantCulture))),
            v => v.Split(';', StringSplitOptions.None).Select(float.Parse).ToArray());
    }
}