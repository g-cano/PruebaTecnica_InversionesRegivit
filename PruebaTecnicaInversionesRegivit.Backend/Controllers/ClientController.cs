using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInversionesRegivit.Backend.Data;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.Client;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using PruebaTecnicaInversionesRegivit.Shared.Models;
using System.Net;

namespace PruebaTecnicaInversionesRegivit.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientGetDto>>> GetAllClients()
        {
            var clients = await _context.Clients
                .Include(c => c.CreatedBy)
                .Select(c => new ClientGetDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Identification = c.Identification,
                    CreatedAt = c.CreatedAt,
                    CreatedBy = new UserGetDto
                    {
                        Id = c.CreatedBy.Id,
                        Username = c.CreatedBy.Username,
                        Role = c.CreatedBy.Role
                    }
                })
                .ToListAsync();

            return Ok(clients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDto clientDto) // Asegúrate de usar [FromBody]
        {
            if (!ModelState.IsValid) // Esto verifica las validaciones
            {
                return BadRequest(ModelState); // Devuelve errores detallados
            }

            // 2. Mapear DTO a entidad
            var client = new Client
            {
                Name = clientDto.Name,
                Identification = clientDto.Identification,
                CreatedById = 1, // Asignar el usuario actual
                CreatedAt = DateTime.UtcNow
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok("New Client Added!");
        }
    }
}
