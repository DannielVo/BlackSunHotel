using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly HotelSQL _context;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(HotelSQL context, ILogger<ReviewsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponseDTO>>> GetReviews()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Select(r => new ReviewResponseDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    UserFullname = r.User.Fullname,
                    BookingId = r.BookingId,
                    ReviewContent = r.ReviewContent,
                    Rating = r.Rating
                })
                .ToListAsync();
        }

        // GET: api/reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReview(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.ReviewId == id)
                .Select(r => new ReviewResponseDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    UserFullname = r.User.Fullname,
                    BookingId = r.BookingId,
                    ReviewContent = r.ReviewContent,
                    Rating = r.Rating
                })
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    StatusCode = 404,
                    Message = "Review not found"
                });
            }
            return review;
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<ActionResult<ReviewResponseDTO>> CreateReview([FromBody] CreateReviewDTO createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    StatusCode = 400,
                    Message = "Invalid review data"
                });
            }

            // Validate user and booking
            var userExists = await _context.Users.AnyAsync(u => u.UserId == createDto.UserId);
            var bookingExists = await _context.Bookings.AnyAsync(b => b.BookingId == createDto.BookingId);

            if (!userExists || !bookingExists)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    StatusCode = 400,
                    Message = "User or Booking does not exist"
                });
            }

            try
            {
                var review = new Review
                {
                    UserId = createDto.UserId,
                    BookingId = createDto.BookingId,
                    ReviewContent = createDto.ReviewContent,
                    Rating = createDto.Rating
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
            }
            catch (DbUpdateException ex)
            {
                // Log the full error (critical for debugging)
                _logger.LogError(ex, "Database save error: {Message}", ex.InnerException?.Message);
                return StatusCode(500, new ErrorResponseDTO
                {
                    StatusCode = 500,
                    Message = "Failed to save review. Check logs for details."
                });
            }
        }

        // DELETE: api/reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound(new ErrorResponseDTO
                {
                    StatusCode = 404,
                    Message = "Review not found"
                });
            }

            try
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed to delete review {id}", id);
                return StatusCode(500, new ErrorResponseDTO
                {
                    StatusCode = 500,
                    Message = "Failed to delete review"
                });
            }

            return NoContent();
        }
    }
}