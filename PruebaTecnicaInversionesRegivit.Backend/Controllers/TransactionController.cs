using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaInversionesRegivit.Backend.Data;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.Account;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.Transaction;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.TransactionType;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using PruebaTecnicaInversionesRegivit.Shared.Models;

namespace PruebaTecnicaInversionesRegivit.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;


        public TransactionController(AppDbContext context)
        {
            _context = context;
        }

      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionGetDto>>> GetAllTransactions()
        {
            var allTransactions = await _context.Transactions
                .Include(t => t.TransactionType)
                .Include(t => t.CreatedBy)
                .Include(t => t.Account)
                .Select(t => new TransactionGetDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    RecordDate = t.RecordDate,
                    ResultingBalance = t.ResultingBalance,
                    CreatedBy = new UserGetDto
                    {
                        Id = t.CreatedBy.Id,
                        Username = t.CreatedBy.Username,
                        Role = t.CreatedBy.Role
                    },
                    Account = new AccountGetDto
                    {
                        Id= t.Account.Id,
                        AccountNumber = t.Account.AccountNumber,
                        Balance = t.Account.Balance
                    },
                    TransactionType = new TransactionTypeDto
                    {
                        Id = t.TransactionType.Id,
                        Name = t.TransactionType.Name,
                        Code = t.TransactionType.Code
                    }

                }).ToListAsync();
            return Ok(allTransactions);
        }

        [HttpPost("{accountId}")]
        public async Task<ActionResult<IEnumerable<TransactionCreateDto>>> CreateTransaction(int accountId, [FromBody] TransactionCreateDto transactionDto)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                throw new ArgumentException("La cuenta que ha ingresado no existe!");
            }

            var transaction = await _context.Database.BeginTransactionAsync();

            if (transactionDto.TransactionTypeId == 1)
            {
                try
                {
                    account.Balance += transactionDto.Amount;
                    var newTransaction = new Transaction
                    {
                        AccountId = accountId,
                        Amount = transactionDto.Amount,
                        TransactionTypeId = 1,
                        TransactionDate = transactionDto.TransactionDate,
                        RecordDate = DateTime.UtcNow,
                        ResultingBalance = account.Balance,
                        CreatedById = 1
                    };
                    _context.Transactions.Add(newTransaction);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok("Se ha depositado correctamente!");

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;

                }

            } else if (transactionDto.TransactionTypeId == 2)
            {

                if (account.Balance <= transactionDto.Amount)
                    return BadRequest("Saldo insuficiente");

                // Validar límite diario
                var dailyWithdrawals = await GetDailyWithdrawals(accountId);
                if (dailyWithdrawals + transactionDto.Amount > 5000)
                    throw new InvalidOperationException("Excede el límite diario de retiros (USD 5,000)");

                try
                {
                    account.Balance -= transactionDto.Amount;
                    var newTransaction = new Transaction
                    {
                        AccountId = accountId,
                        Amount = transactionDto.Amount,
                        TransactionTypeId = 2,
                        TransactionDate = transactionDto.TransactionDate,
                        RecordDate = DateTime.UtcNow,
                        CreatedById = 1,
                        ResultingBalance = account.Balance
                    };

                    _context.Transactions.Add(newTransaction);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok("Se ha retirado exitosamente!");
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            } else 
            {
                throw new ArgumentException("No se puede realizar otra accion!");
            }

           
        }

        [HttpGet("{accountId}/account")]
        public async Task<ActionResult<IEnumerable<TransactionGetDto>>> GetByAccountIdTransactions(int accountId)
        {
            var allTransactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .Include(t => t.TransactionType)
                .Include(t => t.CreatedBy)
                .Include(t => t.Account)
                .Select(t => new TransactionGetDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate,
                    RecordDate = t.RecordDate,
                    ResultingBalance = t.ResultingBalance,
                    CreatedBy = new UserGetDto
                    {
                        Id = t.CreatedBy.Id,
                        Username = t.CreatedBy.Username,
                        Role = t.CreatedBy.Role
                    },
                    Account = new AccountGetDto
                    {
                        Id = t.Account.Id,
                        AccountNumber = t.Account.AccountNumber,
                        Balance = t.Account.Balance
                    },
                    TransactionType = new TransactionTypeDto
                    {
                        Id = t.TransactionType.Id,
                        Name = t.TransactionType.Name,
                        Code = t.TransactionType.Code
                    }

                }).ToListAsync();
            return Ok(allTransactions);
        }

        private async Task<decimal> GetDailyWithdrawals(int accountId)
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Transactions
                .Where(t => t.Id == accountId &&
                           t.TransactionTypeId == 2 &&
                           t.RecordDate >= today)
                .SumAsync(t => t.Amount);
        }
    }
}
