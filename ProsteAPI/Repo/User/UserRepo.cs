using Microsoft.EntityFrameworkCore;
using ProsteAPI.Context;
using ProsteAPI.Entities.User;
using ProsteAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Repo.User
{
    public class UserRepo: IUserRepo
    {
        private readonly IDataBaseContext _context;

        public UserRepo(IDataBaseContext context)
        {
            _context = context;
        }

        public Response<List<UserDbModel>> GetAll()
        {
            var result = default(List<UserDbModel>);
            try
            {
                result = _context.Users.AsNoTracking().ToList();
                if (result == null)
                    return new Response<List<UserDbModel>>
                    {
                        Success = false,
                        Message = "baza nie zawiera żadnego rekordu"
                    };

                return new Response<List<UserDbModel>>
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

        public Response<UserDbModel> GetById(Guid id)
        {
            var user = default(UserDbModel);
            try
            {
                user = _context.Users.AsNoTracking()
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
                if (user == null)
                    return new Response<UserDbModel>
                    {
                        Success = false,
                        Message = $"Brak użytkownika z przydzielonym Id {id}."
                    };
                return new Response<UserDbModel>
                {
                    Success = true,
                    Message = null,
                    Object = user
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<UserDbModel> GetByLoginAndPassword(string login, string password)
        {
            if (login == null || login == ""
                || password == null || password == "")
                return new Response<UserDbModel>
                {
                    Success = false,
                    Message = "Login i hasło nie mogą być puste"
                };
            var user = default(UserDbModel);
            try
            {
                user = _context.Users.AsNoTracking()
                    .Where(u => u.Login == login && u.Password == password)
                    .FirstOrDefault();

                if (user == null)
                    return new Response<UserDbModel>
                    {
                        Success = false,
                        Message = "Użytkownik o podanym login i haśle nie istnieje"
                    };

                return new Response<UserDbModel>
                {
                    Success = true,
                    Message = null,
                    Object = user
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<Guid> Update(UserDbModel user)
        {
            if(user == null)
                return new Response<Guid>
                {
                    Success = false,
                    Message = "Wprowadzono pusty obiekt",
                };
            try
            {
                var update = _context.Users.Update(user);
                if (update.State == EntityState.Modified)
                    return new Response<Guid>
                    {
                        Success = true,
                        Message = null,
                    };

                return new Response<Guid>
                {
                    Success = false,
                    Message = "modyfikacja zakończona niepowodzeniem"
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<string> UpdatePassword(Guid userId, string password)
        {
            if (password == null || password == "")
                return new Response<string>
                {
                    Success = false,
                    Message = "Hasło nie może być puste",
                };

            var user = default(UserDbModel);
            try
            {
                user = GetById(userId).Object;
                user.Password = password;

                var update = _context.Users.Update(user);

                if (update.State == EntityState.Modified)
                    return new Response<string>
                    {
                        Success = true,
                        Message = null,
                        Object = password
                    };

                return new Response<string>
                {
                    Success = false,
                    Message = "modyfikacja zakończona niepowodzeniem"
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<Guid> Add(UserDbModel user)
        {
            if (user.Id == Guid.Empty)
                user.Id =Guid.NewGuid();               

            if (user == null)
                return new Response<Guid>
                {
                    Success = false,
                    Message = "Wprowadzony użytkownik jest pusty",
                };
            try
            {
                var add = _context.Users.Add(user);

                if (add.State == EntityState.Added)
                    return new Response<Guid>
                    {
                        Success = true,
                        Message = null,
                        Object = user.Id
                    };

                return new Response<Guid>
                {
                    Success = false,
                    Message = "rejestracja zakończona niepowodzeniem"
                };
            }
            catch
            {
                throw;
            }
        }

        public Response<Guid> Delete(UserDbModel user)
        {
            if (user == null)
                return new Response<Guid>
                {
                    Success = false,
                    Message = "obiekt jest pusty"
                };
            try
            {
                var delete = _context.Users.Remove(user);
                if (delete.State == EntityState.Deleted)
                    return new Response<Guid>
                    {
                        Success = true,
                        Message = null,
                        Object = user.Id
                    };

                return new Response<Guid>
                {
                    Success = false,
                    Message = "Usuwanie zakończone niepowodzeniem"
                };
            }
            catch
            {
                throw;
            }
        }

       
    }
}
