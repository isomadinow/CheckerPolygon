using PolygonApp.DTOs;
using PolygonApp.Services;
using PolygonApp.Models;
using Microsoft.AspNetCore.Mvc;
using backend.DTOs;

namespace PolygonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolygonController : ControllerBase
    {
        private readonly PolygonService _polygonService;
        private readonly PolygonFileService _polygonFileService;

        public PolygonController(PolygonService polygonService, PolygonFileService polygonFileService)
        {
            _polygonService = polygonService;
            _polygonFileService = polygonFileService;
        }

        /// <summary>
        /// Проверяет, находится ли указанная точка внутри полигона.
        /// </summary>
        /// <param name="request">Запрос, содержащий данные о точке и полигоне.</param>
        /// <returns>Возвращает true, если точка находится внутри полигона, иначе false.</returns>
        [HttpPost("check-point")]
        public IActionResult CheckPoint([FromBody] PointInPolygonRequest request)
        {
            Polygon polygon;
            polygon = request.Polygon;
            var isInside = _polygonService.IsPointInPolygon(request.Point, polygon.Vertices);
            return Ok(new { inside = isInside });
        }

        /// <summary>
        /// Сохраняет новый полигон.
        /// </summary>
        /// <param name="polygonRequest">Данные полигона для сохранения.</param>
        /// <returns>Возвращает ID нового сохраненного полигона.</returns>
        [HttpPost("save")]
        public IActionResult SavePolygon([FromBody] PolygonRequestDto polygonRequest)
        {
            // Создаем объект Polygon на основе данных из DTO
            var polygon = new Polygon { Vertices = polygonRequest.Vertices };

            // Сохраняем полигон через сервис, который генерирует ID
            var polygonId = _polygonFileService.SavePolygonToCsv(polygon);
            return Ok(new { message = "Полигон успешно сохранен", id = polygonId });
        }

        /// <summary>
        /// Загружает все сохраненные полигоны.
        /// </summary>
        /// <returns>Возвращает список всех полигонов.</returns>
        [HttpGet("load-all")]
        public IActionResult LoadAllPolygons()
        {
            var polygons = _polygonFileService.LoadAllPolygonsFromCsv();
            return Ok(polygons);
        }

        /// <summary>
        /// Загружает полигон по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор полигона для загрузки.</param>
        /// <returns>Возвращает данные полигона с указанным ID, если он существует.</returns>
        [HttpGet("load/{id}")]
        public IActionResult LoadPolygonById(int id)
        {
            var polygon = _polygonFileService.LoadPolygonById(id);
            if (polygon == null)
            {
                return NotFound(new { message = "Полигон не найден" });
            }
            return Ok(polygon);
        }
    }
}
