using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksWebApp.Core.Entities;

namespace BooksWebApp.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> ListAllAsync();

        Task<IEnumerable<Book>> GetFilteredAsync(string? genre, int? year, double? minRating);
        Task AddAsync(Book book);
        Task SaveChangesAsync();
        
    }
}
