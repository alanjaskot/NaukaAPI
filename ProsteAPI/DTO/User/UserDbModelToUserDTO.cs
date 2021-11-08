using ProsteAPI.Entities.User;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.DTO.User
{
    public class UserDbModelToUserDTO
    {
        private UserDbModel _user;

        public UserDbModelToUserDTO(UserDbModel user)
        {
            _user = user;
        }

        public UserDTO ConvertUserToUserDTO()
        {
            var userDTO = new UserDTO
            {
                Id = _user.Id,
                Login = _user.Login,
                Password = _user.Password
            };

            return userDTO;
        }
    }
}
