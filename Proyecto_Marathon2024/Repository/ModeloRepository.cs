using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Net;


namespace Proyecto_Marathon2024.Repository
{
    public class ModeloRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public ModeloRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Modelo>> GetListaModelo()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarModelo", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Modelo> listaModelo= new List<Modelo>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Producto y asignar los valores leídos
                                //string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                //DateOnly fecha = DateOnly.Parse(fec);
                                Modelo modelo= new Modelo
                                {
                                    Cod_Modelo = reader.GetInt32(reader.GetOrdinal("Cod_Modelo")),
                                    Nom_Modelo = reader.GetString(reader.GetOrdinal("Nom_Modelo")),
                                    Estado     = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaModelo.Add(modelo);
                            }

                            return listaModelo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de modelo.", ex);
            }
        }
    }
}
