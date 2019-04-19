using System.Threading.Tasks;
using NieuweStroom.POC.IT.Core.Abstract;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanVidlyDbContext context;

        public UnitOfWork(CleanVidlyDbContext context)
        {
            this.context = context;
        }
        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}