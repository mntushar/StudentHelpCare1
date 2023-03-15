using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentHelpCare.StudentHelpCare.AppSetting;
using StudentHelpCare.StudentHelpCare.Repository;

var builder = WebApplication.CreateBuilder(args);

//configer dbContext
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();

//third party library
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//dependency injection
builder.Services.RegisterRepository();
builder.Services.RegisterServices();

//middelware
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//register maps
app.RegisterMap();

app.Run();
