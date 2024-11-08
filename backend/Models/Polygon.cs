using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// Представляет полигон, заданный уникальным идентификатором и списком вершин.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// Уникальный идентификатор полигона.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Список точек, представляющих вершины полигона в порядке обхода.
        /// </summary>
        public List<Point>? Vertices { get; set; } = new List<Point>();
    }
}
