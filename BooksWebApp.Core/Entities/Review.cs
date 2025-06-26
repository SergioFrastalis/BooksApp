using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebApp.Core.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int Rating { get; set; }  // e.g. 1–5 stars
        public DateTime DateCreated { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public ICollection<ReviewVote> Votes { get; set; } = new List<ReviewVote>();
    }
}
