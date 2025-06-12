using System;

namespace DentAssist.Web.Models.Entities
{
    public class Turno
    {
        public int Id { get; set; } // Clave primaria
        public DateTime FechaHora { get; set; }
        public int DuracionMinutos { get; set; } // Duración en minutos

        // Claves foráneas
        public int PacienteId { get; set; }
        public int OdontologoId { get; set; }

        // Propiedades de navegación
        public Paciente Paciente { get; set; }
        public Odontologo Odontologo { get; set; }

        // Estado del turno
        public TurnoEstado Estado { get; set; } = TurnoEstado.Pendiente; // Valor por defecto
    }

    public enum TurnoEstado
    {
        Pendiente,
        Confirmado,
        Realizado,
        Cancelado
    }
}