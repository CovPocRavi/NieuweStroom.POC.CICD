using Microsoft.EntityFrameworkCore;
using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly CleanVidlyDbContext context;

        public CategoryRepository(CleanVidlyDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}