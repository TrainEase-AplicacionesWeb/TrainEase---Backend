using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainEase.Data;
using TrainEase.Models;

namespace TrainEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtiene todos los trainers", Description = "Devuelve una lista de todos los trainers disponibles.")]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
            return await _context.Trainers.ToListAsync();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtiene un trainer por ID", Description = "Devuelve un trainer específico según su ID.")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            return trainer;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crea un nuevo trainer", Description = "Añade un nuevo trainer a la base de datos.")]
        public async Task<ActionResult<Trainer>> PostTrainer(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTrainer), new { id = trainer.Id }, trainer);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualiza un trainer", Description = "Actualiza los detalles de un trainer existente.")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            if (id != trainer.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(id))
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
        [SwaggerOperation(Summary = "Elimina un trainer", Description = "Elimina un trainer existente de la base de datos.")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.Id == id);
        }
    }
}
