namespace HelpDeskHQ.Security
{
    public static class ApiSpec
    {
        public static IEndpointRouteBuilder MapV1Endpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/login", () => { });
            return app;
        }
    }
}
