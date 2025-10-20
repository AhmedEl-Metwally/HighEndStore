namespace HighEndStore.API.Extensions
{
    public static class UseSwaggerExtensions
    {
        public static WebApplication AddUseSwagger(this WebApplication app)
        {
            // app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
