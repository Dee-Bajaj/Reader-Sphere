using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Book
    {
        public Book()
        {
            BookAuthor = new HashSet<BookAuthor>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public decimal? CriticReview { get; set; }
        public DateTime? ReviewDate { get; set; }
        public bool ReviewAdded { get; set; }
        public string ShortReview { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PublishYear { get; set; }

        public virtual ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
