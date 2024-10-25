using PolygonApp.Models;
using System.Globalization;
using System.IO;
using System.Text;

namespace PolygonApp.Services
{
    public class PolygonFileService
    {
        private readonly string _directoryPath;
        private readonly string _filePath;
        private readonly string _idFilePath;

        public PolygonFileService()
        {
            var rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            _directoryPath = Path.Combine(rootDirectory, "csv_file");
            _filePath = Path.Combine(_directoryPath, "polygons.csv");
            _idFilePath = Path.Combine(_directoryPath, "id.txt");

            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            if (!File.Exists(_idFilePath))
            {
                File.WriteAllText(_idFilePath, "0");
            }

            if (!File.Exists(_filePath))
            {
                AddCsvHeader();
            }
        }

        /// <summary>
        /// Добавляет заголовок в CSV-файл, если файл отсутствует.
        /// </summary>
        private void AddCsvHeader()
        {
            var header = "Id,Vertices";
            File.AppendAllText(_filePath, header + Environment.NewLine);
        }

        /// <summary>
        /// Генерирует следующий уникальный ID для полигона.
        /// </summary>
        /// <returns>Уникальный идентификатор для полигона.</returns>
        private int GetNextId()
        {
            var content = File.ReadAllText(_idFilePath);
            var lastId = string.IsNullOrWhiteSpace(content) ? 0 : int.Parse(content);

            var newId = lastId + 1;

            File.WriteAllText(_idFilePath, newId.ToString());
            return newId;
        }

        /// <summary>
        /// Сохраняет полигон с уникальным идентификатором в CSV-файл.
        /// </summary>
        /// <param name="polygon">Экземпляр полигона для сохранения.</param>
        /// <returns>Уникальный идентификатор сохраненного полигона.</returns>
        public int SavePolygonToCsv(Polygon polygon)
        {
            var polygonId = GetNextId();
            var sb = new StringBuilder();

            sb.Append(polygonId);
            foreach (var point in polygon.Vertices)
            {
                sb.Append($",{point.X.ToString(CultureInfo.InvariantCulture)},{point.Y.ToString(CultureInfo.InvariantCulture)}");
            }
            sb.AppendLine();

            File.AppendAllText(_filePath, sb.ToString());
            return polygonId;
        }

        /// <summary>
        /// Загружает все полигоны из CSV-файла.
        /// </summary>
        /// <returns>Список всех полигонов из файла.</returns>
        public List<Polygon> LoadAllPolygonsFromCsv()
        {
            var polygons = new List<Polygon>();

            if (!File.Exists(_filePath))
            {
                return polygons;
            }

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length < 3 || (parts.Length - 1) % 2 != 0)
                {
                    continue;
                }

                if (!int.TryParse(parts[0], out int polygonId))
                {
                    continue;
                }

                var vertices = new List<Point>();
                for (int i = 1; i < parts.Length; i += 2)
                {
                    if (double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out double x) &&
                        double.TryParse(parts[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
                    {
                        vertices.Add(new Point { X = x, Y = y });
                    }
                }

                polygons.Add(new Polygon { Id = polygonId, Vertices = vertices });
            }

            return polygons;
        }

        /// <summary>
        /// Загружает полигон по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор полигона.</param>
        /// <returns>Экземпляр полигона, если найден, иначе null.</returns>
        public Polygon LoadPolygonById(int id)
        {
            if (!File.Exists(_filePath))
            {
                return null;
            }

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                if (int.Parse(parts[0]) == id)
                {
                    var vertices = new List<Point>();
                    for (int i = 1; i < parts.Length; i += 2)
                    {
                        if (double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out double x) &&
                            double.TryParse(parts[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out double y))
                        {
                            vertices.Add(new Point { X = x, Y = y });
                        }
                    }
                    return new Polygon { Id = id, Vertices = vertices };
                }
            }

            return null;
        }
    }
}
