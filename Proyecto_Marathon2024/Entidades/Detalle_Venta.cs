namespace Proyecto_Marathon2024.Entidades
{
    public class Detalle_Venta
    {
        public int Cod_DetalleVenta { get; set; }
        public int Num_Documento { get; set; }
        public int Cod_Tipo_Compro { get; set; }
        public int Cod_Det_Ped { get; set; }
        public int Cod_Desct_Membe { get; set; }
        public int Cod_Almacen { get; set; }
        public int Cod_Prod { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Venta { get; set; }
        public int Puntos_Por_Venta { get; set; }
    }
}
