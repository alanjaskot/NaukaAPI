using ProsteAPI.Entities.UserPermission;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.DTO.UserPermission
{
    public class UserPermissionToUserPermitDTO
    {
        public UserPermitDTO ConvertUserPermissionToUserPermitDTO(UserPermissionDbModel permit)
        {
            var permitDTO = new UserPermitDTO
            {
                Id = permit.Id,
                PermitName = permit.PermitName,
                UserId = permit.UserId
            };

            return permitDTO;
        }
    }
}
