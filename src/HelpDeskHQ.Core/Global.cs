global using HelpDeskHQ.Domain.Security;
global using HelpDeskHQ.Core.Validators;

using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskHQ.Core
{
    public static class Global
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
