using System.ComponentModel.DataAnnotations;

namespace PrjPROYECTO_CINESTARK_2023.Models
{
    public class Entrada
    {
        public int nroEntrada { get; set; }
        [Display(Name = "Entrada")]
        public string idEntrada { get; set; }
        [Display(Name = "Fec. Compra")]
        public string fechCompra { get; set; }
        public string hora { get; set; }
        public int idUsu { get; set; }
        [Display(Name = "Cliente")]
        public string nomCli { get; set; }
        [Display(Name = "Función")]
        public int idFuncion { get; set; }
        [Display(Name = "Película")]
        public string nomPeli { get; set; }
        [Display(Name = "Butacas")]
        public string butacas { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }
        [Display(Name = "Total")]
        public decimal total { get; set; }
    }
}
