
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TegnoStar.AccesoDatos.Data.Repository.IRepository
{
    public interface IProveedorRepository : IRepository<Proveedor>
    {
        IEnumerable<SelectListItem> GetListaProveedor();

        void Update(Proveedor proveedor);
    }
}
