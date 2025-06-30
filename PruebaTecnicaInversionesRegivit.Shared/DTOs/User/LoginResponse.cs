using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaInversionesRegivit.Shared.DTOs.User
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserGetDto User { get; set; }
    }
}
