using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class Tipo_TrabajoRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public Tipo_TrabajoRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Tipo_Trabajo>> GetListaTp_Trabajo()
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarTpTrabajo", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Tipo_Trabajo> listatpTrab = new List<Tipo_Trabajo>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                Tipo_Trabajo tp_trab = new Tipo_Trabajo
                                {
                                    Cod_Tipo_Trabajo = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Trabajo")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                               
                                listatpTrab.Add(tp_trab);
                            }

                            return listatpTrab;
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
