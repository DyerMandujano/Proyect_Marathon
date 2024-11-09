namespace Proyecto_Marathon2024.Entidades
{
    public class Oferta
    {
        public int Cod_Oferta { get; set; }
        public int Cod_Prod { get; set; }
        public String Nombre_Oferta { get; set; }
        public String Descripcion { get; set; }
        public decimal Descuento { get; set; }
        public DateTime Fecha_Inic {  get; set; }
        public DateTime Fecha_Fin {  get; set; }

        public int Estado { get; set; }

    }
}
