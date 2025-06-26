using BooksWebApp.Application.ViewModels;
using BooksWebApp.Core.Entities;
using BooksWebApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksWebApp.Web.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IReviewVoteRepository _reviewVoteRepo;
        private readonly UserManager<ApplicationUser> _userManager;


        public ReviewsController(
            IReviewRepository reviewRepo,
            IBookRepository bookRepo,
            IReviewVoteRepository reviewVoteRepo,
            UserManager<ApplicationUser> userManager)
        {
            _reviewRepo = reviewRepo;
            _bookRepo = bookRepo;
            _userManager = userManager;
            _reviewVoteRepo = reviewVoteRepo;
        }

        // GET: Reviews/Create?bookId=1
        public async Task<IActionResult> Create(int bookId)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book == null) return NotFound();

            ViewBag.BookTitle = book.Title;

            var vm = new ReviewViewModel
            {
                BookId = book.Id
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel vm)
        {
            if (vm.Rating < 1 || vm.Rating > 5)
                ModelState.AddModelError("Rating", "Rating must be between 1 and 5.");

            if (string.IsNullOrWhiteSpace(vm.Content))
                ModelState.AddModelError("Content", "Content is required.");

            if (!ModelState.IsValid)
            {
                ViewBag.BookTitle = (await _bookRepo.GetByIdAsync(vm.BookId))?.Title;
                return View(vm);
            }

            var user = await _userManager.GetUserAsync(User);

            var review = new Review
            {
                BookId = vm.BookId,
                Content = vm.Content,
                Rating = vm.Rating,
                DateCreated = DateTime.UtcNow,
                UserId = user!.Id
            };

            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveChangesAsync();

            return RedirectToAction("Details", "Books", new { id = vm.BookId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Vote(int id, bool isUpvote)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var existingVote = await _reviewVoteRepo.GetVoteAsync(id, userId);

            if (existingVote != null)
            {
                if (existingVote.IsUpvote == isUpvote)
                {
                    // Toggle: remove the vote if it's the same
                    _reviewVoteRepo.Remove(existingVote);
                }
                else
                {
                    // Switch: update the existing vote
                    existingVote.IsUpvote = isUpvote;
                }
            }
            else
            {
                var vote = new ReviewVote
                {
                    ReviewId = id,
                    UserId = userId,
                    IsUpvote = isUpvote
                };

                await _reviewVoteRepo.AddAsync(vote);
            }

            await _reviewVoteRepo.SaveChangesAsync();

            var review = await _reviewRepo.GetByIdAsync(id);
            return RedirectToAction("Details", "Books", new { id = review?.BookId });
        }

    }
}
