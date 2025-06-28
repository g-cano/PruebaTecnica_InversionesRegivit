using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relations
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
    }
}
