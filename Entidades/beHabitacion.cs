using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class beHabitacion
    {
        public int Id { get; set; } // Identificador único de la habitación
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
        public bool Disponibilidad { get; set; } // Indica si la habitación está disponible o no
        public bool Estado { get; set; } // Indica si la habitación está habilitada o deshabilitada

        // Relación con el hotel al que pertenece esta habitación
        public int HotelId { get; set; }
        public beHotel Hotel { get; set; }
    }
}
