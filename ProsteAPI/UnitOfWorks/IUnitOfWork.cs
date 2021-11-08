using ProsteAPI.Repo.User;
using ProsteAPI.Repo.UserPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.UnitOfWorks
{
    public interface IUnitOfWork
    {
        public IUserRepo UserRepository { get; }
        public IPermitsRepo PermitsRepository { get; }

        public int SaveChanges();
    }
}
