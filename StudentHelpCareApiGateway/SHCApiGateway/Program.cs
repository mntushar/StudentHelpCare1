using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SHCApiGateway.Data.Entity;
using SHCApiGateway.Repository.DbContext;
using StudentHelpCare.StudentHelpCare.AppSetting;
using StudentHelpCare.StudentHelpCareIdentityServer.AppSetting;

var builder = WebApplication.CreateBuilder(args);

//configer dbContext
builder.Services.AddDbContext<SHCApiGatewayDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
//configer Identity
builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SHCApiGatewayDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
    options.Lockout.MaxFailedAccessAttempts = 100;
    options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});
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
