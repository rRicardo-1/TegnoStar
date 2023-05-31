using Microsoft.AspNetCore.Mvc;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;

namespace TegnoStar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _ContenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment) 
        { 
            _ContenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        
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
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;


                //Nuevo Slider
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStream =new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStream);
                }
                
                slider.ImageUrl = @"\imagenes\sliders\" +nombreArchivo + extension;
                
                
                _ContenedorTrabajo.Slider.Add(slider);
                _ContenedorTrabajo.Save();
                
                return RedirectToAction(nameof(Index));

            }
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                var slider = _ContenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }

            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {

                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeDB = _ContenedorTrabajo.Slider.Get(slider.IdSlider);

                if(archivos.Count() > 0)
                {
                    //Nueva Imgen par el Slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDB.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }


                    //Nuevamente Subimos el archivo

                    using (var fileStream = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }

                    slider.ImageUrl = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _ContenedorTrabajo.Slider.Update(slider);
                    _ContenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));


                }
                else
                {
                    //Aqui seria cuando la nueva imagen ya existe y se conserva
                    slider.ImageUrl = sliderDesdeDB.ImageUrl;
                }

                _ContenedorTrabajo.Slider.Update(slider);
                _ContenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        //consultas
        #region llamada de Api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _ContenedorTrabajo.Slider.GetAll() });
        }


        [HttpDelete]
        public IActionResult Delete(int id) {

            var sliderDesdeDb = _ContenedorTrabajo.Slider.Get(id);
            
            if (sliderDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }

            _ContenedorTrabajo.Slider.Remove(sliderDesdeDb);
            _ContenedorTrabajo.Save();
            return Json(new { success=true, message = "Slider Borrado" });
        }


        #endregion
    }
}
