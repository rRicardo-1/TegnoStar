
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TegnoStar.AccesoDatos.Data.Repository.IRepository
{
    public interface IVentasRepository : IRepository<Venta>
    {
        IEnumerable<SelectListItem> GetListVentas();

        //para que el cliente pueda actualizar su venta
        //void UpdateCliente(Venta venta);

        //para que el admin pueda actualizar la venta
        void UpdateAdmin(Venta venta);

    }
}
