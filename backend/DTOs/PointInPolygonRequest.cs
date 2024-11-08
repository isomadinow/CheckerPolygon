using backend.Models;

namespace backend.DTOs
{
    /// <summary>
    /// Запрос для проверки нахождения точки внутри заданного полигона.
    /// </summary>
    public class PointInPolygonRequest
    {
        /// <summary>
        /// Точка, которую нужно проверить на вхождение в полигон.
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// Полигон, в котором выполняется проверка нахождения точки.
        /// </summary>
        public Polygon Polygon { get; set; }
    }
}
