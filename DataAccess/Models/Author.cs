using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuthor = new HashSet<BookAuthor>();
        }

        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pseudonym { get; set; }

        public virtual ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
