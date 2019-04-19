using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NieuweStroom.POC.IT.Controllers.Resources;

namespace NieuweStroom.POC.IT.Controllers.Users
{
    public class UserResource
    {
        public UserResource()
        {
            Roles = new Collection<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<string> Roles { get; private set; }
    }
}