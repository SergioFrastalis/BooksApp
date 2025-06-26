using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksWebApp.Core.Entities;
using BooksWebApp.Core.Interfaces;
using BooksWebApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksWebApp.Infrastructure.Repositories
{
    public class ReviewVoteRepository : IReviewVoteRepository
    {
        private readonly AppDbContext _context;

        public ReviewVoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReviewVote?> GetVoteAsync(int reviewId, string userId)
        {
            return await _context.ReviewVotes
                .FirstOrDefaultAsync(v => v.ReviewId == reviewId && v.UserId == userId);
        }

        public async Task AddAsync(ReviewVote vote)
        {
            await _context.ReviewVotes.AddAsync(vote);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Remove(ReviewVote vote)
        {
            _context.ReviewVotes.Remove(vote);
        }

    }
}
