using AccesoDatos;
using Entidades;
using Microsoft.AspNetCore.Mvc;

namespace PruebaSmartTalent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        private readonly IConfiguration _configuration;
        public HotelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetHoteles()
        {
            daHotel databaseManager = new daHotel(_configuration.GetConnectionString("MySQLConnection"));
            var hoteles = databaseManager.GetHoteles();
            return Ok(hoteles);
        }

        [HttpPost]
        public IActionResult CrearHotel([FromBody] beHotel hotel)
        {
            daHotel databaseManager = new daHotel(_configuration.GetConnectionString("MySQLConnection"));
            var nuevoHotel = databaseManager.CrearHotel(hotel);
            return Ok(nuevoHotel);
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {
            daHotel databaseManager = new daHotel(_configuration.GetConnectionString("MySQLConnection"));
            var hotel = databaseManager.GetHotelById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarHotel(int id, [FromBody] beHotel hotel)
        {
            
            daHotel databaseManager = new daHotel(_configuration.GetConnectionString("MySQLConnection"));
            var hotelActualizado = databaseManager.ActualizarHotel(id, hotel);
            if (hotelActualizado == null)
            {
                return NotFound();
            }
            return Ok(hotelActualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarHotel(int id)
        {
            
            daHotel databaseManager = new daHotel(_configuration.GetConnectionString("MySQLConnection"));
            var hotelEliminado = databaseManager.EliminarHotel(id);
            if (hotelEliminado == null)
            {
                return NotFound();
            }
            return Ok(hotelEliminado);
        }

        [HttpGet("{hotelId}/HabitacionesDisponibles")]
        public IActionResult ObtenerHabitacionesDisponibles(int hotelId, DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas)
        {
            daHotel databaseManager = new daHotel(_configuration.GetConnectionString("MySQLConnection"));
            var habitacionesDisponibles = databaseManager.ObtenerHabitacionesDisponibles(hotelId, fechaEntrada, fechaSalida, cantidadPersonas);
            return Ok(habitacionesDisponibles);
        }
    }
}
