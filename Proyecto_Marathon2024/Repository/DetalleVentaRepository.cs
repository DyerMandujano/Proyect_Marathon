using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class DetalleVentaRepository
    {
        private readonly DataAccses dataAccses;

        // Constructor para el acceso a la base de datos
        public DetalleVentaRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        // Método para insertar un detalle de venta y actualizar puntos del cliente
        public async Task<string> InsertDetalleVenta(Detalle_Venta detalleVenta, string dniCliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Insertar_Detalle_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Num_Documento", SqlDbType.Int).Value = detalleVenta.Num_Documento;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = detalleVenta.Cod_Tipo_Compro;
                        command.Parameters.Add("@Cod_Det_Ped", SqlDbType.Int).Value = detalleVenta.Cod_Det_Ped;
                        command.Parameters.Add("@Cod_Desct_Membe", SqlDbType.Int).Value = detalleVenta.Cod_Desct_Membe;
                        command.Parameters.Add("@Cod_Almacen", SqlDbType.Int).Value = detalleVenta.Cod_Almacen;
                        command.Parameters.Add("@Cod_Prod", SqlDbType.Int).Value = detalleVenta.Cod_Prod;
                        command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalleVenta.Cantidad;
                        command.Parameters.Add("@Precio_Venta", SqlDbType.Decimal).Value = detalleVenta.Precio_Venta;
                        command.Parameters.Add("@Dni_Cliente", SqlDbType.VarChar).Value = dniCliente;

                        await command.ExecuteNonQueryAsync();
                        return "Detalle de venta insertado y puntos del cliente actualizados correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el detalle de venta.", ex);
            }
        }

        // Método para listar todos los detalles de venta
        public async Task<List<Detalle_Venta>> GetListaDetalleVenta()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Listar_Detalle_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<Detalle_Venta> listaDetalle = new List<Detalle_Venta>();

                            while (await reader.ReadAsync())
                            {
                                Detalle_Venta detalle = new Detalle_Venta
                                {
                                    Cod_DetalleVenta = reader.GetInt32(reader.GetOrdinal("Cod_DetalleVenta")),
                                    Num_Documento = reader.GetInt32(reader.GetOrdinal("Num_Documento")),
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Cod_Det_Ped = reader.GetInt32(reader.GetOrdinal("Cod_Det_Ped")),
                                    Cod_Desct_Membe = reader.GetInt32(reader.GetOrdinal("Cod_Desct_Membe")),
                                    Cod_Almacen = reader.GetInt32(reader.GetOrdinal("Cod_Almacen")),
                                    Cod_Prod = reader.GetInt32(reader.GetOrdinal("Cod_Prod")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Precio_Venta = reader.GetDecimal(reader.GetOrdinal("Precio_Venta")),
                                    Puntos_Por_Venta = reader.GetInt32(reader.GetOrdinal("Puntos_Por_Venta"))
                                };
                                listaDetalle.Add(detalle);
                            }
                            return listaDetalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de detalles de venta.", ex);
            }
        }

        // Método para obtener un detalle de venta específico por ID
        public async Task<Detalle_Venta> GetDetalleVentaPorID(int codDetalleVenta, int numDocumento, int codTipoCompro)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Obtener_Detalle_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_DetalleVenta", SqlDbType.Int).Value = codDetalleVenta;
                        command.Parameters.Add("@Num_Documento", SqlDbType.Int).Value = numDocumento;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = codTipoCompro;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Detalle_Venta
                                {
                                    Cod_DetalleVenta = reader.GetInt32(reader.GetOrdinal("Cod_DetalleVenta")),
                                    Num_Documento = reader.GetInt32(reader.GetOrdinal("Num_Documento")),
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Cod_Det_Ped = reader.GetInt32(reader.GetOrdinal("Cod_Det_Ped")),
                                    Cod_Desct_Membe = reader.GetInt32(reader.GetOrdinal("Cod_Desct_Membe")),
                                    Cod_Almacen = reader.GetInt32(reader.GetOrdinal("Cod_Almacen")),
                                    Cod_Prod = reader.GetInt32(reader.GetOrdinal("Cod_Prod")),
                                    Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                                    Precio_Venta = reader.GetDecimal(reader.GetOrdinal("Precio_Venta")),
                                    Puntos_Por_Venta = reader.GetInt32(reader.GetOrdinal("Puntos_Por_Venta"))
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
                throw new Exception("Error al obtener el detalle de venta por ID.", ex);
            }
        }

        // Método para actualizar un detalle de venta y ajustar los puntos acumulados del cliente
        public async Task<string> UpdateDetalleVenta(int codDetalleVenta, Detalle_Venta detalleVenta, string dniCliente)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Actualizar_Detalle_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@Cod_DetalleVenta", SqlDbType.Int).Value = codDetalleVenta;
                        command.Parameters.Add("@Num_Documento", SqlDbType.Int).Value = detalleVenta.Num_Documento;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = detalleVenta.Cod_Tipo_Compro;
                        command.Parameters.Add("@Cod_Det_Ped", SqlDbType.Int).Value = detalleVenta.Cod_Det_Ped;
                        command.Parameters.Add("@Cod_Desct_Membe", SqlDbType.Int).Value = detalleVenta.Cod_Desct_Membe;
                        command.Parameters.Add("@Cod_Almacen", SqlDbType.Int).Value = detalleVenta.Cod_Almacen;
                        command.Parameters.Add("@Cod_Prod", SqlDbType.Int).Value = detalleVenta.Cod_Prod;
                        command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalleVenta.Cantidad;
                        command.Parameters.Add("@Precio_Venta", SqlDbType.Decimal).Value = detalleVenta.Precio_Venta;
                        command.Parameters.Add("@Dni_Cliente", SqlDbType.VarChar).Value = dniCliente;

                        await command.ExecuteNonQueryAsync();
                        return "Detalle de venta actualizado y puntos del cliente ajustados correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el detalle de venta.", ex);
            }
        }
    }
}
