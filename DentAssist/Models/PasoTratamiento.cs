using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentAssist.Models
{
    public class PasoTratamiento
    {
        public int Id { get; set; } // Primary Key

        [Required(ErrorMessage = "El plan de tratamiento asociado es obligatorio.")]
        public int IdPlanTratamiento { get; set; } // Foreign Key for PlanTratamiento

        [ForeignKey("IdPlanTratamiento")]
        public PlanTratamiento? PlanTratamiento { get; set; } // Navigation property - NOW NULLABLE

        [Required(ErrorMessage = "El tratamiento específico para este paso es obligatorio.")]
        public int IdTratamiento { get; set; } // Foreign Key for Tratamiento type

        [ForeignKey("IdTratamiento")]
        public Tratamiento? Tratamiento { get; set; } // Navigation property - NOW NULLABLE

        [Required(ErrorMessage = "La secuencia del paso es obligatoria.")]
        [Range(1, 100, ErrorMessage = "La secuencia debe ser un número positivo.")]
        public int Secuencia { get; set; } // Order within the plan (e.g. 1, 2, 3...)

        [Required(ErrorMessage = "La fecha estimada del paso es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaEstimada { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La fecha de realización del paso es obligatoria.")]
        public DateTime? FechaRealizado { get; set; } // Actual completion date (can be null)

        [Required(ErrorMessage = "El estado del paso es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres.")]
        public string Estado { get; set; } = string.Empty; // Added default initialization to fix CS8618 warning for non-nullable string

        [StringLength(500, ErrorMessage = "Las observaciones clínicas no pueden exceder los 500 caracteres.")]
        [Required(ErrorMessage = "Las observaciones clínicas son obligatorias.")]
        public string? ObservacionesClinicas { get; set; } // Changed to nullable string for flexibility, as StringLength doesn't imply requiredness
    }
}