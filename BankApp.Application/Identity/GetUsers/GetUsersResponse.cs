using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BankApp.Application.Identity
{
    public class GetUsersResponse
    {
        public List<IdentityUser> Users { get; set; }
    }
}