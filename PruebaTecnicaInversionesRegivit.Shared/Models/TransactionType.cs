using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.Models
{
    public class TransactionType
    {
        public int Id { get; set; }
        public string Code { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
