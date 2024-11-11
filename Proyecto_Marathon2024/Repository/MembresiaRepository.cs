using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class MembresiaRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public MembresiaRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Membresia>> GetListaMembresia()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarMembresia", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Membresia> listaMembresia = new List<Membresia>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                              
                                Membresia membresia = new Membresia
                                {
                                  
                                    Cod_Mem = reader.GetInt32(reader.GetOrdinal("Cod_Mem")),
                                    Tipo_Membre = reader.GetString(reader.GetOrdinal("Tipo_Membre")),
                                    Puntos = reader.GetInt32(reader.GetOrdinal("Puntos")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaMembresia.Add(membresia);
                            }

                            return listaMembresia;
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de Membresias", ex);
            }
        }
    }
}
