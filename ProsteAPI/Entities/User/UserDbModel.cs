using ProsteAPI.Entities.UserPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProsteAPI.Entities.User
{
    public class UserDbModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public List<UserPermissionDbModel> Permits { get; set; }
    }
}
