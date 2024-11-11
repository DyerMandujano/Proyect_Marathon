using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Net;


namespace Proyecto_Marathon2024.Repository
{
     public class ProductoRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public ProductoRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Producto>> GetListaProducto()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarProducto", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Producto> listaProducto = new List<Producto>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Producto y asignar los valores leídos
                                //string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                //DateOnly fecha = DateOnly.Parse(fec);
                                Producto producto = new Producto
                                {
                                    Cod_Prod = reader.GetInt32(reader.GetOrdinal("Cod_Prod")),
                                    Cod_Modelo = reader.GetInt32(reader.GetOrdinal("Cod_Modelo")),
                                    Cod_Color = reader.GetInt32(reader.GetOrdinal("Cod_Color")),
                                    Cod_Marca = reader.GetInt32(reader.GetOrdinal("Cod_Marca")),
                                    Cod_Cate = reader.GetInt32(reader.GetOrdinal("Cod_Cate")),
                                    Cod_Talla = reader.GetInt32(reader.GetOrdinal("Cod_Talla")),
                                    Nom_Prod = reader.GetString(reader.GetOrdinal("Nom_Prod")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    Image_back = reader.GetString(reader.GetOrdinal("Image_back")),
                                    Image_front = reader.GetString(reader.GetOrdinal("Image_front")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaProducto.Add(producto);
                            }

                            return listaProducto;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de productos.", ex);
            }
        }
        public async Task<Producto> GetProducto_PorID(int id)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_BuscarProductoPorID", connection))
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
                                // Crear una nueva instancia de Perfil_Personal y asignar los valores leídos
                                //string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                //DateOnly fecha = DateOnly.Parse(fec);
                                Producto producto = new Producto
                                {
                                    Cod_Prod = reader.GetInt32(reader.GetOrdinal("Cod_Prod")),
                                    Cod_Modelo = reader.GetInt32(reader.GetOrdinal("Cod_Modelo")),
                                    Cod_Color = reader.GetInt32(reader.GetOrdinal("Cod_Color")),
                                    Cod_Marca = reader.GetInt32(reader.GetOrdinal("Cod_Marca")),
                                    Cod_Cate = reader.GetInt32(reader.GetOrdinal("Cod_Cate")),
                                    Cod_Talla = reader.GetInt32(reader.GetOrdinal("Cod_Talla")),
                                    Nom_Prod = reader.GetString(reader.GetOrdinal("Nom_Prod")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    Image_front = reader.GetString(reader.GetOrdinal("Image_front")),
                                    Image_back = reader.GetString(reader.GetOrdinal("Image_back")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                return producto;
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
                throw new Exception("Error al obtener el producto por ID.", ex);
            }
        }
        public async Task<string> SP_InsertProducto(Producto prod)
        {
            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_InsertProducto", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@cod_modelo", SqlDbType.Int).Value = prod.Cod_Modelo;
                        command.Parameters.Add("@cod_color", SqlDbType.Int).Value = prod.Cod_Color;
                        command.Parameters.Add("@cod_marca", SqlDbType.Int).Value = prod.Cod_Marca;
                        command.Parameters.Add("@cod_cate", SqlDbType.Int).Value = prod.Cod_Cate;
                        command.Parameters.Add("@cod_talla", SqlDbType.Int).Value = prod.Cod_Talla;
                        command.Parameters.Add("@nom_prod", SqlDbType.VarChar).Value = prod.Nom_Prod;
                        command.Parameters.Add("@descipcion", SqlDbType.VarChar).Value = prod.Descripcion;
                        command.Parameters.Add("@precio", SqlDbType.Decimal).Value = prod.Precio;
                        //command.Parameters.Add("@image_front", SqlDbType.VarChar).Value = prod.Image_front;
                        //command.Parameters.Add("@image_back", SqlDbType.VarChar).Value = prod.Image_back;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = prod.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Producto insertado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al insertar el producto.", ex);
            }
        }


        public async Task<string> UpdateProducto_porID(int id, Producto prod)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_UpdateProductoPorID", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        //
                        command.Parameters.Add("@cod_modelo", SqlDbType.Int).Value = prod.Cod_Modelo;
                        command.Parameters.Add("@cod_color", SqlDbType.Int).Value = prod.Cod_Color;
                        command.Parameters.Add("@cod_marca", SqlDbType.Int).Value = prod.Cod_Marca;
                        command.Parameters.Add("@cod_cate", SqlDbType.Int).Value = prod.Cod_Cate;
                        command.Parameters.Add("@cod_talla", SqlDbType.Int).Value = prod.Cod_Talla;
                        command.Parameters.Add("@nom_prod", SqlDbType.VarChar).Value = prod.Nom_Prod;
                        command.Parameters.Add("@descipcion", SqlDbType.VarChar).Value = prod.Descripcion;
                        command.Parameters.Add("@precio", SqlDbType.Decimal).Value = prod.Precio;
                        command.Parameters.Add("@image_front", SqlDbType.VarChar).Value = prod.Image_front;
                        command.Parameters.Add("@image_back", SqlDbType.VarChar).Value = prod.Image_back;
                        command.Parameters.Add("@estado", SqlDbType.Int).Value = prod.Estado;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        // Operación exitosa
                        return "Producto con id " + id + " actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al actualizar el producto.", ex);
            }
        }

        public async Task<string> DeleteProducto_porID(int id)
        {
            try
            {
                // Crea la conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("SP_DeleteProductoPorID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        // pasamos el id a eliminar
                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        return "Producto con ID " + id + " Eliminado CORRECTAMENTE!";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al eliminar el producto.", ex);
            }

        }
    }
}

