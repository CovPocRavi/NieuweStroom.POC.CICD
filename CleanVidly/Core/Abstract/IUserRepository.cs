using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NieuweStroom.POC.IT.Core.Entities;

namespace NieuweStroom.POC.IT.Core.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetWithRoles(Expression<Func<User, bool>> predicate);
    }
}