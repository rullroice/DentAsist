using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DentAssist.Models
{
    public class PlanTratamiento
    {
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "El paciente asociado al plan de tratamiento es obligatorio.")]
        public int IdPaciente { get; set; } // Clave foránea para Paciente

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; } // Propiedad de navegación - ¡CAMBIO AQUÍ! Ahora es anulable (Paciente?)

        [Required(ErrorMessage = "El odontólogo que crea el plan de tratamiento es obligatorio.")]
        public int IdOdontologo { get; set; } // Clave foránea para Odontologo

        [ForeignKey("IdOdontologo")]
        public Odontologo? Odontologo { get; set; } // Propiedad de navegación - ¡CAMBIO AQUÍ! Ahora es anulable (Odontologo?)

        [Required(ErrorMessage = "La fecha de creación del plan es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [StringLength(1000, ErrorMessage = "Las observaciones generales no pueden exceder los 1000 caracteres.")]
        public string? ObservacionesGenerales { get; set; } // También lo hice anulable, ya que StringLength ya maneja su longitud

        // Propiedad de navegación para los pasos individuales del plan
        public ICollection<PasoTratamiento> PasosDelPlan { get; set; }

        public PlanTratamiento()
        {
            PasosDelPlan = new List<PasoTratamiento>();
        }
    }
}