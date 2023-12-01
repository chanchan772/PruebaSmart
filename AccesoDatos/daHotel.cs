using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class daHotel
    {
        private string _connection;

        public daHotel(string connectionString)
        {
            this._connection = connectionString;
        }
        public List<beHotel> GetHoteles()
        {
            List<beHotel> hoteles = new List<beHotel>();
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Hotels";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        beHotel hotel = new beHotel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Comisiones = Convert.ToDecimal(reader["Comisiones"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                        hoteles.Add(hotel);
                    }
                    connection.Close();
                }
            }
            return hoteles;
        }

        public beHotel CrearHotel(beHotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "INSERT INTO Hotels (Nombre, Direccion, Comisiones, Estado) " +
                               "VALUES (@Nombre, @Direccion, @Comisiones, @Estado);" +
                               "SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", hotel.Nombre);
                    command.Parameters.AddWithValue("@Direccion", hotel.Direccion);
                    command.Parameters.AddWithValue("@Comisiones", hotel.Comisiones);
                    command.Parameters.AddWithValue("@Estado", hotel.Estado);

                    connection.Open();
                    int hotelId = Convert.ToInt32(command.ExecuteScalar());
                    hotel.Id = hotelId;
                    connection.Close();
                }
            }
            return hotel;
        }

        public beHotel GetHotelById(int id)
        {
            beHotel hotel = null;
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Hotels WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        hotel = new beHotel
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = reader["Nombre"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Comisiones = Convert.ToDecimal(reader["Comisiones"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                    connection.Close();
                }
            }
            return hotel;
        }

        public beHotel ActualizarHotel(int id, beHotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "UPDATE Hotels SET Nombre = @Nombre, Direccion = @Direccion, " +
                               "Comisiones = @Comisiones, Estado = @Estado WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", hotel.Nombre);
                    command.Parameters.AddWithValue("@Direccion", hotel.Direccion);
                    command.Parameters.AddWithValue("@Comisiones", hotel.Comisiones);
                    command.Parameters.AddWithValue("@Estado", hotel.Estado);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return hotel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public beHotel EliminarHotel(int id)
        {
            beHotel hotel = GetHotelById(id); // Obtener el hotel a eliminar antes de eliminarlo
            if (hotel != null)
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    string query = "DELETE FROM Hotels WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            return hotel;
                        }
                    }
                }
            }
            return null;
        }

        public List<beHabitacion> ObtenerHabitacionesDisponibles(int hotelId, DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas)
        {
            List<beHabitacion> habitacionesDisponibles = new List<beHabitacion>();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                
                string query = "SELECT * FROM Habitaciones WHERE HotelId = @HotelId AND Disponibilidad = 1"; // Modifica la consulta según tu estructura
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@HotelId", hotelId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        beHabitacion habitacion = new beHabitacion
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Tipo = reader["Tipo"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"]),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            HotelId = Convert.ToInt32(reader["HotelId"])
                        };

                        habitacionesDisponibles.Add(habitacion);
                    }

                    reader.Close();
                
            }

            return habitacionesDisponibles;
        }
    }
}
