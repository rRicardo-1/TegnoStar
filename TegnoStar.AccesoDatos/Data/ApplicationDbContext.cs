using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TegnoStar.Models;

namespace TegnoStar.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //CADA MODELO SE DEBE AGREGAR AQUI 
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Productos> Productos { get; set; }

        public DbSet<Slider> Slider { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<Venta> Venta { get; set; }


    }
}