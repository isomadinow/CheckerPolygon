using PolygonApp.Models;

namespace PolygonApp.Services
{
    public class PolygonService
    {
        /// <summary>
        /// Определяет, находится ли указанная точка внутри многоугольника.
        /// </summary>
        /// <param name="point">Точка, которую нужно проверить.</param>
        /// <param name="polygonVertices">Список вершин многоугольника в порядке обхода.</param>
        /// <returns>Возвращает true, если точка находится внутри многоугольника, иначе false.</returns>
        /// <remarks>
        /// Метод реализует алгоритм Ray Casting. 
        /// Алгоритм работает, проверяя, сколько раз горизонтальный луч, 
        /// исходящий из проверяемой точки, пересекает рёбра многоугольника.
        /// Если число пересечений нечётное, точка находится внутри, если чётное — снаружи.
        /// </remarks>
        public bool IsPointInPolygon(Point point, List<Point> polygonVertices)
        {
            int n = polygonVertices.Count;
            bool isInside = false;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                // Проверяем, пересекает ли горизонтальный луч ребро многоугольника
                if ((polygonVertices[i].Y > point.Y) != (polygonVertices[j].Y > point.Y) &&
                    (point.X < (polygonVertices[j].X - polygonVertices[i].X) *
                                (point.Y - polygonVertices[i].Y) /
                                (polygonVertices[j].Y - polygonVertices[i].Y) +
                                polygonVertices[i].X))
                {
                    isInside = !isInside;
                }
            }

            return isInside;
        }
    }
}
