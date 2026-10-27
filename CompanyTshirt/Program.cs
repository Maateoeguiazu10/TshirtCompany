using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Services;
using CompanyTshirt.Domain.Interfaces;
using CompanyTshirt.Infrastructure.Persistence;
using CompanyTshirt.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TshirtCompanyDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TshirtCompanyDb")
    ));

builder.Services.AddScoped<ITshirtService, TshirtService>();
builder.Services.AddScoped<ITshirtRepository, TshirtRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
