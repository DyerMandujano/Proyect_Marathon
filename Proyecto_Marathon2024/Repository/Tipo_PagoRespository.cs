using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class Tipo_PagoRespository
    {
        private readonly DataAccses dataAccses;

        public Tipo_PagoRespository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Tipo_Pago>> GetListaTipo_Pago()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarTipo_Pago", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Tipo_Pago> listaTipoPago = new List<Tipo_Pago>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Perfil_Personal y asignar los valores leídos
                                Tipo_Pago tipoPago = new Tipo_Pago
                                {
                                    Cod_Tp_Pago = reader.GetInt32(reader.GetOrdinal("Cod_Tp_Pago")),
                                    Nom_Tp_Pago = reader.GetString(reader.GetOrdinal("Nom_Tp_Pago")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaTipoPago.Add(tipoPago);
                            }

                            return listaTipoPago;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de perfiles personales.", ex);
            }
        }
    }
}
