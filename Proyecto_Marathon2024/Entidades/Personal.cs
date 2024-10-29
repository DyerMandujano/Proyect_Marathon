namespace Proyecto_Marathon2024.Entidades
{
    public class Personal
    {
        //Propiedades
        public string Dni_Personal { get; set; }
        public int Cod_Perfil { get; set; }
        public int Cod_Tipo_Trabajo { get; set; }
        public int Cod_Local { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public DateOnly Fecha_Nac { get; set; }
        public string Genero { get; set; }
        public string User_Per { get; set; }
        public string Contra_Per { get; set; }
        public int Estado { get; set; }

    }
}
