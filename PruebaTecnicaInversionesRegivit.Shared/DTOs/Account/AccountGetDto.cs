using PruebaTecnicaInversionesRegivit.Shared.DTOs.Client;
using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.DTOs.Account
{
    public class AccountGetDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }    
        public int ClientId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserGetDto CreatedBy { get; set; }
        public ClientGetDto Client { get; set; }
            
             
    }
}
