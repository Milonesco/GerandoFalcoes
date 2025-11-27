using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json.Serialization;
using Transformese.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// MVC
// =======================================
builder.Services.AddControllersWithViews();

// =======================================
// SESSION
// =======================================
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// =======================================
// AUTHENTICATION + COOKIES
// =======================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();

// =======================================
// API CLIENTS
// =======================================

// Usuários — usa BaseUrl da chave "Api"
builder.Services.AddHttpClient<IUsuarioApiClient, UsuarioApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
});

// Candidatos — você estava misturando Api e ApiSettings
// unifiquei para usar SEMPRE "Api:BaseUrl"
builder.Services.AddHttpClient<ICandidatoService, CandidatoService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
});

// Unidades — rota /api/Unidades
builder.Services.AddHttpClient<IUnidadeApiClient, UnidadeApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]);
});

// =======================================
// BUILD
// =======================================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// =======================================
// PIPELINE
// =======================================
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// =======================================
// ROTAS
// =======================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
