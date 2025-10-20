using HighEndStore.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddWebApiServices();
builder.Services.AddCoreServices();
builder.Services.AddServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
await app.AddSeedDataAsync();

// Configure the HTTP request pipeline.

app.AddExceptionHandLingMiddleware();


if (app.Environment.IsDevelopment())
{
    app.AddUseSwagger();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
