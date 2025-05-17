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

    [HttpPost("add")]
    public async Task<ActionResult<ReviewDTO>> AddReview([FromBody] ReviewDTO reviewDto)
    {
        var review = new Review
        {
            UserId = reviewDto.UserId,
            BookingId = reviewDto.BookingId,
            ReviewContent = reviewDto.ReviewContent,
            Rating = reviewDto.Rating
        };
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, reviewDto);
    }
}