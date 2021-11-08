using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Services.User
{
    public interface IUserService
    {
        public Response<List<UserDTO>> GetAllUsers();
        public Response<UserDTO> GetUserByLoginAndPassword(string login, string password);
        public Response<UserDTO> GetUserById(Guid id);
        public Response<Guid> AddUser(UserDTO user);
        public Response<Guid> UpdateUser(UserDTO user);
        public Response<string> ChangeUserPassword(Guid userId, string password);
        public Response<Guid> DeleteUser(UserDTO user);
    }
}
