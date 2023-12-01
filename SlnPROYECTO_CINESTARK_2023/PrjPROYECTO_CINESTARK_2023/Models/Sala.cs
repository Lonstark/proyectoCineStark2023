using System.ComponentModel.DataAnnotations;

namespace PrjPROYECTO_CINESTARK_2023.Models
{
    public class Sala
    {
        public int idSala { get; set; }
        public int idCine { get; set; }
        [Display(Name = "Cine")]
        public string nomCine { get; set; }
        [Display(Name = "Sala")]
        public string nombre { get; set; }
        [Display(Name = "Capacidad")]
        public int capacidad { get; set; }
        public int idFormato { get; set; }
        [Display(Name = "Formato")]
        public string descFormato { get; set; }
    }
}
