using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using Proyecto_Marathon2024.Services;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Proyecto_Marathon2024.Repository
{
    public class Perfil_PersonalRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public Perfil_PersonalRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        //Este metodo retorna una lista de tipo perfil_personal
        public async Task<List<Perfil_Personal>> GetListaPerfil_Personal()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarPerfilPersonal", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Perfil_Personal> listaPerfil = new List<Perfil_Personal>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Perfil_Personal y asignar los valores leídos
                                Perfil_Personal perfil = new Perfil_Personal
                                {
                                    Cod_Perfil = reader.GetInt32(reader.GetOrdinal("Cod_Perfil")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Sueldo = reader.GetDecimal(reader.GetOrdinal("Sueldo")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaPerfil.Add(perfil);
                            }

                            return listaPerfil;
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

        //Metodo que retorna un perfil personal por ID
        public async Task<Perfil_Personal> GetPerfilPerso_PorID(int id)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_BuscarPerfilPorID", connection))
                    {
                        //Se Especifica que es un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadimos el parametro 'id' para el procedimiento almacenado
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Si encuentra un resultado, devolver el perfil
                            if (await reader.ReadAsync())
                            {
                                Perfil_Personal perfil = new Perfil_Personal
                                {
                                    Cod_Perfil = reader.GetInt32(reader.GetOrdinal("Cod_Perfil")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Sueldo = reader.GetDecimal(reader.GetOrdinal("Sueldo")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                return perfil;
                            }else
                            {
                                return null;
                            }                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener el perfil personal por ID.", ex);
            }
        }

        public async Task<string> InsertPerfilPerso(Perfil_Personal pf_perso)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_InsertPerfilPersonal", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@desc", SqlDbType.VarChar).Value = pf_perso.Descripcion;
                        command.Parameters.Add("@sueldo", SqlDbType.Decimal).Value = pf_perso.Sueldo;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = pf_perso.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        //operación exitosa
                        return "Perfil insertado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al insertar el perfil personal.", ex);
            }
        }

        public async Task<string> UpdatePerfilPerso_porID(int id, Perfil_Personal pf_perso)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_UpdatePerfilPorID", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@desc", SqlDbType.VarChar).Value = pf_perso.Descripcion;
                        command.Parameters.Add("@sueldo", SqlDbType.Decimal).Value = pf_perso.Sueldo;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = pf_perso.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Perfil Personal con ID " + id +" actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al actualizar el perfil personal.", ex);
            }
        }

        public async Task<string> DeletePerfilPerso_porID(int id)
        {
            try
            {
                // Crea la conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeletePerfilPorID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // pasamos el ID a eliminar
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        return "Perfil Personal con ID"+ id +" Eliminado CORRECTAMENTE!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al eliminar el perfil personal.", ex);
            }

        }
    }
}
