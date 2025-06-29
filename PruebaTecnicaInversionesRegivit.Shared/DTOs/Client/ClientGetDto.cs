using PruebaTecnicaInversionesRegivit.Shared.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.DTOs.Client
{
    public class ClientGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public DateTime CreatedAt { get; set; }

        // Información del creador
        public UserGetDto CreatedBy { get; set; }
    }

}
