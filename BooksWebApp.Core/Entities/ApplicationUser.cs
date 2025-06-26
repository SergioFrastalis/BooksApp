using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BooksWebApp.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<ReviewVote> Votes { get; set; } = new List<ReviewVote>();
    }
}
