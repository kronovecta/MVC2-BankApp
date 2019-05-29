using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Identity
{
    class GetUsersHandler : IRequestHandler<GetUsersCommand, GetUsersResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public GetUsersHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUsersResponse> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            return new GetUsersResponse { Users = await _userManager.Users.ToListAsync() };
        }
    }
}
