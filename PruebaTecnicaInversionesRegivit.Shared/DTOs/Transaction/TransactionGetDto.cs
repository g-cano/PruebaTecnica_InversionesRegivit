using PruebaTecnicaInversionesRegivit.Shared.DTOs.Account;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.TransactionType;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.DTOs.Transaction
{
    public class TransactionGetDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime RecordDate {get;set;}
        public decimal ResultingBalance { get; set; }
        public int TransactionTypeId { get; set; } 
        public TransactionTypeDto TransactionType { get; set; }
        public int AccountId { get; set; } 
        public AccountGetDto Account { get; set; }
        public int CreatedById { get; set; }
        public UserGetDto CreatedBy { get; set; }
    } 
}
