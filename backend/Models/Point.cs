using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Models
{
    /// <summary>
    /// ������������ ���������� ����� � 2D-������������.
    /// </summary>
    public class Point
    {
        [Key]
        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        [ForeignKey("Polygon")]
        public int? PolygonId { get; set; }

        // ���������� ������������ ����� ��������, ����� �������� ����������� ������
        [JsonIgnore]
        public Polygon? Polygon { get; set; }
    }
}
