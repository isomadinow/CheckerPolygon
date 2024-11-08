using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    /// <summary>
    /// ������������ �������, �������� ���������� ��������������� � ������� ������.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// ���������� ������������� ��������.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ������ �����, �������������� ������� �������� � ������� ������.
        /// </summary>
        public List<Point>? Vertices { get; set; } = new List<Point>();
    }
}
