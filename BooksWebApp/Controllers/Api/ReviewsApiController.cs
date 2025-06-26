using BooksWebApp.Application.DTOs;
using BooksWebApp.Core.Entities;
using BooksWebApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebApp.Web.Controllers.Api
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize] 
    public class ReviewsApiController : ControllerBase
    {
        private readonly IReviewVoteRepository _reviewVoteRepo;
        private readonly IReviewRepository _reviewRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsApiController(
            IReviewRepository reviewRepo,
            IReviewVoteRepository reviewVoteRepo,
            UserManager<ApplicationUser> userManager)
        {
            _reviewRepo = reviewRepo;
            _reviewVoteRepo = reviewVoteRepo;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] CreateReviewDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5 || string.IsNullOrWhiteSpace(dto.Content))
                return BadRequest("Invalid input.");

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var review = new Review
            {
                BookId = dto.BookId,
                Content = dto.Content,
                Rating = dto.Rating,
                DateCreated = DateTime.UtcNow,
                UserId = user.Id
            };

            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveChangesAsync();

            return Ok(new { message = "Review created." });
        }

        [HttpPost("{id}/vote")]
        [Authorize]
        public async Task<IActionResult> VoteOnReview(int id, [FromBody] ReviewVoteDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var existingVote = await _reviewVoteRepo.GetVoteAsync(id, user.Id);
            if (existingVote != null)
            {
                return BadRequest("You have already voted on this review.");
            }

            var vote = new ReviewVote
            {
                ReviewId = id,
                UserId = user.Id,
                IsUpvote = dto.IsUpvote
            };

            await _reviewVoteRepo.AddAsync(vote);
            await _reviewVoteRepo.SaveChangesAsync();

            return Ok(new { message = "Vote recorded." });
        }


    }
}
