    using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;

namespace TegnoStar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProveedoresController : Controller
    {
        private readonly IContenedorTrabajo _ContenedorTrabajo;
        private readonly ApplicationDbContext _context;

        public ProveedoresController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext context)
        {
            _ContenedorTrabajo = contenedorTrabajo;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _ContenedorTrabajo.Proveedores.Add(proveedor);
                _ContenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Proveedor proveedor = new Proveedor();
            proveedor = _ContenedorTrabajo.Proveedores.Get(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _ContenedorTrabajo.Proveedores.Update(proveedor);
                _ContenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public IActionResult Detalle(int id)
        {
            Proveedor proveedor = new Proveedor();
            proveedor = _ContenedorTrabajo.Proveedores.Get(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Json( proveedor);
        }




        //consultas
        #region llamada de Api
        [HttpGet]
        public IActionResult GetAll()
        {
            //1
            return Json(new { data = _ContenedorTrabajo.Proveedores.GetAll() });
        }


        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var objeto = _ContenedorTrabajo.Proveedores.Get(id);
            if(objeto == null)
            {
                return Json(new { success = false, message = "Error Borrando Proveedor" });
            }

            _ContenedorTrabajo.Proveedores.Remove(objeto);
            _ContenedorTrabajo.Save();
            return Json(new { success = true, message = "Proveedor Borrado Exitosamente" });
        }

        #endregion 
    }
}
