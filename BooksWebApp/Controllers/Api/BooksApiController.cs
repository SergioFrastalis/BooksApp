using BooksWebApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BooksWebApp.Core.Entities;
using BooksWebApp.Application.DTOs;

namespace BooksWebApp.Web.Controllers.Api
{
    [ApiController]
    [Route("api/books")]
    public class BooksApiController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public BooksApiController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(
            [FromQuery] string? genre,
            [FromQuery] int? year,
            [FromQuery] double? minRating)
        {
            if (genre != null || year.HasValue || minRating.HasValue)
            {
                var filtered = await _bookRepo.GetFilteredAsync(genre, year, minRating);
                return Ok(filtered);
            }

            var books = await _bookRepo.ListAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("{id}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetBookReviews(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            var reviews = book.Reviews?
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Content = r.Content,
                    Rating = r.Rating,
                    DateCreated = r.DateCreated,
                    UserName = r.User?.UserName ?? "Unknown"
                })
                .ToList() ?? new List<ReviewDto>();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                PublishedYear = dto.PublishedYear,
                Genre = dto.Genre
            };

            await _bookRepo.AddAsync(book);
            await _bookRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }


    }
}
