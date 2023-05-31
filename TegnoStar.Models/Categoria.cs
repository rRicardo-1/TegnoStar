using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TegnoStar.Models
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "Ingresar El Nombre De Categoria")]
        [Display(Name = "Nombre Categoria")]
        public string Nombre { get; set; }

        [Display(Name = "Orden De Visualizacion")]
        public int? Orden { get; set; }
    }
}
