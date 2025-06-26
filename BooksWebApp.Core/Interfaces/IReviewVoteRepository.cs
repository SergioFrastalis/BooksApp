using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksWebApp.Core.Entities;

namespace BooksWebApp.Core.Interfaces
{
    public interface IReviewVoteRepository
    {
        Task<ReviewVote?> GetVoteAsync(int reviewId, string userId);
        Task AddAsync(ReviewVote vote);
        void Remove(ReviewVote vote);

        Task SaveChangesAsync();
    }
}
