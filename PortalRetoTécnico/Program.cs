using Microsoft.AspNetCore.Mvc;
using PortalRetoTécnico;

var builder = WebApplication.CreateBuilder(args);
// Configurar el uso de sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Registrar el filtro
builder.Services.AddScoped<SessionValidationFilter>();

// Agregar Razor Pages y aplicar filtro globalmente
builder.Services.AddRazorPages(options =>
{
    // Aplicar el filtro de sesión a todas las páginas por defecto
    options.Conventions.AddFolderApplicationModelConvention("/", model =>
    {
        model.Filters.Add(new ServiceFilterAttribute(typeof(SessionValidationFilter)));
    });
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
