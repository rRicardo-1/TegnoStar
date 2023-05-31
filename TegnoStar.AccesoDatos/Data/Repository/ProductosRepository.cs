using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TegnoStar.AccesoDatos.Data.Repository
{
    internal class ProductosRepository : Repository<Productos>, IProductosRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductosRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaProductos()
        {
            return _db.Productos.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.IdProducto.ToString(),
            });
            

        }

        public void Update(Productos productos)
        {
            var data = _db.Productos.FirstOrDefault(s => s.IdProducto == productos.IdProducto);
            data.Nombre = productos.Nombre;
            data.Descripcion = productos.Descripcion;
            data.Precio = productos.Precio;
            data.Stock = productos.Stock;
            data.UrlImagen = productos.UrlImagen;
            data.CategoriaId = productos.CategoriaId;

            //_db.SaveChanges();
        }

        public void UpdatePrecio(Productos productos)
        {
            var objeto = _db.Productos.FirstOrDefault(p => p.IdProducto == productos.IdProducto);
            objeto.Precio = productos.Precio;
        }

        public void UpdateStock(Productos productos)
        {
            var objeto = _db.Productos.FirstOrDefault(p => p.IdProducto == productos.IdProducto);
            objeto.Stock = productos.Stock;
        }

        //Mostrar categoria en el modal
        public Productos Get(int id)
        {
            return _db.Productos.Include(s => s.Categoria).FirstOrDefault(p => p.IdProducto == id);
        }
    }
        
}
