using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class beReservas
    {
        public int Id { get; set; } // Identificador único de la reserva
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }

        // Relación con la habitación reservada
        public int HabitacionId { get; set; }
        public beHabitacion Habitacion { get; set; }
    }
}
