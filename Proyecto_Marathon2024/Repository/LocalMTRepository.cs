using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class LocalMTRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public LocalMTRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<LocalMT>> GetListaLocal()
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarLocal", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<LocalMT> listaLocal = new List<LocalMT>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                LocalMT localMT = new LocalMT
                                {
                                    Cod_Local = reader.GetInt32(reader.GetOrdinal("Cod_Local")),
                                    Cod_Zona = reader.GetInt32(reader.GetOrdinal("Cod_Zona")),
                                    Cod_Sucursal = reader.GetInt32(reader.GetOrdinal("Cod_Sucursal")),
                                    Cod_Distrito = reader.GetInt32(reader.GetOrdinal("Cod_Distrito")),
                                    Nom_Local = reader.GetString(reader.GetOrdinal("Nom_Local")),
                                    Direccion = reader.GetString(reader.GetOrdinal("Direccion")),
                                    Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };


                                listaLocal.Add(localMT);
                            }

                            return listaLocal;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de Locales.", ex);
            }
        }
    }
}
