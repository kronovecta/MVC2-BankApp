using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels.Identity
{
    public class UserList
    {
        public List<IdentityUser> Users { get; set; }
    }
}
