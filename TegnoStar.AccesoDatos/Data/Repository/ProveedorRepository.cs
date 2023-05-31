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
    internal class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
    {
        private readonly ApplicationDbContext _db;

        public ProveedorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem>GetListaProveedor()
        {
            return _db.Proveedores.Select(p => new SelectListItem()
            {
                Text = p.NombreEmpresa,
                Value = p.IdProveedor.ToString()
            });
        }

        public void Update(Proveedor proveedor)
        //posible error
        {
            
            var text = _db.Proveedores.FirstOrDefault(s => s.IdProveedor == proveedor.IdProveedor);
            text.NombreEmpresa = proveedor.NombreEmpresa;
            text.NombreContacto = proveedor.NombreContacto;
            text.Telefono = proveedor.Telefono;
            text.CorreoElectronico = proveedor.CorreoElectronico;
            text.Direccion = proveedor.Direccion;
            text.acivo = proveedor.acivo;
            //descomentar cuando haya incluido el modeo de articulo
            //text.IdProducto = proveedor.IdProducto;

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

