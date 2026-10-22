
using System.ComponentModel.DataAnnotations;

namespace CasoPractico1.Models
{
    public class UnidadMedida
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la unidad de medida es obligatorio.")]
        [StringLength(40, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
        [Display(Name = "Unidad de medida")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La abreviatura es obligatoria.")]
        [StringLength(10, ErrorMessage = "La abreviatura no puede superar los {1} caracteres.")]
        [Display(Name = "Abreviatura")]
        public string Abreviatura { get; set; } = string.Empty;
    }
}

