using DmsCreditScoring.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// register scoring service
builder.Services.AddScoped<IScoringService, ScoringService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Application}/{action=Index}/{id?}");

app.Run();
