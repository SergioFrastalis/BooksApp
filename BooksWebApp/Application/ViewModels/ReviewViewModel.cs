using System.ComponentModel.DataAnnotations;

namespace BooksWebApp.Application.ViewModels
{
    public class ReviewViewModel
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
