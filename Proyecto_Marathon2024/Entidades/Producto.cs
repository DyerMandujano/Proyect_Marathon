namespace Proyecto_Marathon2024.Entidades
{
    public class Producto
    {

        public int Cod_Prod {  get; set; }
        public int Cod_Color { get; set; }
        public int Cod_Modelo { get; set; }
        public int Cod_Marca { get; set; }
        public int Cod_Cate { get; set; }
        public int Cod_Talla { get; set; }
        public String Nom_Prod { get; set; }
        public String Descripcion { get; set; }
        public decimal Precio { get; set; }
        public  String Image_front { get; set; }
        public String Image_back { get; set;}
        public int Estado { get; set; }
    }
}
