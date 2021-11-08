using ProsteAPI.Entities.User;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.DTO.User
{
    public class UserDTOToUserDbModel
    {
        private UserDTO _user;

        public UserDTOToUserDbModel(UserDTO user)
        {
            _user = user;
        }

        public UserDbModel ConvertUserToUserDbModel()
        {
            var userDbModel = new UserDbModel
            {
                Id = _user.Id,
                Login = _user.Login,
                Password = _user.Password
            };

            return userDbModel;
        }
    }
}
