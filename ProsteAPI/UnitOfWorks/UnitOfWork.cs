using ProsteAPI.Context;
using ProsteAPI.Repo.User;
using ProsteAPI.Repo.UserPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.UnitOfWorks
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IDataBaseContext _context;

        private IUserRepo _userRepo;
        private IPermitsRepo _permitsRepo;

        public UnitOfWork(
            IDataBaseContext context,
            IUserRepo userRepo,
            IPermitsRepo permitsRepo)
        {
            _context = context;
            _userRepo = userRepo;
            _permitsRepo = permitsRepo;
        }

        public IUserRepo UserRepository
        {
            get
            {
                if (_userRepo == null)
                    _userRepo = new UserRepo(_context);

                return _userRepo;
            }
        }

        public IPermitsRepo PermitsRepository
        {
            get
            {
                if (_permitsRepo == null)
                    _permitsRepo = new PermitsRepo(_context);

                return _permitsRepo;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
