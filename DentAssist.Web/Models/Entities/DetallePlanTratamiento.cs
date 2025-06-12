using System;

namespace DentAssist.Web.Models.Entities
{
    public class DetallePlanTratamiento
    {
        public int Id { get; set; } // Clave primaria
        public int PlanTratamientoId { get; set; } // Clave foránea al PlanTratamiento
        public int TratamientoId { get; set; } // Clave foránea al tipo de Tratamiento

        public DateTime? FechaEstimada { get; set; } // Fecha estimada para este paso (puede ser nulo)
        public string ObservacionesClinicas { get; set; } // Observaciones específicas del paso
        public DetallePlanEstado Estado { get; set; } = DetallePlanEstado.Pendiente; // Estado del paso

        // Propiedades de navegación
        public PlanTratamiento PlanTratamiento { get; set; }
        public Tratamiento Tratamiento { get; set; }
    }

    public enum DetallePlanEstado
    {
        Pendiente,
        Realizado,
        Cancelado
    }
}