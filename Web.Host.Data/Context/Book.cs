using System;
using System.Collections.Generic;

namespace Web.Host.Data
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
