﻿using HelpDeskHQ.Core.Features.Security.Commands.CreateAccount;
using HelpDeskHQ.Core.Features.Security.Commands.Login;
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
            app.MapPost("/account", CreateAccount);

            return app;
        }

        private static async Task<Response<AccountVm>> Login([AsParameters] IMediator mediator, [FromBody] LoginCommand loginCommand)
        {
            return await mediator.Send(loginCommand);
        }

        private static async Task<Response<object>> CreateAccount([AsParameters] IMediator mediator, [FromBody] CreateAccountCommand createAccountCommand)
        {
            return await mediator.Send(createAccountCommand);
        }
    }
}
