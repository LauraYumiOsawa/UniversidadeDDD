using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Universidade.Infra;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddControllers()
    .AddApplicationPart(Assembly.Load("APIUni"));

var app = builder.Build();

app.UseRouting();
app.MapControllers(); 

app.Run();