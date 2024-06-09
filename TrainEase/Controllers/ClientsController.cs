using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Asegúrate de tener esta directiva
using Swashbuckle.AspNetCore.Annotations;
using TrainEase.Data;
using TrainEase.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrainEase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clients
        [HttpGet]
        [SwaggerOperation(Summary = "Get all clients", Description = "Returns a list of all clients")]
        [SwaggerResponse(200, "Success", typeof(IEnumerable<Client>))]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return _context.Clients.ToList();
        }

        // GET: api/clients/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get client by ID", Description = "Returns a client by their ID")]
        [SwaggerResponse(200, "Success", typeof(Client))]
        [SwaggerResponse(404, "Client not found")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        // POST: api/clients
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new client", Description = "Creates a new client and returns the created client")]
        [SwaggerResponse(201, "Client created", typeof(Client))]
        [SwaggerResponse(400, "Bad request")]
        public ActionResult<Client> PostClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        // PUT: api/clients/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a client", Description = "Updates a client's details by their ID")]
        [SwaggerResponse(200, "Client updated")]
        [SwaggerResponse(404, "Client not found")]
        public IActionResult PutClient(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clients.Any(e => e.Id == id))
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

        // DELETE: api/clients/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a client", Description = "Deletes a client by their ID")]
        [SwaggerResponse(204, "Client deleted")]
        [SwaggerResponse(404, "Client not found")]
        public IActionResult DeleteClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
