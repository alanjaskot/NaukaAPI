using ProsteAPI.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProsteAPI.Entities.UserPermission
{
    public class UserPermissionDbModel
    {
        public Guid Id { get; set; }
        public string PermitName { get; set; }
       
        public Guid UserId { get; set; }
        [JsonIgnore]
        public UserDbModel User { get; set; }
    }
}
