using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NieuweStroom.POC.IT.Controllers.Users
{
    public class SaveUserResource
    {
        public SaveUserResource()
        {
            Roles = new Collection<int>();
        }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public ICollection<int> Roles { get; private set; }
    }
}