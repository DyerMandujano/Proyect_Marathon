﻿using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Net;


namespace Proyecto_Marathon2024.Repository
{
    public class TallaRepository
    {
        private readonly DataAccses dataAccses;

        //Contructor para el acceso a la base de datos
        public TallaRepository(DataAccses dataAccses)
        {
            this.dataAccses = dataAccses;
        }

        public async Task<List<Talla>> GetListaTalla()
        {

            try
            {
                // Crear la conexión utilizando el método para obtener la cadena de conexión
                using (SqlConnection connection = new SqlConnection(dataAccses.GetConnectionString()))
                {
                    // Abre la conexión de manera asíncrona
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (SqlCommand command = new SqlCommand("SP_ListarTalla", connection))
                    {
                        // Especificar que se trata de un procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el comando y obtener el SqlDataReader de manera asíncrona
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            // Definir la lista que contendrá los resultados
                            List<Talla> listaTalla = new List<Talla>();

                            // Leer los resultados mientras haya filas
                            while (await reader.ReadAsync())
                            {
                                // Crear una nueva instancia de Producto y asignar los valores leídos
                                //string fec = reader.GetDateTime(reader.GetOrdinal("Fecha_Nac")).ToShortDateString();
                                //DateOnly fecha = DateOnly.Parse(fec);
                                Talla talla = new Talla
                                {
                                    Cod_Talla = reader.GetInt32(reader.GetOrdinal("Cod_Talla")),
                                    Nom_Talla = reader.GetString(reader.GetOrdinal("Nom_Talla")),
                                    Estado = reader.GetInt32(reader.GetOrdinal("Estado"))
                                };

                                // Añadir el perfil a la lista
                                listaTalla.Add(talla);
                            }

                            return listaTalla;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new Exception("Error al obtener la lista de talla.", ex);
            }
        }
    }
}