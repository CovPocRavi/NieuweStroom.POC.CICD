using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        public UserRepository(NieuweStroomPocDbContext nieuweStroomPocDbContext) : base(nieuweStroomPocDbContext)
        {
            this.nieuweStroomPocDbContext = nieuweStroomPocDbContext;
        }

        public Task<User> GetWithRoles(Expression<Func<User, bool>> predicate)
        {
            return nieuweStroomPocDbContext.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(predicate);
        }
    }
}