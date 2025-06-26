using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebApp.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;

        [Display(Name = "Published Year")]
        public int PublishedYear { get; set; }
        public string Genre { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

