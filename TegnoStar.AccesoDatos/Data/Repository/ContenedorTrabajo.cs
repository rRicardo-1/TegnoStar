using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegnoStar.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
       private readonly ApplicationDbContext _db;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categorias = new CategoriaRepository(_db);
            Productos = new ProductosRepository(_db);
            Slider = new SliderRepository(_db);
            Proveedores = new ProveedorRepository(_db);
            Compra = new CompraRepository(_db);
            Venta = new VentasRepository(_db);

        }

        public ICategoriaRepository Categorias { get; private set; }
        public IProductosRepository Productos { get; private set; }
        public ISliderRepository Slider { get; private set; }
        public IProveedorRepository Proveedores { get; private set; }
        public ICompraRepository Compra { get; private set; }
        public IVentasRepository Venta { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
