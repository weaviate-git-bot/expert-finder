﻿namespace ExpertFinder.GraphQL.Articles;

public class PublishArticleInput
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}
