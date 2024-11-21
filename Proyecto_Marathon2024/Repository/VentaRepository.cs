using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Marathon2024.Repository
{
    public class VentaRepository
    {
        private readonly DataAccses dataAccses;

        // Constructor para el acceso a la base de datos
        public VentaRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        // Método para insertar una venta
        public async Task<string> InsertVenta(int codTipoCompro, string dniCliente, int codTpPago, string tipoEnvio = null, string direccionEnvio = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Insertar_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = codTipoCompro;
                        command.Parameters.Add("@Dni_Cliente", SqlDbType.VarChar, 8).Value = dniCliente;
                        command.Parameters.Add("@Cod_Tp_Pago", SqlDbType.Int).Value = codTpPago;
                        command.Parameters.Add("@Tipo_Envio", SqlDbType.VarChar, 50).Value = tipoEnvio ?? (object)DBNull.Value;
                        command.Parameters.Add("@Direccion_Envio", SqlDbType.VarChar, 100).Value = direccionEnvio ?? (object)DBNull.Value;

                        await command.ExecuteNonQueryAsync();

                        return "Venta insertada correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la venta.", ex);
            }
        }

        // Método para listar todas las ventas
        public async Task<List<Venta>> GetListaVenta()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Listar_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<Venta> listaVenta = new List<Venta>();

                            while (await reader.ReadAsync())
                            {
                                Venta venta = new Venta
                                {
                                    Num_Documento = reader.GetInt32(reader.GetOrdinal("Num_Documento")),
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Dni_Cliente = reader.GetString(reader.GetOrdinal("Dni_Cliente")),
                                    Cod_Tp_Pago = reader.GetInt32(reader.GetOrdinal("Cod_Tp_Pago")),
                                    Fecha_Doc = reader.GetDateTime(reader.GetOrdinal("Fecha_Doc")),
                                    Tipo_Envio = reader.IsDBNull(reader.GetOrdinal("Tipo_Envio")) ? null : reader.GetString(reader.GetOrdinal("Tipo_Envio")),
                                    Direccion_Envio = reader.IsDBNull(reader.GetOrdinal("Direccion_Envio")) ? null : reader.GetString(reader.GetOrdinal("Direccion_Envio"))
                                };

                                listaVenta.Add(venta);
                            }

                            return listaVenta;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de ventas.", ex);
            }
        }

        // Método para obtener una venta por ID
        public async Task<Venta> GetVentaPorID(int numDocumento, int codTipoCompro)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Obtener_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Num_Documento", SqlDbType.Int).Value = numDocumento;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = codTipoCompro;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Venta
                                {
                                    Num_Documento = reader.GetInt32(reader.GetOrdinal("Num_Documento")),
                                    Cod_Tipo_Compro = reader.GetInt32(reader.GetOrdinal("Cod_Tipo_Compro")),
                                    Dni_Cliente = reader.GetString(reader.GetOrdinal("Dni_Cliente")),
                                    Cod_Tp_Pago = reader.GetInt32(reader.GetOrdinal("Cod_Tp_Pago")),
                                    Fecha_Doc = reader.GetDateTime(reader.GetOrdinal("Fecha_Doc")),
                                    Tipo_Envio = reader.IsDBNull(reader.GetOrdinal("Tipo_Envio")) ? null : reader.GetString(reader.GetOrdinal("Tipo_Envio")),
                                    Direccion_Envio = reader.IsDBNull(reader.GetOrdinal("Direccion_Envio")) ? null : reader.GetString(reader.GetOrdinal("Direccion_Envio"))
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
                throw new Exception("Error al obtener la venta por ID.", ex);
            }
        }

        // Método para actualizar una venta
        public async Task<string> UpdateVenta(int numDocumento, int codTipoCompro, int codTipoComproNuevo, string dniCliente, int codTpPago, string tipoEnvio = null, string direccionEnvio = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("SP_Actualizar_Venta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Num_Documento", SqlDbType.Int).Value = numDocumento;
                        command.Parameters.Add("@Cod_Tipo_Compro", SqlDbType.Int).Value = codTipoCompro;
                        command.Parameters.Add("@Cod_Tipo_Compro_Nuevo", SqlDbType.Int).Value = codTipoComproNuevo;
                        command.Parameters.Add("@Dni_Cliente", SqlDbType.VarChar, 8).Value = dniCliente;
                        command.Parameters.Add("@Cod_Tp_Pago", SqlDbType.Int).Value = codTpPago;
                        command.Parameters.Add("@Tipo_Envio", SqlDbType.VarChar, 50).Value = tipoEnvio ?? (object)DBNull.Value;
                        command.Parameters.Add("@Direccion_Envio", SqlDbType.VarChar, 100).Value = direccionEnvio ?? (object)DBNull.Value;

                        await command.ExecuteNonQueryAsync();

                        return "Venta actualizada correctamente.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la venta.", ex);
            }
        }
    }
}
