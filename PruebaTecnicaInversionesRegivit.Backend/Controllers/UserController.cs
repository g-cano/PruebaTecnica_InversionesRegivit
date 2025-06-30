using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaInversionesRegivit.Backend.Data;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using PruebaTecnicaInversionesRegivit.Shared.Models;

namespace PruebaTecnicaInversionesRegivit.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;


        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto request)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);

            if (user == null)
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    Message = "Usuario o contraseña incorrectos"
                });
            }

            // En producción, nunca devolver la contraseña
            var userResponse = new UserGetDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Autenticación exitosa",
                User = userResponse
            });
        }
    }
}
