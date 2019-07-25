using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Host.Data;
using Web.Host.GraphQL.Types;

namespace Web.Host.GraphQL.Queries
{
  public class AuthorQuery : ObjectGraphType
  {
    public AuthorQuery(ODataGraphQLDbContext dbContext)
    {
      Field<AuthorType>(
        "Author",
        arguments: new QueryArguments(
          new QueryArgument<IdGraphType> { Name = "id", Description = "The ID of the Author." }),
        resolve: context =>
        {
          var id = context.GetArgument<int>("id");
          var author = dbContext
            .Author
            .Include(a => a.Books)
            .FirstOrDefault(i => i.Id == id);
          return author;
        });

      Field<ListGraphType<AuthorType>>(
        "Authors",
        resolve: context =>
        {
          var authors = dbContext.Author.Include(a => a.Books);
          return authors;
        });
    }
  }
}

