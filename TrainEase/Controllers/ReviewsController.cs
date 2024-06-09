using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TrainEase.Data;
using TrainEase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todas las reseñas")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews
                .Include(r => r.Trainer)
                .Include(r => r.Client)
                .ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene una reseña por ID")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.Trainer)
                .Include(r => r.Client)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // POST: api/Reviews
        [HttpPost]
        [SwaggerOperation(Summary = "Crea una nueva reseña")]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            // Verifica que el cliente y el entrenador existan
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == review.ClientId);
            var trainerExists = await _context.Trainers.AnyAsync(t => t.Id == review.TrainerId);

            if (!clientExists || !trainerExists)
            {
                return BadRequest("Cliente o entrenador no existe.");
            }

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza una reseña por ID")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            // Verifica que el cliente y el entrenador existan
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == review.ClientId);
            var trainerExists = await _context.Trainers.AnyAsync(t => t.Id == review.TrainerId);

            if (!clientExists || !trainerExists)
            {
                return BadRequest("Cliente o entrenador no existe.");
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Elimina una reseña por ID")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
