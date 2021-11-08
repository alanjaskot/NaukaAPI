using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Auth
{
    public interface IJwtAuthenticationToken
    {
        public string Authenticate(Guid id, string userName);
    }
}
