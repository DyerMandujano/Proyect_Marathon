namespace Proyecto_Marathon2024.Abstracion
{
    public class DataAccses
    {
        private readonly string connectionString;
        public DataAccses(IConfiguration config)
        {
         connectionString = config.GetConnectionString("DefaultConnection");
        }

        //METODO DE CONEXION
        public string GetConnectionString()
        {
            return connectionString;
        }

    }
}
