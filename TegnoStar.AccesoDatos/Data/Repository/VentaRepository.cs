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
    public class VentasRepository : Repository<Venta>, IVentasRepository
    {
        private readonly ApplicationDbContext _db;

        public VentasRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListVentas()
        {
            return _db.Venta.Select(i => new SelectListItem()
            {
                Text = i.Descripcion,
                Value = i.Id.ToString()

            });
        }

        public void UpdateAdmin(Venta venta)
        {
            var objDesdeDB = _db.Venta.FirstOrDefault(a => a.Id == venta.Id);
            objDesdeDB.Descripcion = venta.Descripcion;

        }


    }
}
