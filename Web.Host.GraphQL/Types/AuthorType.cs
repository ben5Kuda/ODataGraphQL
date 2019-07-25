using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Host.Data;

namespace Web.Host.GraphQL.Types
{
  public class AuthorType : ObjectGraphType<Author>
  {
    public AuthorType()
    {
      Name = "Author";

      Field(x => x.Id, type: typeof(IdGraphType)).Description("Author's ID.");
      Field(x => x.Name).Description("The name of the Author");
      Field(x => x.Surname).Description("The surname of the Author");
      Field(x => x.Email).Description("The email of the Author");
      Field(x => x.Books, type: typeof(ListGraphType<BookType>)).Description("Author's books");
    }
  }

}
