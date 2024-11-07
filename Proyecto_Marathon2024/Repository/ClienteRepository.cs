using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class ClienteRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public ClienteRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Cliente>> GetListaCliente()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarCliente", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Cliente> listaCliente = new List<Cliente>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Cliente y asignar los valores leídos
                                string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                DateOnly fecha = DateOnly.Parse(fec);
                                Cliente cliente = new Cliente
                                {
                                    Dni_Cliente = reader.GetString(reader.GetOrdinal("Dni_Cliente")),
                                    Cod_Mem = reader.GetInt32(reader.GetOrdinal("Cod_Mem")),
                                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                                    Fecha_Nac = fecha,
                                    Genero = reader.GetString(reader.GetOrdinal("Genero")),
                                    Puntos_Acum = reader.GetInt32(reader.GetOrdinal("Puntos_Acum")),
                                    User_Cli = reader.GetString(reader.GetOrdinal("User_Cli")),
                                    Contra_Cli = reader.GetString(reader.GetOrdinal("Contra_Cli")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaCliente.Add(cliente);
                            }

                            return listaCliente;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de perfiles del cliente", ex);
            }
        }
        public async Task<Cliente> GetClienteDni(string dni)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_BuscarClienteDNI", connection))
                    {
                        //Se Especifica que es un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadimos el parametro 'id' para el procedimiento almacenado
                        command.Parameters.Add("@dni_cliente", SqlDbType.VarChar).Value = dni;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Si encuentra un resultado, devolver el perfil
                            if (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Cliente y asignar los valores leídos
                                string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                DateOnly fecha = DateOnly.Parse(fec);
                                Cliente cliente = new Cliente
                                {
                                    Dni_Cliente = reader.GetString(reader.GetOrdinal("Dni_Cliente")),
                                    Cod_Mem = reader.GetInt32(reader.GetOrdinal("Cod_Mem")),
                                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                                    Correo = reader.GetString(reader.GetOrdinal("Correo")),
                                    Fecha_Nac = fecha,
                                    Genero = reader.GetString(reader.GetOrdinal("Genero")),
                                    Puntos_Acum = reader.GetInt32(reader.GetOrdinal("Puntos_Acum")),
                                    User_Cli = reader.GetString(reader.GetOrdinal("User_Cli")),
                                    Contra_Cli = reader.GetString(reader.GetOrdinal("Contra_Cli")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                return cliente;
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
                throw new Exception("Error al obtener el perfil del cliente por ID.", ex);
            }
        }
        public async Task<string> InsertCliente(Cliente perso)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_InsertCliente", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@dni_cliente", SqlDbType.VarChar).Value = perso.Dni_Cliente;
                        command.Parameters.Add("@cod_mem", SqlDbType.Int).Value = perso.Cod_Mem;
                        command.Parameters.Add("@nombres", SqlDbType.VarChar).Value = perso.Nombres;
                        command.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = perso.Apellidos;
                        command.Parameters.Add("@correo", SqlDbType.VarChar).Value = perso.Correo;
                        command.Parameters.Add("@fec_nac", SqlDbType.Date).Value = perso.Fecha_Nac.ToDateTime(TimeOnly.MinValue);
                        command.Parameters.Add("@genero", SqlDbType.VarChar).Value = perso.Genero;
                        command.Parameters.Add("@puntos_acum", SqlDbType.Int).Value = perso.Puntos_Acum;
                        command.Parameters.Add("@user_cli", SqlDbType.VarChar).Value = perso.User_Cli;
                        command.Parameters.Add("@contra_cli", SqlDbType.VarChar).Value = perso.Contra_Cli;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = perso.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Cliente insertado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al insertar el cliente.", ex);
            }
        }


        public async Task<string> UpdateClienteDni(string dni, Cliente perso)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_UpdateClienteDNI", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@dni_cliente", SqlDbType.VarChar).Value = dni;
                        //
                        command.Parameters.Add("@cod_mem", SqlDbType.Int).Value = perso.Cod_Mem;
                        command.Parameters.Add("@correo", SqlDbType.VarChar).Value = perso.Correo;
                        command.Parameters.Add("@user_cli", SqlDbType.VarChar).Value = perso.User_Cli;
                        command.Parameters.Add("@contra_cli", SqlDbType.VarChar).Value = perso.Contra_Cli;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = perso.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Cliente con Dni " + dni + " actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al actualizar el Cliente.", ex);
            }
        }

        public async Task<string> DesactivarClienteDNI(string dni)
        {
            try
            {
                // Crea la conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DesactivarClienteDNI", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // pasamos el dni a eliminar
                        command.Parameters.Add("@dni_cliente", SqlDbType.VarChar).Value = dni;
                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        return "Cliente con Dni " + dni + " Eliminado CORRECTAMENTE!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al eliminar el cliente.", ex);
            }

        }
    }
}
