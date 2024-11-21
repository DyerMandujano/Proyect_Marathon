using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class DetallePedidoRepository
    {
        private readonly DataAccses dataAccses;

        // Constructor para el acceso a la base de datos
        public DetallePedidoRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        // Método para insertar un detalle de pedido
        public async Task<string> InsertDetallePedido(int codPedido, int codProd, int cantidad, decimal precioVenta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Insertar_Detalle_Pedido", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_Pedido", SqlDbType.Int).Value = codPedido;
                        command.Parameters.Add("@Cod_Prod", SqlDbType.Int).Value = codProd;
                        command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
                        command.Parameters.Add("@Precio_Venta", SqlDbType.Decimal).Value = precioVenta;

                        await command.ExecuteNonQueryAsync();
                        return "Detalle de pedido insertado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el detalle de pedido.", ex);
            }
        }

        // Método para listar todos los detalles de pedidos
        public async Task<List<Detalle_Pedido>> GetListaDetallesPedido()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Listar_Detalle_Pedido", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<Detalle_Pedido> listaDetalles = new List<Detalle_Pedido>();

                            while (await reader.ReadAsync())
                            {
                                Detalle_Pedido detalle = new Detalle_Pedido
                                {
                                    Cod_Det_Ped = reader.GetInt32(reader.GetOrdinal("Cod_Det_Ped")),
                                    Cod_Pedido = reader.GetInt32(reader.GetOrdinal("Cod_Pedido")),
                                    Cod_Prod = reader.GetInt32(reader.GetOrdinal("Cod_Prod")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Precio_Venta = reader.GetDecimal(reader.GetOrdinal("Precio_Venta"))
                                };
                                listaDetalles.Add(detalle);
                            }
                            return listaDetalles;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de detalles de pedidos.", ex);
            }
        }

        // Método para obtener un detalle de pedido por su ID
        public async Task<Detalle_Pedido> GetDetallePedidoPorID(int codDetPed)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Obtener_Detalle_Pedido", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_Det_Ped", SqlDbType.Int).Value = codDetPed;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Detalle_Pedido
                                {
                                    Cod_Det_Ped = reader.GetInt32(reader.GetOrdinal("Cod_Det_Ped")),
                                    Cod_Pedido = reader.GetInt32(reader.GetOrdinal("Cod_Pedido")),
                                    Cod_Prod = reader.GetInt32(reader.GetOrdinal("Cod_Prod")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Precio_Venta = reader.GetDecimal(reader.GetOrdinal("Precio_Venta"))
                                };
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
                throw new Exception("Error al obtener el detalle de pedido por ID.", ex);
            }
        }

        // Método para actualizar un detalle de pedido
        public async Task<string> UpdateDetallePedido(int codDetPed, int codPedido, int codProd, int cantidad, decimal precioVenta)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Actualizar_Detalle_Pedido", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_Det_Ped", SqlDbType.Int).Value = codDetPed;
                        command.Parameters.Add("@Cod_Pedido", SqlDbType.Int).Value = codPedido;
                        command.Parameters.Add("@Cod_Prod", SqlDbType.Int).Value = codProd;
                        command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad;
                        command.Parameters.Add("@Precio_Venta", SqlDbType.Decimal).Value = precioVenta;

                        await command.ExecuteNonQueryAsync();
                        return "Detalle de pedido actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el detalle de pedido.", ex);
            }
        }
       
    }
}
