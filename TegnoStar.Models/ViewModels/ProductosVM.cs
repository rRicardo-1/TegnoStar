using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TegnoStar.Models.ViewModels
{
    public class ProductosVM
    {
        public Productos Productos { get; set; }    

        public   IEnumerable<SelectListItem> ListaCategorias { get; set; }  
    }
}
