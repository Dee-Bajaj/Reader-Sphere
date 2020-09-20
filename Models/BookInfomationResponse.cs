using DataAccess.Models;
using System.Collections.Generic;

namespace Models
{
    public class BookInfomationResponse
    {
        public List<Book> Books { get; set; }
        public int TotalBooks { get; set; }
    }
}
