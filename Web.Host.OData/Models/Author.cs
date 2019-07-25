using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Host.OData.Models
{
  public class Author
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public virtual IEnumerable<Book> Books { get; set; }
  }
}
