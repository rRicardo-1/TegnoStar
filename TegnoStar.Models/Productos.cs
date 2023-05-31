using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegnoStar.Models
{
    public class Productos
    {

        [Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Nombre De Campo Requerido")]
        [Display(Name = "Nombre Del Producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion Requerida")]
        [Display(Name = "Descripcion Del Producto")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Precio Del Producto")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Precio Requerido")]
        [Display(Name = "Existencias")]
        public int Stock { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

    }
}
