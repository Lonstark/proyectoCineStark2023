using System.ComponentModel.DataAnnotations;

namespace PrjPROYECTO_CINESTARK_2023.Models
{
    public class FuncionForAdmin
    {
        public int idFuncion { get; set; }
        [Display(Name = "Película")]
        public string nomPeli { get; set; }
        public string dpto { get; set; }
        public string prov { get; set; }
        public string distrito { get; set; }
        [Display(Name = "Cine")]
        public string nomCine { get; set; }
        [Display(Name = "Sala")]
        public string nomSala { get; set; }
        [Display(Name = "Formato")]
        public string descFormat { get; set; }
        [Display(Name = "Idioma")]
        public string descIdioma { get; set; }
        [Display(Name = "Fecha")]
        public string fecha { get; set; }
        [Display(Name = "Hora")]
        public string hora { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
    }
}
