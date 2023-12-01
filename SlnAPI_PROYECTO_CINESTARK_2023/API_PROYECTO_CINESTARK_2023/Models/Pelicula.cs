using System.ComponentModel.DataAnnotations;

namespace API_PROYECTO_CINESTARK_2023.Models
{
    public class Pelicula
    {
        [Display(Name = "Código")]
        public int idPelicula { get; set; }

        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Imagen")]
        public string img { get; set; }

        [Display(Name = "Sinopsis")]
        public string sinopsis { get; set; }

        [Display(Name = "Tráiler")]
        public string trailer { get; set; }

        [Display(Name = "Fecha Estreno")]
        public string fechEstreno { get; set; }

        [Display(Name = "Duración")]
        public string duracion { get; set; }

        [Display(Name = "Director")]
        public string director { get; set; }

        [Display(Name = "Id Gen")]
        public int idGen { get; set; }

        [Display(Name = "Género")]
        public string descGen { get; set; }

        public string idiomas { get; set; }
        public string formatos { get; set; }

        [Display(Name = "Id Clasif")]
        public int idClas { get; set; }

        [Display(Name = "Clasificación")]
        public string descClas { get; set; }

        [Display(Name = "Estado")]
        public string estado { get; set; }
    }
}
