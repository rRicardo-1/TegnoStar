using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TegnoStar.Models
{
    public class Slider
    {

        public Slider() { }

        [Key]
        public int IdSlider { get; set; }

        [Required(ErrorMessage = "Ingrese nombre para el slider")]
        [Display(Name = "Nombre Slider")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Imagen")]
        public string ImageUrl { get; set; }
    }
}
