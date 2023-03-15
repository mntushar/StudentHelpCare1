using Microsoft.EntityFrameworkCore;
using StudentHelpCare.Identity.Repository;
using StudentHelpCare.StudentHelpCareIdentityServer.AppSetting;

var builder = WebApplication.CreateBuilder(args);

//configer dbContext
builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//middelware
var app = builder.Build();

//register maps
app.RegisterMap();

app.Run();
