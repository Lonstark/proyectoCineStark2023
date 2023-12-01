using System.ComponentModel.DataAnnotations;

namespace PrjPROYECTO_CINESTARK_2023.Models
{
    public class Boleta
    {
        [Display(Name = "Nro Ticket")]
        public string idEntrada { get; set; }
        [Display(Name = "Fecha")]
        public string fechCompra { get; set; }
        [Display(Name = "Hora")]
        public string hora { get; set; }
        [Display(Name = "Cine")]
        public string nomCine { get; set; }
        [Display(Name = "Sala")]
        public string nomSala { get; set; }
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
