using StudentHelpCare.AppSetting;

var builder = WebApplication.CreateBuilder(args);

//dependency injection

var app = builder.Build();

//register maps
app.RegisterMap();

app.Run();
