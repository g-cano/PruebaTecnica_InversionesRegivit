using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Identification { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relation with User
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
