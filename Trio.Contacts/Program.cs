using Trio.ContactSync.Api;
using Trio.ContactSync.Api.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Bootstrap bootstrap = new();
bootstrap.ConfigureServices(builder.Services);

WebApplication app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();