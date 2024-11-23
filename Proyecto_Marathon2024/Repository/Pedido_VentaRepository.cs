using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class Pedido_VentaRepository
    {
        private readonly DataAccses dataAccses;

        public Pedido_VentaRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }


        public async Task<List<Ventas_Perso>> GetListaVentaPerso()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarVentasPerso", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Ventas_Perso> listaVenta_Perso = new List<Ventas_Perso>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                Ventas_Perso ventas_Perso = new Ventas_Perso
                                {
                                    Num_Documento = reader.GetInt32(reader.GetOrdinal("Num_Documento")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombres")),
                                    Apellido = reader.GetString(reader.GetOrdinal("Apellidos")),
                                    Fecha_Doc = reader.GetDateTime(reader.GetOrdinal("Fecha_Doc")),
                                    Nom_Prod = reader.GetString(reader.GetOrdinal("Nom_Prod")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Precio_Venta = reader.GetDecimal(reader.GetOrdinal("Precio_Venta")),
                                };

                                // Añadir el perfil a la lista
                                listaVenta_Perso.Add(ventas_Perso);
                            }

                            return listaVenta_Perso;
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

        public async Task<string> InsertPed_Venta(Pedido_Venta ped_venta)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_InsertPd_Venta", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@cod_tp_compro", SqlDbType.Int).Value = ped_venta.Cod_Tipo_Compro;
                        command.Parameters.Add("@cod_tp_pago", SqlDbType.Int).Value = ped_venta.Cod_Tp_Pago;
                        command.Parameters.Add("@dni_cliente", SqlDbType.VarChar).Value = ped_venta.Dni_Cliente;
                        command.Parameters.Add("@tipo_envio", SqlDbType.VarChar).Value = ped_venta.Tipo_Envio;
                        command.Parameters.Add("@direccion", SqlDbType.VarChar).Value = ped_venta.Direccion_Envio;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        //operación exitosa
                        return "Venta insertada correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al insertar la venta.", ex);
            }
        }

        public async Task<string> InsertDetallePed_Venta(DetallePedido_Venta det_ped_venta)
        {
            try
            {
                // Crea la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crea el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_InsertDetallesPd_Venta", connection))
                    {
                        // Especifica que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Añadir los parámetros requeridos por el procedimiento almacenado
                        command.Parameters.Add("@cod_ped_num_docu", SqlDbType.Int).Value = det_ped_venta.Cod_ped_num_docu;
                        command.Parameters.Add("@cod_prod", SqlDbType.Int).Value = det_ped_venta.Cod_prod;
                        command.Parameters.Add("@cantidad", SqlDbType.VarChar).Value = det_ped_venta.Cantidad;
                        command.Parameters.Add("@precio_venta", SqlDbType.VarChar).Value = det_ped_venta.Precio_Venta;

                        // Ejecutar el comando de manera asíncrona
                        await command.ExecuteNonQueryAsync();

                        //operación exitosa
                        return "Detalle Venta insertada correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al insertar el detalle de venta.", ex);
            }
        }
    }
}
