using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TegnoStar.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        ICategoriaRepository Categorias { get; }
        //Agregar Repositorios
        IProductosRepository Productos { get; }
        ISliderRepository Slider { get; }
        IProveedorRepository Proveedores { get; }
        ICompraRepository Compra { get; }
        IVentasRepository Venta { get; }



        void Save();
    }
}
