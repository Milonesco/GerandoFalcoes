using Transformese.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Adiciona serviços essenciais
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Necessário para o InscricaoController falar com a API

// Se o Controller de Unidades buscar dados da API, registre o serviço aqui:
// builder.Services.AddScoped<IUnidadeApiClient, UnidadeApiClient>(); 

var app = builder.Build();

// 2. Configuração de Erros
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// (Removido Authentication/Authorization pois é um site público)

// 3. Rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();