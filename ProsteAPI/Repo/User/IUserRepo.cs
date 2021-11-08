using ProsteAPI.Entities.User;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Repo.User
{
    public interface IUserRepo
    {
        public Response<List<UserDbModel>> GetAll();
        public Response<UserDbModel> GetByLoginAndPassword(string login, string password);
        public Response<UserDbModel> GetById(Guid id);
        public Response<Guid> Add(UserDbModel user);
        public Response<Guid> Update(UserDbModel user);
        public Response<string> UpdatePassword(Guid userId, string password);
        public Response<Guid> Delete(UserDbModel user);

    }
}
