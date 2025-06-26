using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksWebApp.Core.Entities;

namespace BooksWebApp.Core.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review?> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByBookIdAsync(int bookId);
        Task AddAsync(Review review);
        Task SaveChangesAsync();
    }
}
