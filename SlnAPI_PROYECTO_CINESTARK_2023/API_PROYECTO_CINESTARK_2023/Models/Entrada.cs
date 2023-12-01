namespace API_PROYECTO_CINESTARK_2023.Models
{
    public class Entrada
    {
        public int nroEntrada { get; set; }
        public string idEntrada { get; set; }
        public string fechCompra { get; set; }
        public string hora { get; set; }
        public int idUsu { get; set; }
        public string nomCli { get; set; }
        public int idFuncion { get; set; }
        public string nomPeli { get; set; }
        public string butacas { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
    }
}
