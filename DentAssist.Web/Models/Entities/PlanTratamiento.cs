using System;
using System.Collections.Generic;

namespace DentAssist.Web.Models.Entities
{
    public class PlanTratamiento
    {
        public int Id { get; set; } // Clave primaria
        public int PacienteId { get; set; }
        public int OdontologoId { get; set; } // El odontólogo que crea el plan
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Fecha de creación del plan
        public string ObservacionesGenerales { get; set; } // Observaciones generales del plan

        // Propiedades de navegación
        public Paciente Paciente { get; set; }
        public Odontologo Odontologo { get; set; }
        public ICollection<DetallePlanTratamiento> Detalles { get; set; } // Pasos del plan
    }
}