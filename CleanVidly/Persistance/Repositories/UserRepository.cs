using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CleanVidlyDbContext context;

        public UserRepository(CleanVidlyDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<User> GetWithRoles(Expression<Func<User, bool>> predicate)
        {
            return context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(predicate);
        }
    }
}