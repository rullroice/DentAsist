using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentAssist.Models
{
    public class Tratamiento
    {
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "El nombre del tratamiento es obligatorio.")]
        [StringLength(150, ErrorMessage = "El nombre no puede exceder los 150 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } // Descripción detallada del tratamiento

        [Required(ErrorMessage = "El precio estimado es obligatorio.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 1000000.00, ErrorMessage = "El precio debe ser un valor positivo.")]
        [DataType(DataType.Currency)] // Sugiere un tipo de dato monetario
        public decimal PrecioEstimado { get; set; }

        // Propiedad de navegación para la relación con PasoTratamiento
        // Un Tratamiento puede ser parte de muchos PasosTratamiento (en diferentes planes)
        public ICollection<PasoTratamiento> PasosEnTratamientos { get; set; }

        public Tratamiento()
        {
            PasosEnTratamientos = new List<PasoTratamiento>();
        }
    }
}