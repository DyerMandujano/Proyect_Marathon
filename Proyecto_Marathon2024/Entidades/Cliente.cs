using System.Security.Cryptography.X509Certificates;

namespace Proyecto_Marathon2024.Entidades
{
    public class Cliente
    {
        //Atributos
        public string Dni_Cliente { get; set; }
        public int Cod_Mem {  get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo {  get; set; }
        public DateOnly Fecha_Nac {  get; set; }
        public string Genero { get; set; }
        public int Puntos_Acum {  get; set; }
        public string User_Cli {  get; set; }
        public string Contra_Cli {  get; set; } 
        public int Estado {  get; set; }
       
    }
}
