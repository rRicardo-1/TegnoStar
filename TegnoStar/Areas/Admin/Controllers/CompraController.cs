using Microsoft.AspNetCore.Mvc;
using TegnoStar.AccesoDatos.Data.Repository;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using TegnoStar.Models.ViewModels;

namespace TegnoStar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompraController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CompraController(IContenedorTrabajo contenedorTrabajo  )
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
            CompraVM compvm = new CompraVM()
            {
                Compra = new Models.Compra(),
                ListaProveedores = _contenedorTrabajo.Proveedores.GetListaProveedor(),
                ListaProductos = _contenedorTrabajo.Productos.GetListaProductos()
            };
            ViewData["Titulo"] = "Crear Compras";
            return View(compvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompraVM compvm)
        {
            if (ModelState.IsValid)
            {
                if (compvm.Compra.Cantidad > 0)
                {
                    var productoFromDb = _contenedorTrabajo.Productos.Get(compvm.Compra.ProductosId);
                    productoFromDb.Stock = productoFromDb.Stock + compvm.Compra.Cantidad;
                    _contenedorTrabajo.Productos.UpdateStock(productoFromDb);
                }
                _contenedorTrabajo.Compra.Add(compvm.Compra);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            compvm.ListaProductos = _contenedorTrabajo.Productos.GetListaProductos();
            compvm.ListaProveedores = _contenedorTrabajo.Proveedores.GetListaProveedor();
            return View(compvm);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            CompraVM compvm = new CompraVM()
            {
                Compra = new Models.Compra(),
                ListaProveedores = _contenedorTrabajo.Proveedores.GetListaProveedor(),
                ListaProductos = _contenedorTrabajo.Productos.GetListaProductos()
            };
            if (id != null)
            {
                compvm.Compra = _contenedorTrabajo.Compra.Get(id.GetValueOrDefault());
            }
            ViewData["Titulo"] = "Editar Compras";
            return View(compvm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompraVM compvm)
        {
            if (ModelState.IsValid)
            {
                if (compvm.Compra.Cantidad >= 0)
                {
                    var productoFromDb = _contenedorTrabajo.Productos.Get(compvm.Compra.ProductosId);
                    var compraFromDb = _contenedorTrabajo.Compra.Get(compvm.Compra.IdCompra);

                    productoFromDb.Stock = productoFromDb.Stock - compraFromDb.Cantidad;

                    productoFromDb.Stock = productoFromDb.Stock + compvm.Compra.Cantidad;

                    _contenedorTrabajo.Productos.UpdateStock(productoFromDb);
                }
                _contenedorTrabajo.Compra.Update(compvm.Compra);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            compvm.ListaProductos = _contenedorTrabajo.Productos.GetListaProductos();
            compvm.ListaProveedores = _contenedorTrabajo.Productos.GetListaProductos();
            return View(compvm);
        }

        #region LLAMADO A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Compra.GetAll(includeProperties: "Proveedor,Producto") });
        }

        public IActionResult GetAllProductos()
        {
            return Json(_contenedorTrabajo.Productos.GetAll());
        }

        [HttpGet]
        public IActionResult Detalles(int id)
        {
            CompraVM compvm = new CompraVM()
            {
                Compra = new Models.Compra(),
                ListaProveedores = _contenedorTrabajo.Proveedores.GetListaProveedor(),
                ListaProductos = _contenedorTrabajo.Productos.GetListaProductos()
            };
            compvm.Compra = _contenedorTrabajo.Compra.Get(id);
            return Json(compvm);
        }



        #endregion
    }
}

