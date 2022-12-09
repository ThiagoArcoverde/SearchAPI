using APISearch.Model;
using APISearch.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IResultRepository, ResultRepository>();
builder.Services.AddControllers();   
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
