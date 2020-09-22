using System.Collections.Generic;

namespace Models
{
    public class BookInfomationResponse
    {
        public List<Book> Books { get; set; }
        public int TotalBooks { get; set; }
    }

    public class FindBookRequest
    {
        public string Title { get; set; }
        public Writer Author { get; set; }
        public GenreType Genre { get; set; }
        public string Publisher { get; set; }
    }

    public enum GenreType
    {
        None,
        Drama,
        Romance,
        Horror,
        Comedy,
        RomCom,
        Thriller,
        Suspense,
        Action,
        ScienceFiction,
        Motivational,
        Mythology,
        FairyTale,
        Paranormal,
        SelfHelp,
        Epic,
        ShortStory,
        Comic,
        Biography
    }

    public class Writer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pseudonym { get; set; }
    }
}
