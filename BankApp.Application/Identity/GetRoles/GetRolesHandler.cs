using BankApp.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Identity
{
    public class GetRolesHandler : IRequestHandler<GetRolesRequest, GetRolesResponse>
    {
        private readonly IdentityContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetRolesHandler(IdentityContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            return new GetRolesResponse { Roles = roles };
        }
    }
}
