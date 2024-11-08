using System.Collections.Generic;
using System.Linq;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class PolygonDatabaseService
    {
        private readonly PolygonContext _context;

        public PolygonDatabaseService(PolygonContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Сохраняет полигон с уникальным идентификатором в базе данных PostgreSQL.
        /// </summary>
        /// <param name="polygon">Экземпляр полигона для сохранения.</param>
        /// <returns>Уникальный идентификатор сохраненного полигона.</returns>
        public int SavePolygon(Polygon polygon)
        {
            _context.Polygons.Add(polygon);
            _context.SaveChanges();
            return polygon.Id;
        }

        /// <summary>
        /// Загружает все полигоны из базы данных.
        /// </summary>
        /// <returns>Список всех полигонов из базы данных.</returns>
        public List<Polygon> LoadAllPolygons()
        {
            return _context.Polygons.Include(p => p.Vertices).ToList();
        }

        /// <summary>
        /// Загружает полигон по его уникальному идентификатору из базы данных.
        /// </summary>
        /// <param name="id">Уникальный идентификатор полигона.</param>
        /// <returns>Экземпляр полигона, если найден, иначе null.</returns>
        public Polygon LoadPolygonById(int id)
        {
            return _context.Polygons
                           .Include(p => p.Vertices)
                           .FirstOrDefault(p => p.Id == id);
        }
    }
}
