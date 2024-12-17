using Trio.ContactSync.Api;
using Trio.ContactSync.Api.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Bootstrap bootstrap = new();
bootstrap.ConfigureServices(builder.Services);

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trio Contact Sync");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();