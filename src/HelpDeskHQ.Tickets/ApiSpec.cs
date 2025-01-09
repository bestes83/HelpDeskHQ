namespace HelpDeskHQ.Tickets
{
    public static class ApiSpec
    {
        public static IEndpointRouteBuilder MapV1Endpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/tickets", () => new {Test = "Tickets"});

            return app;
        }
    }
}
