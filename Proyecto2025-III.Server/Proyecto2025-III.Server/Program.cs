using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto2025_III.BD.Datos;
using Proyecto2025_III.Repositorio.Repositorios;
using Proyecto2025_III.Repositorio.Seguridad;
using Proyecto2025_III.Server.Components;
using Proyecto2025_III.Server.Components.Account;
using Proyecto2025_III.Servicio.ServiciosHttp;
using Proyecto2025_III.Shared.Constantes;

var builder = WebApplication.CreateBuilder(args);

var duracionCache = ConstantesGlobales.DuracionCacheEnSegundos;
builder.Services.AddOutputCache(op =>
{
    op.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(ConstantesGlobales.DuracionCacheEnSegundos);
});

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:7073") });
builder.Services.AddScoped<IHttpServicio, HttpServicio>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

//base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<ITipoProvinciaRepositorio, TipoProvinciaRepositorio>();
builder.Services.AddScoped<IPaisRepositorio, PaisRepositorio>();
builder.Services.AddScoped<IProvinciaRepositorio, ProvinciaRepositorio>();
builder.Services.AddScoped<IServicioSeguridad, ServicioSeguridad>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Proyecto2025_III.Server.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.UseOutputCache();
app.MapControllers();
app.Run();
