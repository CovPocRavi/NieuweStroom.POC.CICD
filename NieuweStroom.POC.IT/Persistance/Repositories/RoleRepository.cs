using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly CleanVidlyDbContext context;

        public RoleRepository(CleanVidlyDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}