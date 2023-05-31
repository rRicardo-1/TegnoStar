using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TegnoStar.Models.ViewModels
{
    public class CompraVM
    {
        public Compra Compra { get; set; }    

        public   IEnumerable<SelectListItem> ListaProveedores { get; set; }
        public IEnumerable<SelectListItem> ListaProductos { get; set; }

    }
}
