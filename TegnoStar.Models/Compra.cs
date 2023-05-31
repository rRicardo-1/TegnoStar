using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TegnoStar.Models
{   //mmetodo para colorcar fechas del pasado hasta la actual
        public class MyDateEndAttribute : ValidationAttribute
        {
                    public override bool IsValid(object value)

                    {   DateTime d =    Convert.ToDateTime(value);
                        return d < DateTime.Now;
                    }
        }



    public class Compra
    {
        [Key]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [Display(Name = "Descripcion")]
        [RegularExpression(@"^[A-Z0-9 a-z ><\/]*$", ErrorMessage="No se aceptan caracteres especiales")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"^\(?([0-9]{1,5})\)?$", ErrorMessage = "No se aceptan numeros menores a 10000")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"^(\d{1,4(?:[.]\d{2})*(?:[.]\d+)?)", ErrorMessage = "Formato: 00.00 hasta 9999.99")]
        public double Precio_Compra { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        [DataType(DataType.DateTime)]
        [MyDateEnd(ErrorMessage = "La fecha no es aceptada")]

        public DataType? Fecha_Compra { get; set; }

        //Relacion con tabla de proveedor
        [Required]
        public int ProveedorId { get; set; }
        [ForeignKey("ProveedorId")]
        public Proveedor Proveedor { get; set; }

        //Relacion con tabla productos
        [Required]
        public int ProductosId { get; set; }
        [ForeignKey("ProductosId")]
        public Productos Productos { get; set; }

    }
}
