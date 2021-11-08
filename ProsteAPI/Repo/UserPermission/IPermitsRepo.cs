using ProsteAPI.Entities.UserPermission;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Repo.UserPermission
{
    public interface IPermitsRepo
    {
        public Response<List<UserPermissionDbModel>> GetAll();
        public Response<List<UserPermissionDbModel>> GetAllByUser(Guid userId);
        public Response<UserPermissionDbModel> GetById(Guid id);
        public Response<UserPermissionDbModel> SetPermitForUser(Guid userId, string permitName);
        public Response<UserPermissionDbModel> DeleteUserPermit(UserPermissionDbModel permit);
    }
}
