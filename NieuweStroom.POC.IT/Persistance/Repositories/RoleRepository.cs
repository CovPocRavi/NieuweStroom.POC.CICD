using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace NieuweStroom.POC.IT.Persistance.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        public RoleRepository(NieuweStroomPocDbContext nieuweStroomPocDbContext) : base(nieuweStroomPocDbContext)
        {
            this.nieuweStroomPocDbContext = nieuweStroomPocDbContext;
        }
    }
}