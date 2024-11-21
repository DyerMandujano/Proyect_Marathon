namespace Proyecto_Marathon2024.Entidades
{
    public class Pedido
    {
        public int Cod_Pedido { get; set; }        
        public int Cod_Tipo_Compro { get; set; }     
        public int Cod_Tp_Pago { get; set; }        
        public string Dni_Cliente { get; set; }       
        public DateTime Fecha_Pedido { get; set; }
    }
}
