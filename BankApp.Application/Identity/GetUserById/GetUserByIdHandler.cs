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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdCommand, GetUserByIdResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public GetUserByIdHandler(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<GetUserByIdResponse> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            var roles = await _userManager.GetRolesAsync(user);

            return new GetUserByIdResponse { Email = user.Email, Phone = user.PhoneNumber, Username = user.UserName, Role = roles.First(), Id = user.Id };
        }
    }
}
