using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TegnoStar.AccesoDatos.Data.Repository.IRepository
{
    public interface ISliderRepository : IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
