using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TegnoStar.Models
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [RegularExpression(@"^[A-Z0-9 a-z ><\/]*$", ErrorMessage = "No se aceptan caracteres especiales")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [RegularExpression(@"^[A-Z0-9 a-z ><\/]*$", ErrorMessage = "No se aceptan caracteres especiales")]
        public string Cliente { get; set; }

        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [RegularExpression(@"^\(?([0-9]{1,5})\)?$", ErrorMessage = "Solo se aceptan numeros menores a 10000")]
        public int Cantidad { get; set; }



        [DataType(DataType.DateTime)]

        public DateTime? Fecha_compra { get; set; }


        //relacion con tabla productos
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Productos Productos { get; set; }

    }
}
