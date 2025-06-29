using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.DTOs.Client
{
    public class ClientCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Identification { get; set; }
    }
}
