using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class Tipo_ComproRespository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public Tipo_ComproRespository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Tipo_Comprobante>> GetListaTipo_Compro()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarTipo_Compro", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Tipo_Comprobante> listatp_comprob = new List<Tipo_Comprobante>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Perfil_Personal y asignar los valores leídos
                                Tipo_Comprobante tp_comprobante = new Tipo_Comprobante
                                {
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Descrip = reader.GetString(reader.GetOrdinal("Descrip")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listatp_comprob.Add(tp_comprobante);
                            }

                            return listatp_comprob;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de Tipos Comprobantes.", ex);
            }
        }
    }
}
