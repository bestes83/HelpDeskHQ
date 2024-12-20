using HelpDeskHQ.Core.Contracts;
using HelpDeskHQ.Persistence.DaperImp;
using Microsoft.Extensions.DependencyInjection;

namespace HelpDeskHQ.Persistence
{
    public static class Global
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
