using ProsteAPI.Entities.UserPermission;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.DTO.UserPermission
{
    public class UserPermitDTOToUserPermission
    {
        public UserPermissionDbModel ConvertToUserPermissionDbModel(UserPermitDTO permit)
        {
            var permitDbModel = new UserPermissionDbModel
            {
                Id = permit.Id,
                PermitName = permit.PermitName,
                UserId = permit.UserId
            };

            return permitDbModel;
        }
    }
}
