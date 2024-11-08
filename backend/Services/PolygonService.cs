using backend.Models;

namespace backend.Services
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
            if (polygonVertices == null || polygonVertices.Count < 3)
            {
                Console.WriteLine("Полигон не имеет достаточно вершин (нужно хотя бы 3).");
                return false;  // Минимум 3 вершины для полигона.
            }

            Console.WriteLine($"Точка для проверки: ({point.X}, {point.Y})");
            Console.WriteLine("Вершины полигона:");
            foreach (var vertex in polygonVertices)
            {
                Console.WriteLine($"({vertex.X}, {vertex.Y})");
            }

            int n = polygonVertices.Count;
            bool isInside = false;
            for (int i = 0, j = n - 1; i < n; j = i++) // i — это индекс текущей вершины, j — предыдущей.
            {
                // Проверяем, пересекает ли горизонтальный луч рёбро полигона
                if ((polygonVertices[i].Y > point.Y) != (polygonVertices[j].Y > point.Y) &&
                    (point.X < (polygonVertices[j].X - polygonVertices[i].X) * (point.Y - polygonVertices[i].Y) /
                     (polygonVertices[j].Y - polygonVertices[i].Y) + polygonVertices[i].X))
                {
                    isInside = !isInside; // Инвертируем флаг, если пересечение
                }
            }

            return isInside; // Если пересечений нечётное — точка внутри, если чётное — снаружи.
        }


    }
}
