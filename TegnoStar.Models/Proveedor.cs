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
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "Ingresar El Nombre De La Empresa")]
        [MaxLength(50)]
        [Display(Name = "Nombre de la Empresa")]
        public string NombreEmpresa { get; set; }

        [Required(ErrorMessage = "Ingresar El Nombre Del Contacto")]
        [Display(Name = "Nombre del Contacto ")]
        public string NombreContacto { get; set; }

        [Required(ErrorMessage = "Ingresar El Teléfono")]
        [Display(Name = "Teléfono")]
        [RegularExpression(@"([0-9]{4})[-]([0-9]{4})", ErrorMessage="El formato es AAAA-AAAA")]
        public string Telefono { get; set; }


        [Display(Name = "Correo Eletrónico ")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "El campo {0} no es un correo electrónico válido.")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Ingresar La Dirección")]
        [Display(Name = "Dirección: ")]
        public string Direccion { get; set; }

        [Display(Name = "Proveedor Activo: ")]
        public bool acivo { get; set; }

        //Esto se deshabilita cuando tengamos el modelo de productos por el
        //momento esta comentada hasta que unamos el proyecto
        // [Required]
        // public int IdProducto { get;set; }

        // //especificamos que este atributo es una llave foranea
        // [ForeignKey("IdProducto")]
        // //desde aca especficiamos que estamos llamando al modelo producto cuando se cree
        ////se agregar segunco como esta agregado en la clase de ApplicationDBCOntext
        // public Producto Producto { get; set; }

    }
}
