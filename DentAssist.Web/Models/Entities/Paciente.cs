namespace DentAssist.Web.Models.Entities
{
    public class Paciente
    {
        public int Id { get; set; } // Clave primaria
        public string Nombre { get; set; }
        public string RUT { get; set; } // Puede ser string para manejar guiones o puntos
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        // Propiedad de navegación para el historial de tratamientos
        public ICollection<Turno> Turnos { get; set; } // Un paciente puede tener muchos turnos
        public ICollection<PlanTratamiento> PlanesTratamiento { get; set; } // Un paciente puede tener varios planes de tratamiento
    }
}