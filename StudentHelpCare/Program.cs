using StudentHelpCare.AppSetting;

var builder = WebApplication.CreateBuilder(args);

//dependency injection
builder.Services.RegisterRepository();
builder.Services.RegisterServices();

var app = builder.Build();

//register maps
app.RegisterMap();

app.Run();
