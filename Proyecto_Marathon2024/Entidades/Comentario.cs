namespace Proyecto_Marathon2024.Entidades
{
    public class Comentario
    {
        public int Cod_Comentario { get; set; }
        public int Cod_Prod { get; set; }
        public  String Dni_Cliente { get; set; }
        public String Comentari {  get; set; }
        public DateOnly Fecha_Comentario { get; set; }
    }
}
