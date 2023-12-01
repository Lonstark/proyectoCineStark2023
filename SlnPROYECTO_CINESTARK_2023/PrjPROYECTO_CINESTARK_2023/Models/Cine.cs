using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrjPROYECTO_CINESTARK_2023.Models
{
    public class Cine
    {
        public int idCine { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Dirección")]
        public string direccion { get; set; }
        public int idUbigeo { get; set; }
        public string dpto { get; set; }
        public string prov { get; set; }
        public string distrito { get; set; }
    }
}
