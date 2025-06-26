namespace BooksWebApp.Application.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
