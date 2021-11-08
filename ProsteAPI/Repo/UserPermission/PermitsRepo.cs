using Microsoft.EntityFrameworkCore;
using ProsteAPI.Context;
using ProsteAPI.Entities.UserPermission;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Repo.UserPermission
{
    public class PermitsRepo: IPermitsRepo
    {
        private readonly IDataBaseContext _context;

        public PermitsRepo(IDataBaseContext context)
        {
            _context = context;
        }

        public Response<List<UserPermissionDbModel>> GetAll()
        {
            var result = default(List<UserPermissionDbModel>);
            try
            {
                result = _context.Permits.AsNoTracking().ToList();
                if (result == null)
                    return new Response<List<UserPermissionDbModel>>
                    {
                        Success = false,
                        Message = "brak rekordów dla uprawnień"
                    };

                return new Response<List<UserPermissionDbModel>>
                {
                    Success = true,
                    Message = null,
                    Object = result
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<List<UserPermissionDbModel>> GetAllByUser(Guid userId)
        {
            if (userId == Guid.Empty)
                return new Response<List<UserPermissionDbModel>>
                {
                    Success = false,
                    Message = "Nie ma obiektu o podanym Id"
                };
            try
            {
                var user = _context.Users.AsNoTracking()
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                if (user == null)
                    return new Response<List<UserPermissionDbModel>>
                    {
                        Success = false,
                        Message = $"użytkownik o podanym Id {userId} nie istnieje"
                    };

                var permits = _context.Permits.AsNoTracking()
                    .Where(p => p.UserId == userId).ToList();
                if (permits == null)
                    return new Response<List<UserPermissionDbModel>>
                    {
                        Success = true,
                        Message = $"użytkownik o id {userId} nie posiada żadnych uprawnień"
                    };

                return new Response<List<UserPermissionDbModel>>
                {
                    Success = true,
                    Message = null,
                    Object = permits
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<UserPermissionDbModel> GetById(Guid id)
        {
            var permit = default(UserPermissionDbModel);

            if (id == Guid.Empty)
                return new Response<UserPermissionDbModel>
                {
                    Success = false,
                    Message = "Podany identyfikator nie istnieje"
                };

            try
            {
                permit = _context.Permits.AsNoTracking()
                    .Where(p => p.Id == id)
                    .FirstOrDefault();
                if (permit == null)
                    return new Response<UserPermissionDbModel>
                    {
                        Success = false,
                        Message = "Uprawnienie o danym id nie istnieje"
                    };

                return new Response<UserPermissionDbModel>
                {
                    Success = true,
                    Message = null,
                    Object = permit
                };
            }
            catch
            {
                throw;
            }
        }      

        public Response<UserPermissionDbModel> SetPermitForUser(Guid userId, string permitName)
        {
            try
            {
                var user = _context.Users.AsNoTracking()
                    .Where(u => u.Id == userId)
                    .FirstOrDefault();

                if (user == null)
                    return new Response<UserPermissionDbModel>
                    {
                        Success = false,
                        Message = $"użytkownik o podanym id {userId} nie istnieje"
                    };
                
                var permit = new UserPermissionDbModel
                {
                    Id = Guid.NewGuid(),
                    PermitName = permitName,
                    UserId = userId
                };

                var add = _context.Permits.Add(permit);
                if(add.State == EntityState.Added)
                {
                    return new Response<UserPermissionDbModel>
                    {
                        Success = true,
                        Message = null,
                        Object = permit
                    };
                }

                return new Response<UserPermissionDbModel>
                {
                    Success = false,
                    Message = "Dodanie uprawnień zostało zakończone niepowodzeniem"
                };
            }
            catch
            {
                throw;
            }
        }


        public Response<UserPermissionDbModel> DeleteUserPermit(UserPermissionDbModel permit)
        {
            try
            {
                if (permit == null)
                    return new Response<UserPermissionDbModel>
                    {
                        Success = false,
                        Message = $"uprawnienie o podanym id {permit.Id} nie istnieje"
                    };

                var delet = _context.Permits.Add(permit);
                if (delet.State == EntityState.Added)
                {
                    return new Response<UserPermissionDbModel>
                    {
                        Success = true,
                        Message = null,
                        Object = permit
                    };
                }

                return new Response<UserPermissionDbModel>
                {
                    Success = false,
                    Message = "Dodanie uprawnień zostało zakończone niepowodzeniem"
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
