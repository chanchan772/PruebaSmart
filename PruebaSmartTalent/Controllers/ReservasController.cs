using AccesoDatos;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PruebaSmartTalent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : Controller
    {
        private readonly IConfiguration _configuration;
        public ReservasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetReservas()
        {
            try
            {
                daReservas databaseManager = new daReservas(_configuration.GetConnectionString("MySQLConnection"));
                var reservas = databaseManager.GetReservas();
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CrearReserva([FromBody] beReservas reserva)
        {
            try
            {
                daReservas databaseManager = new daReservas(_configuration.GetConnectionString("MySQLConnection"));
                var nuevaReserva = databaseManager.CrearReserva(reserva);
                return Ok(nuevaReserva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
        [HttpGet("{id}")]
        public IActionResult GetReservaById(int id)
        {
                try
                {
                    daReservas databaseManager = new daReservas(_configuration.GetConnectionString("MySQLConnection"));
                    var reserva = databaseManager.GetReservaById(id);
                    if (reserva == null)
                    {
                        return NotFound();
                    }
                    return Ok(reserva);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
        }


        [HttpPut("{id}")]
        public IActionResult ActualizarReserva(int id, [FromBody] beReservas reserva)
        {
            try
            {
                daReservas databaseManager = new daReservas(_configuration.GetConnectionString("MySQLConnection"));
                var reservaActualizada = databaseManager.ActualizarReserva(id, reserva);
                if (reservaActualizada == null)
                {
                    return NotFound();
                }
                return Ok(reservaActualizada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarReserva(int id)
        {
            try
            {
                daReservas databaseManager = new daReservas(_configuration.GetConnectionString("MySQLConnection"));
                var reservaEliminada = databaseManager.EliminarReserva(id);
                if (reservaEliminada == null)
                {
                    return NotFound();
                }
                return Ok(reservaEliminada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BuscarHoteles")]
        public IActionResult BuscarHoteles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudadDestino)
        {
            try { 
                if (fechaEntrada == default || fechaSalida == default || cantidadPersonas <= 0 || string.IsNullOrEmpty(ciudadDestino))
                {
                    return BadRequest("Parámetros de búsqueda incorrectos.");
                }
                daReservas databaseManager = new daReservas(_configuration.GetConnectionString("MySQLConnection"));
                var hotelesEncontrados = databaseManager.ObtenerHotelesDisponibles(fechaEntrada, fechaSalida, cantidadPersonas, ciudadDestino);
                return Ok(hotelesEncontrados);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
