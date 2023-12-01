namespace API_PROYECTO_CINESTARK_2023.Models
{
    public class Boleta
    {
        public string idEntrada { get; set; }
        public string fechCompra { get; set; }
        public string hora { get; set; }
        public string nomCine { get; set; }
        public string nomSala { get; set; }
        public string nomPeli { get; set; }
        public string butacas { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
    }
}
