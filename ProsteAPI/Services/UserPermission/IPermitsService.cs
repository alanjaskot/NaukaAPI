using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Services.UserPermission
{
    public interface IPermitsService
    {
        public Response<List<UserPermitDTO>> GetAllPermits();
        public Response<List<UserPermitDTO>> GetAllPermitsByUser(Guid userId);
        public Response<UserPermitDTO> GetPermitById(Guid id);
        public Response<UserPermitDTO> SetPermitPermitForUser(Guid userId, string permitName);
        public Response<UserPermitDTO> DeleteUserPermit(UserPermitDTO permit);
    }
}
