using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DentAssist.Models
{
    public class Odontologo
    {
        public int Id { get; set; } // Clave primaria

        [Required(ErrorMessage = "El nombre del odontólogo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        [StringLength(50, ErrorMessage = "La matrícula no puede exceder los 50 caracteres.")]
        public string Matricula { get; set; } // Ejemplo: matrícula profesional

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        [StringLength(100, ErrorMessage = "La especialidad no puede exceder los 100 caracteres.")]
        public string Especialidad { get; set; } // Ejemplo: Ortodoncia, Periodoncia, General, etc.

        [Required(ErrorMessage = "El email del odontólogo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [StringLength(100, ErrorMessage = "El email no puede exceder los 100 caracteres.")]
        public string Email { get; set; }

        // Propiedades de navegación
        public ICollection<Turno> Turnos { get; set; }
        public ICollection<PlanTratamiento> PlanesDeTratamiento { get; set; }

        public Odontologo()
        {
            Turnos = new List<Turno>();
            PlanesDeTratamiento = new List<PlanTratamiento>();
        }
    }
}