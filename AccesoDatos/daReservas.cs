using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class daReservas
    {
        private string _connection;

        public daReservas(string connectionString)
        {
            this._connection = connectionString;
        }

        public List<beReservas> GetReservas()
        {
            List<beReservas> reservas = new List<beReservas>();
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Reservas";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        beReservas reserva = new beReservas
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Cliente = reader["Cliente"].ToString(),
                            HabitacionId = Convert.ToInt32(reader["HabitacionId"])
                        };
                        reservas.Add(reserva);
                    }
                    connection.Close();
                }
            }
            return reservas;
        }

        public beReservas CrearReserva(beReservas reserva)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "INSERT INTO Reservas (Fecha, Cliente, HabitacionId) " +
                               "VALUES (@Fecha, @Cliente, @HabitacionId);" +
                               "SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fecha", reserva.Fecha);
                    command.Parameters.AddWithValue("@Cliente", reserva.Cliente);
                    command.Parameters.AddWithValue("@HabitacionId", reserva.HabitacionId);

                    connection.Open();
                    int reservaId = Convert.ToInt32(command.ExecuteScalar());
                    reserva.Id = reservaId;
                    connection.Close();
                }
            }
            return reserva;
        }

        public beReservas GetReservaById(int id)
        {
            beReservas reserva = null;
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Reservas WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        reserva = new beReservas
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Cliente = reader["Cliente"].ToString(),
                            HabitacionId = Convert.ToInt32(reader["HabitacionId"])
                        };
                    }
                    connection.Close();
                }
            }
            return reserva;
        }

        public beReservas ActualizarReserva(int id, beReservas reserva)
        {
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "UPDATE Reservas SET Fecha = @Fecha, Cliente = @Cliente, " +
                               "HabitacionId = @HabitacionId WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fecha", reserva.Fecha);
                    command.Parameters.AddWithValue("@Cliente", reserva.Cliente);
                    command.Parameters.AddWithValue("@HabitacionId", reserva.HabitacionId);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        return reserva;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public beReservas EliminarReserva(int id)
        {
            beReservas reserva = GetReservaById(id);
            if (reserva != null)
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    string query = "DELETE FROM Reservas WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();

                        if (rowsAffected > 0)
                        {
                            return reserva;
                        }
                    }
                }
            }
            return null;
        }


        public List<beHotel> ObtenerHotelesDisponibles(DateTime fechaEntrada, DateTime fechaSalida, int cantidadPersonas, string ciudadDestino)
        {
            List<beHotel> hotelesEncontrados = new List<beHotel>();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                string query = "SELECT * FROM Hotels WHERE Estado = 1 AND Direccion LIKE '%' + @Ciudad + '%'"; // Modifica la consulta según tu estructura
                

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Ciudad", ciudadDestino);

                
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

                        hotelesEncontrados.Add(hotel);
                    }

                    reader.Close();
                
            }

            return hotelesEncontrados;
        }

    }
}
