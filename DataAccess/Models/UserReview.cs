namespace DataAccess.Models
{
    public partial class UserReview
    {
        public int BookId { get; set; }
        public decimal? UserReview1 { get; set; }
        public int ReviewCount { get; set; }
        public virtual Book Book { get; set; }
    }
}
