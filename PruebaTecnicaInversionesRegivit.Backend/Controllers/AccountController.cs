using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInversionesRegivit.Backend.Data;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.Account;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.Client;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using PruebaTecnicaInversionesRegivit.Shared.Models;

namespace PruebaTecnicaInversionesRegivit.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        /*
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
         */

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountGetDto>>> GetAllAccounts()
        {
            var accounts = await _context.Accounts
        .Include(a => a.Client)          // Carga la relación con Client
        .Include(a => a.CreatedBy)       // Carga la relación con User (CreatedBy)
        .OrderBy(a => a.AccountNumber)
        .Select(a => new AccountGetDto
        {
            Id = a.Id,
            AccountNumber = a.AccountNumber,
            Balance = a.Balance,
            CreatedAt = a.CreatedAt,
            Client = new ClientGetDto
            {
                Id = a.Client.Id,
                Name = a.Client.Name,
                Identification = a.Client.Identification
            },
            CreatedBy = new UserGetDto
            {
                Id = a.CreatedBy.Id,
                Username = a.CreatedBy.Username,
                Role = a.CreatedBy.Role
            },
            /*Transactions = a.Transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                TransactionDate = t.TransactionDate,
                Type = t.TransactionType.Name
            }).ToList()*/
        })
        .ToListAsync();
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto accountDto)
        {
            
            string accountNumber;
            bool isUnique;
            var random = new Random();
            do
            {
                // Generar número aleatorio (ejemplo: REG-12345678)
                accountNumber = $"REG-{random.Next(10000000, 99999999)}";

                // Verificar si ya existe
                isUnique = !await _context.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
            }
            while (!isUnique);
            var account = new Account
            {
                AccountNumber = accountNumber,
                Balance = 0.00m,
                ClientId = accountDto.ClientId,
                CreatedById = 1,
                CreatedAt = DateTime.UtcNow,

            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return Ok(account);
        }

    }
}
