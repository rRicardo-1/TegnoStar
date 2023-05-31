using Microsoft.AspNetCore.Mvc;
using TegnoStar.AccesoDatos.Data.Repository;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using TegnoStar.Models.ViewModels;

namespace TegnoStar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
         
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductosController(IContenedorTrabajo contenedorTrabajo,ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        
        }

        [HttpGet]
        public IActionResult Create()
        {
            var productosvm = new ProductosVM()
            {
                Productos = new TegnoStar.Models.Productos(),
                ListaCategorias = _contenedorTrabajo.Categorias.GetListaCategorias()
            };
            return View(productosvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductosVM productosvm)
        {
            if (ModelState.IsValid)
            {
                string ruta = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (productosvm.Productos.IdProducto == 0)
                {
                    //Para crear un nuevo producto
                    string namearchi = Guid.NewGuid().ToString();
                    var cargarfile = Path.Combine(ruta, @"imagenes/productos");
                    var extension = Path.Combine(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(cargarfile, namearchi + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    productosvm.Productos.UrlImagen = @"\imagenes\productos\" + namearchi + extension;
                    productosvm.Productos.Stock = 0;

                    _contenedorTrabajo.Productos.Add(productosvm.Productos);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            productosvm.ListaCategorias = _contenedorTrabajo.Categorias.GetListaCategorias();
            return View(productosvm);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var productosvm = new ProductosVM()
            {
                Productos = new Models.Productos(),
                ListaCategorias = _contenedorTrabajo.Categorias.GetListaCategorias()
            };

            if (id != null)
            {
                productosvm.Productos = _contenedorTrabajo.Productos.Get(id.GetValueOrDefault());
            }

            return View(productosvm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductosVM productosvm)
        {
            if (ModelState.IsValid)
            {
                string ruta = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var productodb = _contenedorTrabajo.Productos.Get(productosvm.Productos.IdProducto);


                if (archivos.Count() > 0)
                {
                    //Nueva imagen para el artículo
                    string namefile = Guid.NewGuid().ToString();
                    var cargarimg = Path.Combine(ruta, @"imagenes\productos");
                    var extension = Path.Combine(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(ruta, productodb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Nuevamente subimos el archivo
                    using (var file = new FileStream(Path.Combine(cargarimg, namefile + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(file);
                    }

                    productosvm.Productos.UrlImagen = @"\imagenes\productos\" + namefile + nuevaExtension;


                    _contenedorTrabajo.Productos.Update(productosvm.Productos);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //Aquí sería cuando la imagen ya existe y se conserva
                    productosvm.Productos.UrlImagen = productodb.UrlImagen;
                }

                _contenedorTrabajo.Productos.Update(productosvm.Productos);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(productosvm);
        }



        //consultas
        #region llamadas Api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Productos.GetAll(includeProperties: "Categoria") });
        }

        public IActionResult Detalle(int id)
        {
            var productosvm = new ProductosVM()
            {
                Productos = new Models.Productos(),
                ListaCategorias = _contenedorTrabajo.Categorias.GetListaCategorias()
            };


            productosvm.Productos = _contenedorTrabajo.Productos.Get(id);
            return Json(productosvm);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var articuloDesdeDb = _contenedorTrabajo.Productos.Get(id);
            string rutaDirectorioPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando producto" });
            }

            _contenedorTrabajo.Productos.Remove(articuloDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Producto Eliminado " });
        }

        #endregion llamada a API

    }

   
}

