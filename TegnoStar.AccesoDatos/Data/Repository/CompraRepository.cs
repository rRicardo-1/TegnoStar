using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TegnoStar.AccesoDatos.Data.Repository
{
    internal class CompraRepository : Repository<Compra>, ICompraRepository
    {
        private readonly ApplicationDbContext _db;

        public CompraRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListCompra()
        {
            return _db.Compra.Select(p => new SelectListItem()
            {
                Text = p.Descripcion,
                Value = p.IdCompra.ToString()
            });
        }

        public void Update(Compra compra)
        //posible error
        {
            var text = _db.Compra.FirstOrDefault(s => s.IdCompra == compra.IdCompra);
            text.Descripcion = compra.Descripcion;
            text.Cantidad = compra.Cantidad;
            text.Precio_Compra = compra.Precio_Compra;
            text.Fecha_Compra = compra.Fecha_Compra;
            text.ProveedorId = compra.ProveedorId;
            text.ProductosId = compra.ProductosId;

            _db.SaveChanges();
        }

        public new void Remove(int id)
        {
            var delete = _db.Proveedores.Find(id);

            _db.Remove(delete);

            _db.SaveChanges();
        }
    }
}

