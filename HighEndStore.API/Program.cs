using Domain.Contracts.SeedData;
using Domain.Contracts.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Context;
using Persistance.Data.SeedData;
using Persistance.Repositories.UnitOfWorks;
using Services.Abstraction.Interface;
using Services.Implementation;
using Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HighEndStoreDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddScoped<IDataSeeding,DataSeeding>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<IServiceManager,ServiceManager>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddAutoMapper(Mapping => Mapping.AddProfile(new ProductProfile()));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var objOfSeedData = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
await objOfSeedData.SeedDataAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
