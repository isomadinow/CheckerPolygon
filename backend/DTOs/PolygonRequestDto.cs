using backend.Models;

namespace backend.DTOs
{
    /// <summary>
    /// Запрос для создания или сохранения нового полигона с заданными вершинами.
    /// </summary>
    public class PolygonRequestDto
    {
        /// <summary>
        /// Список точек, представляющих вершины полигона.
        /// </summary>
        public List<PointDto> Vertices { get; set; } = new List<PointDto>();

    }
}
