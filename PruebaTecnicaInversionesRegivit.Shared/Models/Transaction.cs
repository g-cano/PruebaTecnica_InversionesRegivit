using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ResultingBalance { get; set; }

        // Relaciones
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
