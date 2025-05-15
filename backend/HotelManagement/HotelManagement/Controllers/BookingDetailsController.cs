using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingDetailsController : ControllerBase
    {
        private readonly HotelSQL _context;

        public BookingDetailsController(HotelSQL context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDetail>>> GetBookingDetails()
        {
            return await _context.BookingDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetail>> GetBookingDetail(int id)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound();
            }
            return bookingDetail;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDetail>> PostBookingDetail(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookingDetail), new { id = bookingDetail.BookingDetailId }, bookingDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingDetail(int id, BookingDetail bookingDetail)
        {
            if (id != bookingDetail.BookingDetailId)
            {
                return BadRequest();
            }
            _context.Entry(bookingDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BookingDetails.Any(e => e.BookingDetailId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound();
            }
            _context.BookingDetails.Remove(bookingDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
