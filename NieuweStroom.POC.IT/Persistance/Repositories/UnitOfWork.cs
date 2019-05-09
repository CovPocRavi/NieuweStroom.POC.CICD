using System.Threading.Tasks;
using NieuweStroom.POC.IT.Core.Abstract;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        public UnitOfWork(NieuweStroomPocDbContext nieuweStroomPocDbContext)
        {
            this.nieuweStroomPocDbContext = nieuweStroomPocDbContext;
        }
        public Task<int> SaveAsync()
        {
            return nieuweStroomPocDbContext.SaveChangesAsync();
        }
    }
}