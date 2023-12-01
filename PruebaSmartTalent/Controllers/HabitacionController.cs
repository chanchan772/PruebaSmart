using AccesoDatos;
using Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PruebaSmartTalent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : Controller
    {
        private readonly IConfiguration _configuration;
        public HabitacionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{hotelId}")]
        public IActionResult GetHabitacionesPorHotel(int hotelId)
        {
            daHabitacion databaseManager = new daHabitacion(_configuration.GetConnectionString("MySQLConnection"));
            var habitaciones = databaseManager.GetHabitacionesPorHotel(hotelId);
            return Ok(habitaciones);
        }

        [HttpPost]
        public IActionResult CrearHabitacion([FromBody] beHabitacion habitacion)
        {
            daHabitacion databaseManager = new daHabitacion(_configuration.GetConnectionString("MySQLConnection"));
            var nuevaHabitacion = databaseManager.CrearHabitacion(habitacion);
            return Ok(nuevaHabitacion);
        }

        [HttpGet("detalle/{id}")]
        public IActionResult GetHabitacionById(int id)
        {
            daHabitacion databaseManager = new daHabitacion(_configuration.GetConnectionString("MySQLConnection"));
            var habitacion = databaseManager.GetHabitacionById(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            return Ok(habitacion);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarHabitacion(int id, [FromBody] beHabitacion habitacion)
        {
            daHabitacion databaseManager = new daHabitacion(_configuration.GetConnectionString("MySQLConnection"));
            var habitacionActualizada = databaseManager.ActualizarHabitacion(id, habitacion);
            if (habitacionActualizada == null)
            {
                return NotFound();
            }
            return Ok(habitacionActualizada);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarHabitacion(int id)
        {
            daHabitacion databaseManager = new daHabitacion(_configuration.GetConnectionString("MySQLConnection"));
            var habitacionEliminada = databaseManager.EliminarHabitacion(id);
            if (habitacionEliminada == null)
            {
                return NotFound();
            }
            return Ok(habitacionEliminada);
        }
    }
}
