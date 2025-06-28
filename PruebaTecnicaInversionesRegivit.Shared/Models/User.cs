using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.Models
{
    public class User
    {
        public User()
        {
            ClientsCreated = new HashSet<Client>();
            AccountsCreated = new HashSet<Account>();
            TransactionsCreated = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; } 

        public bool IsActive { get; set; } = true;

        // Relaciones
        public virtual ICollection<Client> ClientsCreated { get; set; }
        public virtual ICollection<Account> AccountsCreated { get; set; }
        public virtual ICollection<Transaction> TransactionsCreated { get; set; }
    }
}
