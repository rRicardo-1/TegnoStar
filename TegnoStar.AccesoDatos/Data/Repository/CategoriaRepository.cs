using TegnoStar.AccesoDatos.Data.Repository.IRepository;
using TegnoStar.Data;
using TegnoStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TegnoStar.AccesoDatos.Data.Repository
{
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categorias.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.IdCategoria.ToString()
            }
            
            
            );
        }

        public void Update(Categoria categoria)
        //posible error
        {
            
            var text = _db.Categorias.FirstOrDefault(s => s.IdCategoria == categoria.IdCategoria);
            text.Nombre = categoria.Nombre;
            text.Orden = categoria.Orden;

            _db.SaveChanges();
        }

        public new void Remove(int id) {
            var delete = _db.Categorias.Find(id);

            _db.Remove(delete);

            _db.SaveChanges();
        }
    }
}
