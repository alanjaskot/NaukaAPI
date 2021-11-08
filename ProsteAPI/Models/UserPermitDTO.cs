using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Models
{
    public class UserPermitDTO
    {
        public Guid Id { get; set; }
        public string PermitName { get; set; }
        public Guid UserId { get; set; }
    }
}
