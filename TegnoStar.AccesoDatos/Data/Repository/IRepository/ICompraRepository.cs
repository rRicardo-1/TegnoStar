using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TegnoStar.AccesoDatos.Data.Repository.IRepository
{
    public interface ICompraRepository : IRepository<Compra>
    {
        //para obtener listas de compra en otras entidades 
        IEnumerable<SelectListItem> GetListCompra();

        void Update(Compra compra);
    }
}
