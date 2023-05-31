using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TegnoStar.AccesoDatos.Data.Repository
{
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        //posible error
        {
            
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.IdSlider == slider.IdSlider);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.ImageUrl = slider.ImageUrl;

            _db.SaveChanges();
        }



    }
}
