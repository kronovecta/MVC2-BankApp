using BankApp.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BankApp.Application.Commands
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IMediator _mediator;
        private readonly IdentityContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateUserHandler(IMediator mediator, IdentityContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mediator = mediator;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _mediator.Send(new UpdateUserCommand { UserId = request.UserId });
            var user = await _userManager.FindByIdAsync(request.UserId);
            var currentRole = await _userManager.GetRolesAsync(user);

            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PhoneNumber = request.Phone;

            await _userManager.UpdateAsync(user);

            await _userManager.RemoveFromRolesAsync(user, currentRole);
            await _userManager.AddToRoleAsync(user, request.Role);

            return Unit.Value;
        }
    }
}
