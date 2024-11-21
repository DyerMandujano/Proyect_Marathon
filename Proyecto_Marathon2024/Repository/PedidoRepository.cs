using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class PedidoRepository
    {
        private readonly DataAccses dataAccses;

        // Constructor para el acceso a la base de datos
        public PedidoRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        // Método para insertar un pedido
        public async Task<string> InsertPedido(int codTipoCompro, int codTpPago, string dniCliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Insert_Pedido", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = codTipoCompro;
                        command.Parameters.Add("@Cod_Tp_Pago", SqlDbType.Int).Value = codTpPago;
                        command.Parameters.Add("@Dni_Cliente", SqlDbType.VarChar).Value = dniCliente;

                        await command.ExecuteNonQueryAsync();
                        return "Pedido insertado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el pedido.", ex);
            }
        }

        // Método para listar todos los pedidos
        public async Task<List<Pedido>> GetListaPedidos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Listar_Pedidos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<Pedido> listaPedidos = new List<Pedido>();

                            while (await reader.ReadAsync())
                            {
                                Pedido pedido = new Pedido
                                {
                                    Cod_Pedido = reader.GetInt32(reader.GetOrdinal("Cod_Pedido")),
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Cod_Tp_Pago = reader.GetInt32(reader.GetOrdinal("Cod_Tp_Pago")),
                                    Dni_Cliente = reader.GetString(reader.GetOrdinal("Dni_Cliente")),
                                    Fecha_Pedido = reader.GetDateTime(reader.GetOrdinal("Fecha_Pedido"))
                                };
                                listaPedidos.Add(pedido);
                            }
                            return listaPedidos;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de pedidos.", ex);
            }
        }

        // Método para obtener un pedido por su ID
        public async Task<Pedido> GetPedidoPorID(int codPedido)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Obtener_Pedido_Por_Id", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_Pedido", SqlDbType.Int).Value = codPedido;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Pedido
                                {
                                    Cod_Pedido = reader.GetInt32(reader.GetOrdinal("Cod_Pedido")),
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Cod_Tp_Pago = reader.GetInt32(reader.GetOrdinal("Cod_Tp_Pago")),
                                    Dni_Cliente = reader.GetString(reader.GetOrdinal("Dni_Cliente")),
                                    Fecha_Pedido = reader.GetDateTime(reader.GetOrdinal("Fecha_Pedido"))
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
                throw new Exception("Error al obtener el pedido por ID.", ex);
            }
        }

        // Método para actualizar un pedido
        public async Task<string> UpdatePedido(int codPedido, int codTipoCompro, int codTpPago, string dniCliente, DateTime fechaPedido)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Actualizar_Pedido", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_Pedido", SqlDbType.Int).Value = codPedido;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = codTipoCompro;
                        command.Parameters.Add("@Cod_Tp_Pago", SqlDbType.Int).Value = codTpPago;
                        command.Parameters.Add("@Dni_Cliente", SqlDbType.VarChar).Value = dniCliente;
                        command.Parameters.Add("@Fecha_Pedido", SqlDbType.DateTime).Value = fechaPedido;

                        await command.ExecuteNonQueryAsync();
                        return "Pedido actualizado correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el pedido.", ex);
            }
        }
    }
}
