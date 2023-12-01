using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class daHabitacion
    {
        private string _connection;

        public daHabitacion(string connectionString)
        {
            this._connection = connectionString;
        }

        public List<beHabitacion> GetHabitacionesPorHotel(int hotelId)
        {
            List<beHabitacion> habitaciones = new List<beHabitacion>();
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Habitaciones WHERE HotelId = @HotelId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
                        habitaciones.Add(habitacion);
                    }
                    connection.Close();
                }
            }
            return habitaciones;
        }

        public beHabitacion CrearHabitacion(beHabitacion habitacion)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "INSERT INTO Habitaciones (Tipo, Precio, Disponibilidad, Estado, HotelId) " +
                               "VALUES (@Tipo, @Precio, @Disponibilidad, @Estado, @HotelId);" +
                               "SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                    command.Parameters.AddWithValue("@Precio", habitacion.Precio);
                    command.Parameters.AddWithValue("@Disponibilidad", habitacion.Disponibilidad);
                    command.Parameters.AddWithValue("@Estado", habitacion.Estado);
                    command.Parameters.AddWithValue("@HotelId", habitacion.HotelId);

                    connection.Open();
                    int habitacionId = Convert.ToInt32(command.ExecuteScalar());
                    habitacion.Id = habitacionId;
                    connection.Close();
                }
            }
            return habitacion;
        }

        public beHabitacion GetHabitacionById(int id)
        {
            beHabitacion habitacion = null;
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Habitaciones WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        habitacion = new beHabitacion
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Tipo = reader["Tipo"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"]),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            HotelId = Convert.ToInt32(reader["HotelId"])
                        };
                    }
                    connection.Close();
                }
            }
            return habitacion;
        }

        public beHabitacion ActualizarHabitacion(int id, beHabitacion habitacion)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "UPDATE Habitaciones SET Tipo = @Tipo, Precio = @Precio, " +
                               "Disponibilidad = @Disponibilidad, Estado = @Estado WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Tipo", habitacion.Tipo);
                    command.Parameters.AddWithValue("@Precio", habitacion.Precio);
                    command.Parameters.AddWithValue("@Disponibilidad", habitacion.Disponibilidad);
                    command.Parameters.AddWithValue("@Estado", habitacion.Estado);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return habitacion;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public beHabitacion EliminarHabitacion(int id)
        {
            beHabitacion habitacion = GetHabitacionById(id);
            if (habitacion != null)
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    string query = "DELETE FROM Habitaciones WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            return habitacion;
                        }
                    }
                }
            }
            return null;
        }
    }
}
