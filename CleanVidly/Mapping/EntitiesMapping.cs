using System.Linq;
using AutoMapper;
using NieuweStroom.POC.IT.Controllers.Resources;
using NieuweStroom.POC.IT.Controllers.Users;
using NieuweStroom.POC.IT.Core.Entities;

namespace NieuweStroom.POC.IT.Mapping
{
    public class EntitiesMapping : Profile
    {
        public EntitiesMapping()
        {
            CreateMap<Category, KeyValuePairResource>();
            CreateMap<Role, KeyValuePairResource>();
            CreateMap<User, UserResource>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(ur => ur.UserRoles.Select(r => r.Role.Description)));
        }

    }
}