using PolygonApp.Models;

namespace PolygonApp.Services
{
    public class PolygonService
    {
        /// <summary>
        /// ����������, ��������� �� ��������� ����� ������ ��������������.
        /// </summary>
        /// <param name="point">�����, ������� ����� ���������.</param>
        /// <param name="polygonVertices">������ ������ �������������� � ������� ������.</param>
        /// <returns>���������� true, ���� ����� ��������� ������ ��������������, ����� false.</returns>
        /// <remarks>
        /// ����� ��������� �������� Ray Casting. 
        /// �������� ��������, ��������, ������� ��� �������������� ���, 
        /// ��������� �� ����������� �����, ���������� ���� ��������������.
        /// ���� ����� ����������� ��������, ����� ��������� ������, ���� ������ � �������.
        /// </remarks>
        public bool IsPointInPolygon(Point point, List<Point> polygonVertices)
        {
            int n = polygonVertices.Count;
            bool isInside = false;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                // ���������, ���������� �� �������������� ��� ����� ��������������
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
