using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Models;
using TegnoStar.Models.ViewModels;

namespace TegnoStar.Areas.Cliente.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                Slider = _contenedorTrabajo.Slider.GetAll(),
                ListaProductos = _contenedorTrabajo.Productos.GetAll()
            };
            return View(homeVm);
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}