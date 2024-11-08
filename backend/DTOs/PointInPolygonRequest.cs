using backend.Models;

namespace backend.DTOs
{
    /// <summary>
    /// ������ ��� �������� ���������� ����� ������ ��������� ��������.
    /// </summary>
    public class PointInPolygonRequest
    {
        /// <summary>
        /// �����, ������� ����� ��������� �� ��������� � �������.
        /// </summary>
        public Point Point { get; set; }

        /// <summary>
        /// �������, � ������� ����������� �������� ���������� �����.
        /// </summary>
        public Polygon Polygon { get; set; }
    }
}
