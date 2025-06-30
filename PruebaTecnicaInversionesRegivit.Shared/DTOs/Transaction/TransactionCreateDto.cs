using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.DTOs.Transaction
{
 
public class TransactionCreateDto
    {
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeId { get; set; }
        public int AccountId { get; set; }
        public int CreatedById { get; set; }
    }
}
