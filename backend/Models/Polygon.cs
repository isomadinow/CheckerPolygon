namespace PolygonApp.Models
{
    /// <summary>
    /// ������������ �������, �������� ���������� ��������������� � ������� ������.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// ���������� ������������� ��������.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ������ �����, �������������� ������� �������� � ������� ������.
        /// </summary>
        public List<Point> Vertices { get; set; }
    }
}
