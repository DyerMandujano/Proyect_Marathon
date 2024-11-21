namespace Proyecto_Marathon2024.Entidades
{
    public class Venta
    {
        public int Num_Documento { get; set; }
        public int Cod_Tipo_Compro { get; set; }
        public string Dni_Cliente { get; set; }
        public int Cod_Tp_Pago { get; set; }
        public DateTime Fecha_Doc { get; set; }
        public string Tipo_Envio { get; set; }
        public string Direccion_Envio { get; set; }

    }
}
