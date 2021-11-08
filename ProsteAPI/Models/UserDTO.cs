using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
