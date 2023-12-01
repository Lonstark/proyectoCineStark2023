using System.ComponentModel.DataAnnotations;

namespace PrjPROYECTO_CINESTARK_2023.Models
{
    public class Usuario
    {
        public int idUsu { get; set; }
        [Display(Name ="Nombre")]
        public string nomUsu { get; set; }
        [Display(Name = "Apellido")]
        public string apeUsu { get; set; }
        public int idSexo { get; set; }
        [Display(Name = "Sexo")]
        public string descSexo { get; set; }
        [Display(Name = "DNI")]
        public string nroDoc { get; set; }
        [Display(Name = "Fec. Nacimiento")]
        public string fecNac { get; set; }
        [Display(Name = "Correo")]
        public string email { get; set; }
        public string contra { get; set; }
        [Display(Name = "Celular")]
        public string celular { get; set; }
        public int idTipo { get; set; }
        public string descTipo { get; set; }
        public int idUbigeo { get; set; }
        public string dpto { get; set; }
        public string prov { get; set; }
        public string distrito { get; set; }
    }
}
