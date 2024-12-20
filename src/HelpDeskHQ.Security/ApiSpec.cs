using HelpDeskHQ.Core.Features.Security.Commands;
using HelpDeskHQ.Core.Helpers;
using HelpDeskHQ.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskHQ.Security
{
    public static class ApiSpec
    {
        public static IEndpointRouteBuilder MapV1Endpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/login", Login);
            return app;
        }

        private static async Task<Response<AccountVm>> Login([AsParameters] IMediator mediator, [FromBody] LoginCommand loginCommand)
        {
            return await mediator.Send(loginCommand);
        }
    }
}
