using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.AccesoDatos.Data.Repository;
using TegnoStar.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConexionSQL") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Agregar Contenedor de trabajo
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();