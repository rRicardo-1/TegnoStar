using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TegnoStar.AccesoDatos.Data.Repository.IRepository
{
    public interface IProductosRepository : IRepository<Productos>
    {

        IEnumerable<SelectListItem> GetListaProductos();
        void Update(Productos productos);
        void UpdateStock(Productos productos);
        void UpdatePrecio(Productos productos);
    }
}
