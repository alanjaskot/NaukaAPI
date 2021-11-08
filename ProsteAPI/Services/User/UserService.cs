using ProsteAPI.DTO.User;
using ProsteAPI.Models;
using ProsteAPI.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Services.User
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<Guid> AddUser(UserDTO user)
        {
            try
            {
                var userToConvert = new UserDTOToUserDbModel(user);
                var userConverted = userToConvert.ConvertUserToUserDbModel();
                var repoResponse = _unitOfWork.UserRepository.Add(userConverted);
                if (repoResponse.Success)
                {
                    var save = _unitOfWork.SaveChanges();
                    if (save > -1)
                    {
                        return repoResponse;
                    }
                    else
                        return new Response<Guid>
                        {
                            Success = false,
                            Message = "błąd w trakcie zapisu danych"
                        };
                }
                else
                    return repoResponse;
            }
            catch
            {
                throw;
            }
        }

        public Response<string> ChangeUserPassword(Guid userId, string password)
        {
            try
            {
                var repoResponse = _unitOfWork.UserRepository.UpdatePassword(userId, password);
                if (repoResponse.Success)
                {
                    var save = _unitOfWork.SaveChanges();
                    if (save > -1)
                    {
                        return repoResponse;
                    }
                    else
                        return new Response<string>
                        {
                            Success = false,
                            Message = "błąd w trakcie zapisu danych"
                        };
                }
                else
                    return repoResponse;
            }
            catch
            {
                throw;
            }
        }

        public Response<Guid> DeleteUser(UserDTO user)
        {
            try
            {
                var userToConvert = new UserDTOToUserDbModel(user);
                var userConverted = userToConvert.ConvertUserToUserDbModel();
                var repoResponse = _unitOfWork.UserRepository.Delete(userConverted);
                if (repoResponse.Success)
                {
                    var save = _unitOfWork.SaveChanges();
                    if (save > -1)
                    {
                        return repoResponse;
                    }
                    else
                        return new Response<Guid>
                        {
                            Success = false,
                            Message = "błąd w trakcie zapisu danych"
                        };
                }
                else
                    return repoResponse;
            }
            catch
            {
                throw;
            }
        }

        public Response<List<UserDTO>> GetAllUsers()
        {
            var list = default(List<UserDTO>);
            try
            {
                var repoResponse = _unitOfWork.UserRepository.GetAll();
                if (!repoResponse.Success)
                    return new Response<List<UserDTO>>
                    {
                        Success = false,
                        Message = repoResponse.Message
                    };
                else
                {
                    foreach (var user in repoResponse.Object)
                    {
                        var userToConvert = new UserDbModelToUserDTO(user);
                        var userConverted = userToConvert.ConvertUserToUserDTO();
                        list.Add(userConverted);
                    }
                    return new Response<List<UserDTO>>
                    {
                        Success = true,
                        Message = null,
                        Object = list
                    };
                }
                
            }
            catch
            {
                throw;
            }
        }

        public Response<UserDTO> GetUserById(Guid id)
        {
            try
            {
                var repoResponse = _unitOfWork.UserRepository.GetById(id);

                if (!repoResponse.Success)
                    return new Response<UserDTO>
                    {
                        Success = false,
                        Message = repoResponse.Message
                    };
                else
                {
                    var userToConvert = new UserDbModelToUserDTO(repoResponse.Object);
                    var userConverted = userToConvert.ConvertUserToUserDTO();

                    return new Response<UserDTO>
                    {
                        Success = true,
                        Message = null,
                        Object = userConverted
                    };
                }
            }
            catch
            {
                throw;
            }
        }

        public Response<UserDTO> GetUserByLoginAndPassword(string login, string password)
        {
            try
            {
                var repoResponse = _unitOfWork.UserRepository.GetByLoginAndPassword(login, password);

                if (!repoResponse.Success)
                    return new Response<UserDTO>
                    {
                        Success = false,
                        Message = repoResponse.Message
                    };
                else
                {
                    var userToConvert = new UserDbModelToUserDTO(repoResponse.Object);
                    var userConverted = userToConvert.ConvertUserToUserDTO();

                    return new Response<UserDTO>
                    {
                        Success = true,
                        Message = null,
                        Object = userConverted
                    };
                }
            }
            catch
            {
                throw;
            }
        }

        public Response<Guid> UpdateUser(UserDTO user)
        {
            try
            {
                var userToConvert = new UserDTOToUserDbModel(user);
                var userConverted = userToConvert.ConvertUserToUserDbModel();
                var repoResponse = _unitOfWork.UserRepository.Update(userConverted);
                if (repoResponse.Success)
                {
                    var save = _unitOfWork.SaveChanges();
                    if (save > -1)
                    {
                        return repoResponse;
                    }
                    else
                        return new Response<Guid>
                        {
                            Success = false,
                            Message = "błąd w trakcie zapisu danych"
                        };
                }
                else
                    return repoResponse;
            }
            catch
            {
                throw;
            }
        }
    }
}
