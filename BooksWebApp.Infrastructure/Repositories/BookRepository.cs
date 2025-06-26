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
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<IEnumerable<Book>> ListAllAsync()
        {
            return await _context.Books.Include(b => b.Reviews).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.Votes) 
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)  
                .FirstOrDefaultAsync(b => b.Id == id);
        }



        public async Task<IEnumerable<Book>> GetFilteredAsync(string? genre, int? year, double? minRating)
        {
            var query = _context.Books.Include(b => b.Reviews).AsQueryable();

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre == genre);

            if (year.HasValue)
                query = query.Where(b => b.PublishedYear == year.Value);

            if (minRating.HasValue)
                query = query.Where(b =>
                    b.Reviews.Any() &&
                    b.Reviews.Average(r => r.Rating) >= minRating.Value);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
