using ProsteAPI.DTO.UserPermission;
using ProsteAPI.Models;
using ProsteAPI.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Services.UserPermission
{
    public class PermitService: IPermitsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<UserPermitDTO> DeleteUserPermit(UserPermitDTO permit)
        {
            try
            {
                var converter = new UserPermitDTOToUserPermission();
                var permissionDbModel = converter.ConvertToUserPermissionDbModel(permit);

                var repoResponce = _unitOfWork.PermitsRepository.DeleteUserPermit(permissionDbModel);
                if (!repoResponce.Success)
                    return new Response<UserPermitDTO>
                    {
                        Success = false,
                        Message = repoResponce.Message
                    };
                else
                {
                    var save = _unitOfWork.SaveChanges();
                    if (save > -1)
                        return new Response<UserPermitDTO>
                        {
                            Success = true,
                            Message = null,
                            Object = permit
                        };
                    else
                        return new Response<UserPermitDTO>
                        {
                            Success = false,
                            Message = "błąd zapisu"
                        };
                }
            }
            catch
            {
                throw;
            }
        }

        public Response<List<UserPermitDTO>> GetAllPermits()
        {
            var list = default(List<UserPermitDTO>);
            try
            {
                var repoResponse = _unitOfWork.PermitsRepository.GetAll();
                if (!repoResponse.Success)
                    return new Response<List<UserPermitDTO>>
                    {
                        Success = false,
                        Message = repoResponse.Message
                    };
                else
                {
                    foreach (var userPermit in repoResponse.Object)
                    {
                        var permitToConvert = new UserPermissionToUserPermitDTO();
                        var permitConverted = permitToConvert.ConvertUserPermissionToUserPermitDTO(userPermit);
                        list.Add(permitConverted);
                    }
                    return new Response<List<UserPermitDTO>>
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

        public Response<List<UserPermitDTO>> GetAllPermitsByUser(Guid userId)
        {
            var list = default(List<UserPermitDTO>);
            try
            {
                var repoResponse = _unitOfWork.PermitsRepository.GetAllByUser(userId);
                if (!repoResponse.Success)
                    return new Response<List<UserPermitDTO>>
                    {
                        Success = false,
                        Message = repoResponse.Message
                    };
                else
                {
                    foreach (var userPermit in repoResponse.Object)
                    {
                        var permitToConvert = new UserPermissionToUserPermitDTO();
                        var permitConverted = permitToConvert.ConvertUserPermissionToUserPermitDTO(userPermit);
                        list.Add(permitConverted);
                    }
                    return new Response<List<UserPermitDTO>>
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

        public Response<UserPermitDTO> GetPermitById(Guid id)
        {
            try
            {
                var repoResponse = _unitOfWork.PermitsRepository.GetById(id);
                if (!repoResponse.Success)
                    return new Response<UserPermitDTO>
                    {
                        Success = repoResponse.Success,
                        Message = repoResponse.Message
                    };
                else
                {
                    var permitToConvert = new UserPermissionToUserPermitDTO();
                    var permitConverted = permitToConvert.ConvertUserPermissionToUserPermitDTO(repoResponse.Object);
                    return new Response<UserPermitDTO>
                    {
                        Success = true,
                        Message = null,
                        Object = permitConverted
                    };
                }
            }
            catch
            {
                throw;
            }
        }

        public Response<UserPermitDTO> SetPermitPermitForUser(Guid userId, string permitName)
        {
            try
            {

                var repoResponce = _unitOfWork.PermitsRepository.SetPermitForUser(userId, permitName);
                if (!repoResponce.Success)
                    return new Response<UserPermitDTO>
                    {
                        Success = false,
                        Message = repoResponce.Message
                    };
                else
                {
                    var converter = new UserPermissionToUserPermitDTO();
                    var permissionDTO = converter.ConvertUserPermissionToUserPermitDTO(repoResponce.Object);

                    var save = _unitOfWork.SaveChanges();
                    if (save > -1)
                        return new Response<UserPermitDTO>
                        {
                            Success = true,
                            Message = null,
                            Object = permissionDTO
                        };
                    else
                        return new Response<UserPermitDTO>
                        {
                            Success = false,
                            Message = "błąd zapisu"
                        };
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
