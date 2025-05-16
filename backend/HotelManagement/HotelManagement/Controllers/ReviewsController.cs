using HotelManagement.DTOs;
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly HotelSQL _context;

    public ReviewsController(HotelSQL context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
    {
        return await _context.Reviews
            .Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                BookingId = r.BookingId,
                ReviewContent = r.ReviewContent,
                Rating = r.Rating
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewDTO>> GetReview(int id)
    {
        var review = await _context.Reviews
            .Where(r => r.ReviewId == id)
            .Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                BookingId = r.BookingId,
                ReviewContent = r.ReviewContent,
                Rating = r.Rating
            })
        .FirstOrDefaultAsync();

        if (review == null) return NotFound();
        return review;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, [FromBody] ReviewDTO ReviewDTO)
    {
        if (id != ReviewDTO.ReviewId) return BadRequest();

        var existingReview = await _context.Reviews.FindAsync(id);
        if (existingReview == null) return NotFound();

        // Update only allowed fields
        existingReview.UserId = ReviewDTO.UserId;
        existingReview.BookingId = ReviewDTO.BookingId;
        existingReview.ReviewContent = ReviewDTO.ReviewContent;
        existingReview.Rating = ReviewDTO.Rating;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reviews.Any(e => e.ReviewId == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }
}