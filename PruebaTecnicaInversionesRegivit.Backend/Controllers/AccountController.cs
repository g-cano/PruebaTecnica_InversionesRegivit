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
            AccountName = a.AccountName,
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

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AccountGetDto>>> GetAccount(int id)
        {
            var account = await _context.Accounts
            .Where(a => a.Id == id)
            .Include(a => a.Client)          // Carga la relación con Client
            .Include(a => a.CreatedBy)       // Carga la relación con User (CreatedBy)
            .OrderBy(a => a.AccountNumber)
            .Select(a => new AccountGetDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                AccountName = a.AccountName,
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
            })
            .FirstOrDefaultAsync(a => a.Id == id);

            return Ok(account);
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
            if (accountDto.AccountName != null) account.AccountName = accountDto.AccountName;
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return Ok(account);
        }

    }
}
