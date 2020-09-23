using System;
using System.Collections.Generic;

namespace Models
{
    public class BookInfomation
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
    public class AddBookRequest
    {
        public string BookName { get; set; }
        public GenreType Genre { get; set; }
        public string Publisher { get; set; }
        public decimal? Price { get; set; }
        public string PublishYear { get; set; }
        public Writer Author { get; set; }
        public Review Review { get; set; }
    }

    public class AddBookResponse
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Unkwown,
        BookAdded,
        AlreadyExisting
    }
    public class UpdateBookRequest : AddBookRequest
    {
        public int BookId { get; set; }
    }

    public class Review
    {
        public decimal? CriticReview { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string ShortReview { get; set; }
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

    public class BookInfo
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }
        public Review Review { get; set; }
        public decimal? Price { get; set; }
        public string PublishYear { get; set; }
        public bool IsReviewAdded { get; set; }
        public Writer Author { get; set; }
    }
}
