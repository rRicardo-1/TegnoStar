using Microsoft.AspNetCore.Mvc;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Models;
using TegnoStar.Models.ViewModels;

namespace TegnoStar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VentasController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        public VentasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            VentaVM ventavm = new VentaVM()
            {
                Venta = new Models.Venta(),
                ListaProductos = _contenedorTrabajo.Productos.GetListaProductos()

            };

            return View(ventavm);
        }

        [HttpPost]
        public IActionResult Create(VentaVM ventavm)
        {
            if (ModelState.IsValid)
            {
                if (ventavm.Venta.Cantidad > 0)
                {
                    var productoFromDb = _contenedorTrabajo.Productos.Get(ventavm.Venta.ProductoId);

                    productoFromDb.Stock = productoFromDb.Stock - ventavm.Venta.Cantidad;
                    _contenedorTrabajo.Productos.UpdateStock(productoFromDb);
                }
                ventavm.Venta.Fecha_compra = DateTime.Now;
                _contenedorTrabajo.Venta.Add(ventavm.Venta);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            ventavm.ListaProductos = _contenedorTrabajo.Productos.GetListaProductos();
           
            return View(ventavm);

        
        }





        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _contenedorTrabajo.Venta.GetAll(includeProperties: "Productos");

            return Json(new { data = _contenedorTrabajo.Venta.GetAll(includeProperties: "Productos") });
        }

        public IActionResult DetallesVenta(int id)
        {
            return Json(new { data = _contenedorTrabajo.Venta.GetAll(i => i.Id == id, includeProperties: "Productos") });
        }

        //[HttpDelete]
        //public IActionResult EliminarVenta(int id)
        //{
        //    var objFromDb = _contenedorTrabajo.Venta.Get(id);
        //    if (objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "No se logro borrar la venta" });
        //    }
        //    _contenedorTrabajo.Venta.Remove(objFromDb);
        //    _contenedorTrabajo.Save();
        //    return Json(new { success = true, message = "La venta se elimino corectamente" });
        //}


        #endregion


    }
}
