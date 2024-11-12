using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Net;

namespace Proyecto_Marathon2024.Repository
{
    public class PersonalRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public PersonalRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Personal>> GetListaPersonal()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarPersonal", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Personal> listaPersonal = new List<Personal>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Perfil_Personal y asignar los valores leídos
                                string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                DateOnly fecha = DateOnly.Parse(fec);
                                Personal personal = new Personal
                                {
                                    Dni_Personal = reader.GetString(reader.GetOrdinal("Dni_Personal")),
                                    Cod_Perfil = reader.GetInt32(reader.GetOrdinal("Cod_Perfil")),
                                    Cod_Tipo_Trabajo = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Trabajo")),
                                    Cod_Local = reader.GetInt32(reader.GetOrdinal("Cod_Local")),
                                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                                    Fecha_Nac = fecha,
                                    Genero = reader.GetString(reader.GetOrdinal("Genero")),
                                    User_Per = reader.GetString(reader.GetOrdinal("User_Per")),
                                    Contra_Per = reader.GetString(reader.GetOrdinal("Contra_Per")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                }; 

                                // Añadir el perfil a la lista
                                listaPersonal.Add(personal);
                            }

                            return listaPersonal;
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
        public async Task<Personal> GetPersonal_PorDni(string dni)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_BucarPersonalPorDNI", connection))
                    {
                        //Se Especifica que es un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadimos el parametro 'id' para el procedimiento almacenado
                        command.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Si encuentra un resultado, devolver el perfil
                            if (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Perfil_Personal y asignar los valores leídos
                                string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                DateOnly fecha = DateOnly.Parse(fec);
                                Personal personal = new Personal
                                {
                                    Dni_Personal = reader.GetString(reader.GetOrdinal("Dni_Personal")),
                                    Cod_Perfil = reader.GetInt32(reader.GetOrdinal("Cod_Perfil")),
                                    Cod_Tipo_Trabajo = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Trabajo")),
                                    Cod_Local = reader.GetInt32(reader.GetOrdinal("Cod_Local")),
                                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                                    Fecha_Nac = fecha,
                                    Genero = reader.GetString(reader.GetOrdinal("Genero")),
                                    User_Per = reader.GetString(reader.GetOrdinal("User_Per")),
                                    Contra_Per = reader.GetString(reader.GetOrdinal("Contra_Per")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                return personal;
                            }
                            else
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
        public async Task<string> InsertPersonal(Personal perso)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_InsertPersonal", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@dni", SqlDbType.VarChar).Value = perso.Dni_Personal;
                        command.Parameters.Add("@cod_perfil", SqlDbType.Int).Value = perso.Cod_Perfil;
                        command.Parameters.Add("@cod_tp_trabajo", SqlDbType.Int).Value = perso.Cod_Tipo_Trabajo;
                        command.Parameters.Add("@cod_local", SqlDbType.Int).Value = perso.Cod_Local;
                        command.Parameters.Add("@nom", SqlDbType.VarChar).Value = perso.Nombres;
                        command.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = perso.Apellidos;
                        command.Parameters.Add("@correo", SqlDbType.VarChar).Value = perso.Correo;
                        command.Parameters.Add("@fec_nac", SqlDbType.Date).Value = perso.Fecha_Nac.ToDateTime(TimeOnly.MinValue);
                        command.Parameters.Add("@genero", SqlDbType.VarChar).Value = perso.Genero;
                        command.Parameters.Add("@user_per", SqlDbType.VarChar).Value = perso.User_Per;
                        command.Parameters.Add("@contra_per", SqlDbType.VarChar).Value = perso.Contra_Per;
                        //command.Parameters.Add("@estado", SqlDbType.Int).Value = perso.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Personal insertado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al insertar el personal.", ex);
            }
        }

        
        public async Task<string> UpdatePersonal_porDni(string dni, Personal perso)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_UpdatePersonalPorDNI", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
                        //
                        command.Parameters.Add("@cod_perfil", SqlDbType.Int).Value = perso.Cod_Perfil;
                        command.Parameters.Add("@cod_tp_trabajo", SqlDbType.Int).Value = perso.Cod_Tipo_Trabajo;
                        command.Parameters.Add("@cod_local", SqlDbType.Int).Value = perso.Cod_Local;
                        command.Parameters.Add("@nom", SqlDbType.VarChar).Value = perso.Nombres;
                        command.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = perso.Apellidos;
                        command.Parameters.Add("@correo", SqlDbType.VarChar).Value = perso.Correo;
                        command.Parameters.Add("@fec_nac", SqlDbType.Date).Value = perso.Fecha_Nac.ToDateTime(TimeOnly.MinValue);
                        command.Parameters.Add("@genero", SqlDbType.VarChar).Value = perso.Genero;
                        command.Parameters.Add("@user_per", SqlDbType.VarChar).Value = perso.User_Per;
                        command.Parameters.Add("@contra_per", SqlDbType.VarChar).Value = perso.Contra_Per;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = perso.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Personal con Dni " + dni + " actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al actualizar el personal.", ex);
            }
        }

        public async Task<string> DeletePersonal_porDni(string dni)
        {
            try
            {
                // Crea la conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeletePersonalPorDNI", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // pasamos el dni a eliminar
                        command.Parameters.Add("@dni", SqlDbType.VarChar).Value = dni;
                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        return "Personal con Dni " + dni + " Eliminado CORRECTAMENTE!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al eliminar el personal.", ex);
            }

        }

        //Select Tipo trabajo


    }
}
