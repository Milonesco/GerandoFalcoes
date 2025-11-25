using Microsoft.AspNetCore.Authentication.Cookies;
using Transformese.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// SESSION
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// =========================
// AUTHENTICATION + COOKIES
// =========================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

// SUA INJEÇÃO DE API CLIENTS
builder.Services.AddHttpClient<IUsuarioApiClient, UsuarioApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
});
builder.Services.AddHttpClient<ICursoApiClient, CursoApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
});
builder.Services.AddHttpClient<IUnidadeApiClient, UnidadeApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
});

// BUILD
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// PIPELINE
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication(); // <--- ESSENCIAL
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
