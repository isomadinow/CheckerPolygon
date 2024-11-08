using backend.DTOs;
using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolygonController : ControllerBase
    {
        private readonly PolygonService _polygonService;
        private readonly PolygonDatabaseService _polygonDatabaseService;

        public PolygonController(PolygonService polygonService, PolygonDatabaseService polygonDatabaseService)
        {
            _polygonService = polygonService;
            _polygonDatabaseService = polygonDatabaseService;
        }

        /// <summary>
        /// Проверяет, находится ли указанная точка внутри полигона.
        /// </summary>
        /// <param name="request">Запрос, содержащий данные о точке и полигоне.</param>
        /// <returns>Возвращает true, если точка находится внутри полигона, иначе false.</returns>
        [HttpPost("check-point")]
        public IActionResult CheckPoint([FromBody] PointInPolygonRequest request)
        {
            var polygon = request.Polygon;
            var isInside = _polygonService.IsPointInPolygon(request.Point, polygon.Vertices);
            return Ok(new { inside = isInside });
        }

        /// <summary>
        /// Сохраняет новый полигон в базе данных.
        /// </summary>
        /// <param name="polygonRequest">Данные полигона для сохранения.</param>
        /// <returns>Возвращает ID нового сохраненного полигона.</returns>
        [HttpPost("save")]
        public IActionResult SavePolygon([FromBody] PolygonRequestDto polygonRequest)
        {
            // Преобразуем данные из DTO в объекты модели Polygon и Point
            var polygon = new Polygon
            {
                Vertices = polygonRequest.Vertices.Select(v => new Point
                {
                    X = v.X,
                    Y = v.Y
                }).ToList()
            };

            // Сохраняем полигон через сервис базы данных
            var polygonId = _polygonDatabaseService.SavePolygon(polygon);
            return Ok(new { message = "Полигон успешно сохранен", id = polygonId });
        }


        /// <summary>
        /// Загружает все сохраненные полигоны из базы данных.
        /// </summary>
        /// <returns>Возвращает список всех полигонов.</returns>
        [HttpGet("load-all")]
        public IActionResult LoadAllPolygons()
        {
            var polygons = _polygonDatabaseService.LoadAllPolygons();
            return Ok(polygons);
        }

        /// <summary>
        /// Загружает полигон по его идентификатору из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор полигона для загрузки.</param>
        /// <returns>Возвращает данные полигона с указанным ID, если он существует.</returns>
        [HttpGet("load/{id}")]
        public IActionResult LoadPolygonById(int id)
        {
            var polygon = _polygonDatabaseService.LoadPolygonById(id);
            if (polygon == null)
            {
                return NotFound(new { message = "Полигон не найден" });
            }
            return Ok(polygon);
        }
    }
}
