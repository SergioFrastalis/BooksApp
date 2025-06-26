using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksWebApp.Core.Entities
{
    public class ReviewVote
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public bool IsUpvote { get; set; }
    }
}
