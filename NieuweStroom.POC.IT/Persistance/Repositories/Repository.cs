using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NieuweStroom.POC.IT.Core.Abstract;
using Microsoft.EntityFrameworkCore;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        public Repository(DbContext nieuweStroomPocDbContext) => this.dbSet = nieuweStroomPocDbContext.Set<T>();//testing triggegfdg

        public Task<List<T>> GetFindAllAsync()
        {
            return dbSet.ToListAsync();
        }

        public Task AddAsync(T entity) => dbSet.AddAsync(entity);
        public Task AddRangeAsync(IEnumerable<T> entities) => dbSet.AddRangeAsync(entities);
        public Task<bool> ExistAsync(Expression<Func<T, bool>> predicate) => dbSet.AnyAsync(predicate);

        public Task<List<T>> FindByAsync(Expression<Func<T, bool>> predicate) => dbSet.Where(predicate).ToListAsync();
        public Task<T> FindUniqueAsync(Expression<Func<T, bool>> predicate) => dbSet.FirstOrDefaultAsync(predicate);
        public void Remove(T entity) => dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => dbSet.RemoveRange(entities);
    }
}