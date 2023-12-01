using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class beHotel
    {
        public int Id { get; set; } // Identificador único del hotel
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public decimal Comisiones { get; set; }
        public bool Estado { get; set; } // Indica si el hotel está habilitado o deshabilitado

        // Relación con las habitaciones disponibles en el hotel
        public List<beHabitacion> Habitaciones { get; set; }
    }
}
