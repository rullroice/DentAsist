using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Para [ForeignKey]

namespace DentAssist.Models
{
    public class Turno
    {
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "La fecha y hora del turno son obligatorias.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "La duración del turno es obligatoria.")]
        [Range(15, 240, ErrorMessage = "La duración debe ser entre 15 y 240 minutos.")]
        public int DuracionMinutos { get; set; } // Duración estimada en minutos

        [Required(ErrorMessage = "El paciente es obligatorio para el turno.")]
        public int IdPaciente { get; set; } // Clave foránea para Paciente

        [ForeignKey("IdPaciente")]
        public Paciente? Paciente { get; set; } // ¡CAMBIO AQUÍ! Ahora es anulable (Paciente?)

        [Required(ErrorMessage = "El odontólogo es obligatorio para el turno.")]
        public int IdOdontologo { get; set; } // Clave foránea para Odontologo

        [ForeignKey("IdOdontologo")]
        public Odontologo? Odontologo { get; set; } // ¡CAMBIO AQUÍ! Ahora es anulable (Odontologo?)

        [Required(ErrorMessage = "El estado del turno es obligatorio.")]
        [StringLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres.")]
        public string Estado { get; set; } = string.Empty; // Inicialización para evitar CS8618 si no se le da un valor por defecto.

        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string? Descripcion { get; set; } // ¡CAMBIO AQUÍ! Ahora es anulable (string?)
    }
}