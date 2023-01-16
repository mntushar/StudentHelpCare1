using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentHelpCare.AppSetting;
using StudentHelpCare.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//dependency injection
builder.Services.RegisterRepository();
builder.Services.RegisterServices();

var app = builder.Build();

//register maps
app.RegisterMap();

app.Run();
