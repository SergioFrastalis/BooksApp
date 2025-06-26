namespace BooksWebApp.Application.DTOs
{
    public class CreateReviewDto
    {
        public int BookId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
