using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        public CategoryRepository(NieuweStroomPocDbContext nieuweStroomPocDbContext) : base(nieuweStroomPocDbContext)
        {
            this.nieuweStroomPocDbContext = nieuweStroomPocDbContext;
        }
    }
}