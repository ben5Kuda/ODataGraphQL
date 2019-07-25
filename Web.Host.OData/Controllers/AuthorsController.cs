using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Host.OData.Models;

namespace Web.Host.OData.Controllers
{
  public class AuthorsController : ODataController
  {
    private readonly Data.ODataGraphQLDbContext _dbContext;

    public AuthorsController(Data.ODataGraphQLDbContext dbContext) => _dbContext = dbContext;

    [EnableQuery(PageSize = 20)]
    public IQueryable<Author> Get()
    {
      return Query();
    }
  
    [EnableQuery]
    public IQueryable<Author> Get(int key)
    {
      return Query().Where(x => x.Id == key);
    }

    private IQueryable<Author> Query()
    {
      var authors = from a in _dbContext.Author.Include(b => b.Books)
                    select new Author
                    {
                      Id = a.Id,
                      Name = a.Name,
                      Email = a.Email,
                      Surname = a.Surname,
                      Books = (from b in a.Books
                               select new Book
                               {
                                 Id = b.Id,
                                 AuthorId = b.AuthorId,
                                 Genre = b.Genre,
                                 Name = b.Name,
                                 Published = b.Published
                               }).AsQueryable()
                    };

      return authors;
      
    }


  }
}
