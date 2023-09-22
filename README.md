# Expert finder example application

This repository demonstrates how to use embeddings and a vector database to build an expert finder.

## Introduction

Finding an expert on certain knowledge can be hard for organizations. This is where a good expert finding algorithm
comes into play. In this example, you'll find an expert finder that matches experts to articles based on the content
that the experts wrote.

The process has two parts to it. First, we need to build an expert profile based on the content written by the
expert. This part of the process has two steps:

- First, when an article gets published it's encoded using an embedding model and stored in the vector database.
- Next, we'll take all the embeddings from the published articles of the expert and dot multiply them so that we get
  the embedding profile for the expert.

Next time we display an article, we take the embedding vector for the article and find the K nearest embedding vectors
for the article in the authors vector database. These authors are considered experts for the purpose of the expert finder.

## Getting started

Prepare the application by creating a `.env` file with the following content:

```shell
SQL_PASSWORD=<your-password>
```

After creating the `.env` file, navigate to the `src/ExpertFinder` folder and use the following command to set up
a connection to the SQL server instance:

```shell
dotnet user-secrets set "ConnectionStrings:DefaultDatabase" "data source=127.0.0.1;initial catalog=expertfinder;user id=sa;password=<your-password>;trust server certificate=true"
```

This will configure the connection string. Next, start the databases using the following command:

```shell
docker compose up -d
```

After starting the databases, start the application by running the following command from the `src/ExpertFinder` directory:

```shell
dotnet run
```

The application uses GraphQL to make querying various pieces of information easier. You can access the GraphQL interface
going to `http://localhost:5229/graphql`. It will show you a GraphQL editor to make queries against the application.

## Developing

This section covers how to modify the code to suit your own needs.

### Application structure

The structure for the application looks like this:

```plaintext
+---src
    +---ExpertFinder
        +---Data            Contains the Database context class
        +---GraphQL
        |   +---Articles    Contains the queries and mutations for articles
        |   +---Users       Contains the queries and mutations for users
        +---Migrations      Contains the database migrations
        +---Models          Contains the application models
```
