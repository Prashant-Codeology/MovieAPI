using DAL.DALDependencyInjections;
using DAL.DBContext;
using Microsoft.AspNetCore.Identity;
using Services.Dependency;

var builder = WebApplication.CreateBuilder(args);


//Dependencies
var services = builder.Services;
var configuration = builder.Configuration;
services.DALServices(configuration);
services.AddServices(configuration);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<MovieDbContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
